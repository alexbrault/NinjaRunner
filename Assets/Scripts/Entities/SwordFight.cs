using UnityEngine;
using System.Collections;

public class SwordFight : MonoBehaviour {

	Spritesheet sheet;
	
	// Use this for initialization
	void Start () {
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/AttaqueFin");
		sheet.CreateAnimation("Fight", 50);
		sheet.AddFrame("Fight", 0, 0, 128, 128);
		sheet.AddFrame("Fight", 0, 128, 128, 128);
		sheet.AddFrame("Fight", 0, 256, 128, 128);
		sheet.AddFrame("Fight", 0, 384, 128, 128);
		sheet.AddFrame("Fight", 0, 512, 128, 128);
		sheet.AddFrame("Fight", 0, 640, 128, 128);
		sheet.AddFrame("Fight", 0, 768, 128, 128);
		sheet.AddFrame("Fight", 0, 896, 128, 128);
		//sheet.AddFrame("Fight", 0, 1024, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
		sheet.Render();
	}
}
