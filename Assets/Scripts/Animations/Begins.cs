using UnityEngine;
using System.Collections;

public class Begins : MonoBehaviour {

	Spritesheet sheet;
	
	public enum Player
	{
		PLAYER_1,
		PLAYER_2
	}
	
	public Player player;
	private Vector3 startPos;
	
	float timer = 0;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/NinjaVersion2");
		
		sheet.CreateAnimation("IdleRight", 200);
		sheet.AddFrame("IdleRight", 0, 0, 128, 128);
		sheet.AddFrame("IdleRight", 0, 128, 128, 128);
		sheet.AddFrame("IdleRight", 0, 256, 128, 128);
		sheet.AddFrame("IdleRight", 0, 384, 128, 128);
		sheet.AddFrame("IdleRight", 0, 512, 128, 128);
		sheet.AddFrame("IdleRight", 0, 640, 128, 128);
		sheet.AddFrame("IdleRight", 0, 768, 128, 128);
		sheet.AddFrame("IdleRight", 0, 896, 128, 128);
		
		sheet.CreateAnimation("IdleLeft", 200);
		sheet.AddFrame("IdleLeft", 128, 0, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 128, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 256, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 384, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 512, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 640, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 768, 128, 128);
		sheet.AddFrame("IdleLeft", 128, 896, 128, 128);
		
		if(player == Player.PLAYER_1)
			sheet.SetCurrentAnimation("IdleRight");
		
		else
			sheet.SetCurrentAnimation("IdleLeft");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		sheet.Render();
		
		if(player == Player.PLAYER_1)
		{
			float x = Mathf.Lerp(startPos.x, -10, timer);
			transform.position = new Vector3(x, startPos.y, startPos.z);
		}
		
		else
		{
			float x = Mathf.Lerp(startPos.x, 10, timer);
			transform.position = new Vector3(x, startPos.y, startPos.z);
		}
	}
}
