using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour 
{	
	public int[] playerScore = new int[(int)NinjaController.PlayerID.NumPlayer];
	private float nextUpdateTime = 0.0F;
	public float updateIntervalMax = 1.0F; // maximum of 10 updates per second
	
	private static ScoreGUI _instance;
	public static ScoreGUI Instance {
		get { return _instance; }
	}
	
	private void Start() {
		if (_instance == null) {
			_instance = this;
		}
	}
	
	private void Destroy() {
		if (_instance == this) {
			_instance = null;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.realtimeSinceStartup > nextUpdateTime)
		{
			nextUpdateTime = Time.realtimeSinceStartup + updateIntervalMax;
		}
	}
	
	public void AddScore(int playerID, int myValue)
	{
		playerScore[playerID] += myValue;
	}
	
 	void OnGUI ()
	{
		GUILayout.Label("Player 1 : " + playerScore[0]);
		GUILayout.Label("Player 2 : " + playerScore[1]);
	}
}
