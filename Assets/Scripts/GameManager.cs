using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public Transform Player1;
	public Transform Player2;
	
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
			if(Player2.position.x - Player1.position.x < 60)
			{
				Debug.Log ("Battle");
				int player1Score = gameObject.GetComponent<ScoreManager>().playerScore[0];
				int player2Score = gameObject.GetComponent<ScoreManager>().playerScore[1];
				
				player1Score += (int)(Player1.position.x - player1StartX);
				player2Score += (int)(player2StartX - Player2.position.x);
				
				if(player1Score > player2Score)
				{
					StartCoroutine(BattleAndKillLooser(Player1.gameObject, Player2.gameObject, player1Score - player2Score));
				}
				
				else
				{
					StartCoroutine(BattleAndKillLooser(Player2.gameObject, Player1.gameObject, player2Score - player1Score));
				}
			}
		}
	}
	
	IEnumerator BattleAndKillLooser(GameObject winner, GameObject looser, int deltaScore)
	{
		float seconds = Mathf.Log(player2StartX - player1StartX - deltaScore);
		
		if(seconds < 0)
			seconds = 0;
		
		Debug.Log (seconds);
		inAnim = true;
		
		yield return new WaitForSeconds(seconds);
		
		Debug.Log ("Winner : " + winner.name);
		Debug.Log (deltaScore);
		Debug.Break();
	}
}
