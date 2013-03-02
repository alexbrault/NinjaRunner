using UnityEngine;
using System.Collections;

public class SwordFight : MonoBehaviour {

	Spritesheet sheet;
	
	// Use this for initialization
	void Start () {
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/AttaqueFinwExplosion");
		sheet.CreateAnimation("Fight", 50);
		sheet.AddFrame("Fight", 0, 0, 64, 64);
		sheet.AddFrame("Fight", 0, 64, 64, 64);/*
		sheet.AddFrame("Fight", 0, 128, 64, 64);
		sheet.AddFrame("Fight", 0, 192, 64, 64);
		sheet.AddFrame("Fight", 0, 256, 64, 64);
		sheet.AddFrame("Fight", 0, 320, 64, 64);
		sheet.AddFrame("Fight", 0, 384, 64, 64);
		sheet.AddFrame("Fight", 0, 448, 64, 64);
		sheet.AddFrame("Fight", 0, 512, 64, 64);
		sheet.AddFrame("Fight", 0, 192, 64, 64);
		sheet.AddFrame("Fight", 0, 256, 64, 64);
		sheet.AddFrame("Fight", 0, 320, 64, 64);*/
	}
	
	// Update is called once per frame
	void Update () {
		sheet.Render();
	}
}
