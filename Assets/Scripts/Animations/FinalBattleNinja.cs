using UnityEngine;
using System.Collections;

public class FinalBattleNinja : MonoBehaviour {
	
	Spritesheet sheet;
	
	public enum Player
	{
		PLAYER_1,
		PLAYER_2
	}
	
	public Player player;
	
	// Use this for initialization
	void Start () {
	
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/ninja");
		sheet.CreateAnimation("Idle", 200);
		
		if(player == Player.PLAYER_1)
			sheet.AddFrame("Idle", 0, 0, 128, 128);
		
		else
			sheet.AddFrame("Idle", 128, 0, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
	
		sheet.Render();
	}
}
