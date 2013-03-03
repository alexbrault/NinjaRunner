using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject finalBattle;
	
	public Transform Player1;
	public Transform Player2;
	public Transform AudioManager;
	public float EndGameThreshold = 20;
	
	private float player1StartX;
	private float player2StartX;
	
	private bool inAnim = false;
	
	// Use this for initialization
	void Start () {
	
		player1StartX = Player1.position.x;
		player2StartX = Player2.position.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!inAnim)
		{
			if(Player2.position.x - Player1.position.x < EndGameThreshold)
			{
				int player1Score = gameObject.GetComponent<ScoreManager>().playerScore[0];
				int player2Score = gameObject.GetComponent<ScoreManager>().playerScore[1];
				
				player1Score += (int)(Player1.position.x - player1StartX);
				player2Score += (int)(player2StartX - Player2.position.x);
				
				
				GameObject battle = (GameObject)GameObject.Instantiate(finalBattle);
				GameObject.DontDestroyOnLoad(battle);
				
				battle.GetComponent<FinalBattle>().maxDeltaScore = (int)(player2StartX - player1StartX);
				
				if(player1Score > player2Score)
				{
					battle.GetComponent<FinalBattle>().winner = Player1.gameObject;
					battle.GetComponent<FinalBattle>().looser = Player2.gameObject;
					battle.GetComponent<FinalBattle>().deltaScore = player1Score - player2Score;
				}
				
				else
				{
					battle.GetComponent<FinalBattle>().winner = Player2.gameObject;
					battle.GetComponent<FinalBattle>().looser = Player1.gameObject;
					battle.GetComponent<FinalBattle>().deltaScore = player2Score - player1Score;
				}
				
				GameObject.DontDestroyOnLoad(Player1);
				GameObject.DontDestroyOnLoad(Player2);
				GameObject.DontDestroyOnLoad(AudioManager);
				
				Destroy(Player1.rigidbody);
				Destroy(Player2.rigidbody);
				Destroy(Player1.collider);
				Destroy(Player2.collider);
				Destroy(Player1.GetComponent<NinjaController>());
				Destroy(Player2.GetComponent<NinjaController>());
				
				Player1.position = new Vector3(-5, 0, 0);
				Player2.position = new Vector3(5, 0, 0);
				
				Player1.transform.parent = battle.transform;
				Player2.transform.parent = battle.transform;
				
				battle.GetComponent<FinalBattle>().player1 = Player1.gameObject;
				battle.GetComponent<FinalBattle>().player2 = Player2.gameObject;
				
				Player1.gameObject.AddComponent<FinalBattleNinja>();
				Player2.gameObject.AddComponent<FinalBattleNinja>().player = FinalBattleNinja.Player.PLAYER_2;
				
				Application.LoadLevel("FinalBattle");
			}
		}
	}
}
