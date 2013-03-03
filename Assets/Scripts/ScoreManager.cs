using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{	
	public int[] playerScore = new int[(int)NinjaController.PlayerID.NumPlayer];
	private float nextUpdateTime = 0.0F;
	public float updateIntervalMax = 1.0F; // maximum of 10 updates per second
	
	private static ScoreManager _instance;
	public static ScoreManager Instance {
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
	
	public int GetScore(int noPlayer)
	{
		return playerScore[noPlayer];
	}
}
