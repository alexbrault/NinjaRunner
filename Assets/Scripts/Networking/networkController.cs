using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Text;
using System.IO;

enum state 
{
	invalid=0,
	mainmenu=1,
	joinmenu=2,
	hostmenu=3,
	waitserver=4,
	waitclient=5,
	failconnect=6,
	playing=7
}

public class networkController : MonoBehaviour 
{	
	private string LocalAddress = "127.0.0.1";
	private string ServerAddress = "127.0.0.1";
	private int GameState = (int)state.waitserver;
	private int playerCount = 0;
	private bool LANOnly = true;
	private float nextNetworkUpdateTime = 0.0F;
	private GameObject localPlayerObject;
	private Hashtable players = new Hashtable();
	private Vector3 lastLocalPlayerPosition;
	
	public NetworkPlayer localPlayer;
	public GameObject playerPrefab;
	public float networkUpdateIntervalMax = 0.1F;
	
	class MyState
	{
		public Vector3 pos;
		public Vector3 velocity;
		public Quaternion rot;
		public Vector3 angularVelocity;
		
		static XmlSerializer serializer = new XmlSerializer(typeof(MyState));

	    public string Serialize()
	    {
	        StringBuilder builder = new StringBuilder();

	        serializer.Serialize(System.Xml.XmlWriter.Create(builder), this);

	        return builder.ToString();
	    }

	    public static MyState Deserialize(string serializedData)
	    {
	        return serializer.Deserialize(new StringReader(serializedData)) as MyState;
	    }
	}
	
	void Start () 
	{	
		LocalAddress = GetLocalIPAddress();
		ServerAddress = LocalAddress;
		GameState = (int)state.mainmenu;
	}
	
	void Update () 
	{
		if(Time.realtimeSinceStartup > nextNetworkUpdateTime)
		{
			nextNetworkUpdateTime = Time.realtimeSinceStartup + networkUpdateIntervalMax;
			if(localPlayerObject!=null)
			{
				if(lastLocalPlayerPosition != localPlayerObject.transform.position)
				{
					lastLocalPlayerPosition = localPlayerObject.transform.position;
					
					MyState s = new MyState();
					s.pos = localPlayerObject.rigidbody.position;
					s.velocity = localPlayerObject.rigidbody.velocity;
					s.rot = localPlayerObject.rigidbody.rotation;
					s.angularVelocity = localPlayerObject.rigidbody.angularVelocity;
					
					string serialized = s.Serialize();
					
					if(Network.isClient)
					{
						networkView.RPC("ClientUpdatePlayer",RPCMode.Server,serialized);
					}
					/*
					else
					{
						networkView.RPC("ServerUpdatePlayer",RPCMode.Others, Network.player, s);
					}*/
				}
			}
		}		
	}
	
	public string GetLocalIPAddress()
	{
	   IPHostEntry host;
	   string localIP = "";
	   host = Dns.GetHostEntry(Dns.GetHostName());
	   foreach (IPAddress ip in host.AddressList)
	   {
	     if (ip.AddressFamily == AddressFamily.InterNetwork)
	     {
	       localIP = ip.ToString();
	     }
	   }
	   return localIP;
	}
	
	void ConnectToServer()
	{
    	Network.Connect(ServerAddress, 25000);
		GameState = (int)state.waitserver;
	}
	
	void OnConnectedToServer()
	{	
		networkView.RPC("SendAllPlayers", RPCMode.Server);
	}
	
	[RPC]
	void SendAllPlayers(NetworkMessageInfo info)
	{
		if(Network.isServer)
		{
			GameObject[] goPlayers = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject gop in goPlayers)
			{
				NetworkPlayer gonp = gop.GetComponent<NinjaController>().netPlayer;
				NetworkViewID gonvid = gop.GetComponent<NetworkView>().viewID;
						
				if(gonp.ToString() != info.sender.ToString())
				{
					networkView.RPC("JoinPlayer", info.sender, gonvid, gop.transform.position, gonp);
				}
	    	}
		}
	}
	
    void OnFailedToConnect(NetworkConnectionError error) 
	{
		GameState = (int)state.failconnect;
    }

	void StartServer()
	{
		bool useNat=false;
		if (LANOnly==true)
			useNat=false;
		else
			useNat=!Network.HavePublicAddress();
		
		Network.InitializeServer(16,25000,useNat);
	}
	
	void CreateServerPlayer()
	{
		playerCount++;
		
		NetworkViewID newViewID = Network.AllocateViewID();
		
		Vector3 pos = Vector3.zero;		
		JoinPlayer(newViewID, pos, Network.player);
	}
	
	void OnServerInitialized() 
	{
		GameState = (int)state.waitclient;
    }
	
	[RPC]
	void ClientUpdatePlayer(string serialized, NetworkMessageInfo info)
	{
		
		NetworkPlayer p = info.sender;
		networkView.RPC("ServerUpdatePlayer",RPCMode.Others, p, serialized);
		
		ServerUpdatePlayer(p, serialized);
	}
	
	[RPC]
	void ServerUpdatePlayer(NetworkPlayer p, string serialized)
	{
		if(players.ContainsKey(p))
		{
			GameObject gop = (GameObject)players[p];
			MyState s = MyState.Deserialize(serialized);
			
			gop.rigidbody.position = s.pos;
			gop.rigidbody.velocity = s.velocity;
			gop.rigidbody.rotation = s.rot;
			gop.rigidbody.angularVelocity = s.angularVelocity;
		}
	}
	
 	void OnPlayerConnected(NetworkPlayer p) 
	{
		if(Network.isServer)
		{
			playerCount++;
			
			NetworkViewID newViewID = Network.AllocateViewID();
			
			networkView.RPC("JoinPlayer", RPCMode.All, newViewID, Vector3.zero, p);
		}
    }
	
	[RPC]
	void JoinPlayer(NetworkViewID newPlayerView, Vector3 pos, NetworkPlayer p)
	{
		GameObject newPlayer = Instantiate(playerPrefab, pos, Quaternion.Euler(new Vector3(270.0f, 0.0f, 0.0f))) as GameObject;
		newPlayer.GetComponent<NetworkView>().viewID = newPlayerView;
		newPlayer.tag = "Player";
		
		newPlayer.GetComponent<NinjaController>().transform.position = pos;
		
		newPlayer.GetComponent<NinjaController>().netPlayer = p;
		
		players.Add(p,newPlayer);
		
		if(p.ipAddress!=LocalAddress)
		{
			newPlayer.GetComponent<NinjaController>().SetPlayer(NinjaController.PlayerID.PlayerNone);
			Destroy(newPlayer.transform.FindChild("Camera").gameObject);
		} 
			
		else
		{
			if(Network.isClient)
			{
				newPlayer.GetComponent<NinjaController>().SetPlayer(NinjaController.PlayerID.Player2);
			}
			
			else
			{		
				newPlayer.GetComponent<NinjaController>().SetPlayer(NinjaController.PlayerID.Player1);
			}
			
			localPlayerObject = newPlayer;
			GameState = (int)state.playing;
		}
	}
	
	void OnPlayerDisconnected(NetworkPlayer player) 
	{
		if(Network.isServer)
		{
			playerCount--;
			networkView.RPC("DisconnectPlayer", RPCMode.All, player);	
		}
    }
	
	[RPC]
	void DisconnectPlayer(NetworkPlayer player)
	{
		
		if(players.ContainsKey(player))
		{
			if((GameObject)players[player]) 
			{
				Destroy((GameObject)players[player]);
			}
			
			players.Remove(player);
		}
	}
	
 	void OnGUI()
	{
		switch (GameState) 
		{
		case (int)state.mainmenu:
			if(GUILayout.Button("Join Game"))
			{
				GameState = (int)state.joinmenu;
			}
			if(GUILayout.Button("Host Game"))
			{
				GameState = (int)state.hostmenu;
			}
			if(GUILayout.Button("Quit"))
			{
				Application.Quit();
			}
			break;

		case (int)state.joinmenu:
			
			GUILayout.BeginHorizontal();
				GUILayout.Label("Server Address: ");
				ServerAddress = GUILayout.TextField(ServerAddress);
			GUILayout.EndHorizontal();
			
			LANOnly = GUILayout.Toggle(LANOnly, "Local Network Only");
			
			if(GUILayout.Button("Join!"))
			{
				ConnectToServer();
			}
			if(GUILayout.Button("Cancel"))
			{
				GameState = (int)state.mainmenu;
			}
			break;

		case (int)state.hostmenu:
			if(GUILayout.Button("Host!"))
			{
				StartServer();
				CreateServerPlayer();				
			}
			if(GUILayout.Button("Cancel"))
			{
				GameState = (int)state.mainmenu;
			}
			break;

		case (int)state.waitserver:
			GUILayout.Label("Connecting...");
			if(GUILayout.Button("Cancel"))
			{
				Network.Disconnect();
				GameState = (int)state.joinmenu;
			}
			break;
			
		case (int)state.failconnect:
			GUILayout.Label("Connection to server failed");
			if(GUILayout.Button("I'll check my firewall, IP Address, Server Address, etc..."))
			{
				GameState = (int)state.joinmenu;
			}
			break;
			
		case (int)state.waitclient:
			GUILayout.Label("SERVER RUNNING");
			GUILayout.Label("waiting for connections...");
			GUILayout.Space(16);
			GUILayout.BeginHorizontal();
				GUILayout.Label("Connected Players: " + playerCount.ToString());
			GUILayout.EndHorizontal();

			if(GUILayout.Button("Kill Server"))
			{
				Network.Disconnect();
				Application.Quit();
			}
			break;
			
		case (int)state.playing:
			/*GUILayout.Label("FPS Networking Sample");
			GUILayout.Label("---------------------");
			GUILayout.Label("WASD keys to move");
			GUILayout.Label("Hold down mouse button 1");
			GUILayout.Label("to mouselook, space to jump");*/
			//Debug.Log(localPlayerObject.gameObject);
			//GUILayout.Label("Score : " + localPlayerObject.gameObject.GetComponent<ScoreGUI>().playerScore);
			break;	
		}
	}	
}