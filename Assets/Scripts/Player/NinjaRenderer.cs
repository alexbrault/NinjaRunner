using UnityEngine;
using System.Collections;

public class NinjaRenderer : MonoBehaviour {
	Spritesheet sheet;
	// Use this for initialization
	void Start () {
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
		
		sheet.CreateAnimation("RunRight", 200);
		sheet.AddFrame("RunRight", 256, 256, 128, 128);
		sheet.AddFrame("RunRight", 256, 384, 128, 128);
		sheet.AddFrame("RunRight", 256, 512, 128, 128);
		sheet.AddFrame("RunRight", 256, 640, 128, 128);
		sheet.AddFrame("RunRight", 256, 768, 128, 128);
		sheet.AddFrame("RunRight", 256, 896, 128, 128);
		sheet.AddFrame("RunRight", 256, 768, 128, 128);
		sheet.AddFrame("RunRight", 256, 640, 128, 128);
		sheet.AddFrame("RunRight", 256, 512, 128, 128);
		sheet.AddFrame("RunRight", 256, 384, 128, 128);
		
		sheet.CreateAnimation("RunLeft", 200);
		sheet.AddFrame("RunLeft", 384, 256, 128, 128);
		sheet.AddFrame("RunLeft", 384, 384, 128, 128);
		sheet.AddFrame("RunLeft", 384, 512, 128, 128);
		sheet.AddFrame("RunLeft", 384, 640, 128, 128);
		sheet.AddFrame("RunLeft", 384, 768, 128, 128);
		sheet.AddFrame("RunLeft", 384, 896, 128, 128);
		sheet.AddFrame("RunLeft", 384, 768, 128, 128);
		sheet.AddFrame("RunLeft", 384, 640, 128, 128);
		sheet.AddFrame("RunLeft", 384, 512, 128, 128);
		sheet.AddFrame("RunLeft", 384, 384, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
		sheet.Render();
	}
	
	public void PlayAnimation(string anim)
	{
		sheet.SetCurrentAnimation(anim);
	}
}
