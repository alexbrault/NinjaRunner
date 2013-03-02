using UnityEngine;
using System.Collections;

public class NinjaRenderer : MonoBehaviour {
	Spritesheet sheet;
	// Use this for initialization
	void Start () {
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/ninja");
		sheet.CreateAnimation("Idle", 200);
		sheet.AddFrame("Idle", 0, 0, 128, 128);
		sheet.AddFrame("Idle", 128, 0, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
		sheet.Render();
	}
}
