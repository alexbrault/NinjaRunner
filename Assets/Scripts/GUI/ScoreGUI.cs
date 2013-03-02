using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour 
{	
	public int playerScore = 0;
	public int playerTime = 300; 
	private float nextUpdateTime = 0.0F;
	public float updateIntervalMax = 1.0F; // maximum of 10 updates per second
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.realtimeSinceStartup > nextUpdateTime)
		{
			nextUpdateTime = Time.realtimeSinceStartup + updateIntervalMax;
			playerTime--;
		}
	}
	
	void AddScore(int myValue)
	{
		playerScore += myValue;
	}
	
	void AddTimeToScore()
	{
		if(playerTime > 0)
			playerScore += playerTime*10; 
	}
	
 	void OnGUI ()
	{
		GUILayout.Label("Score : " + playerScore);
		GUILayout.Label("Time : " + playerTime);		
	}
}
