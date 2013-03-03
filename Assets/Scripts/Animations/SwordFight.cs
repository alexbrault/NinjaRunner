using UnityEngine;
using System.Collections;

public class SwordFight : MonoBehaviour {

	Spritesheet sheet;
	
	bool killCompleted = false;
	
	// Use this for initialization
	void Start () {
		sheet = new Spritesheet(gameObject);
		sheet.Load("Sprites/AttaqueFinalDerniereVersion");
		
		sheet.CreateAnimation("Fight", 20);
		sheet.AddFrame("Fight", 0, 0, 128, 128);
		sheet.AddFrame("Fight", 0, 128, 128, 128);
		sheet.AddFrame("Fight", 0, 256, 128, 128);
		sheet.AddFrame("Fight", 0, 384, 128, 128);
		sheet.AddFrame("Fight", 0, 512, 128, 128);
		sheet.AddFrame("Fight", 0, 640, 128, 128);
		sheet.AddFrame("Fight", 0, 768, 128, 128);
		sheet.AddFrame("Fight", 0, 896, 128, 128);
		sheet.AddFrame("Fight", 0, 1024, 128, 128);
		sheet.AddFrame("Fight", 0, 1152, 128, 128);
		sheet.AddFrame("Fight", 0, 1280, 128, 128);
		sheet.AddFrame("Fight", 0, 1408, 128, 128);
		
		sheet.CreateAnimation("Kill", 15);
		sheet.AddFrame("Kill", 128, 0, 128, 128);
		sheet.AddFrame("Kill", 128, 128, 128, 128);
		sheet.AddFrame("Kill", 128, 256, 128, 128);
		sheet.AddFrame("Kill", 128, 384, 128, 128);
		sheet.AddFrame("Kill", 128, 512, 128, 128);
		sheet.AddFrame("Kill", 128, 640, 128, 128);
		sheet.AddFrame("Kill", 128, 768, 128, 128);
		sheet.AddFrame("Kill", 128, 896, 128, 128);
		sheet.AddFrame("Kill", 128, 1024, 128, 128);
		sheet.AddFrame("Kill", 128, 1152, 128, 128);
		sheet.AddFrame("Kill", 128, 1280, 128, 128);
		sheet.AddFrame("Kill", 128, 1408, 128, 128);
		sheet.AddFrame("Kill", 128, 1536, 128, 128);
		sheet.AddFrame("Kill", 128, 1664, 128, 128);
		sheet.AddFrame("Kill", 128, 1792, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
		if(!killCompleted)
			sheet.Render();
	}
	
	void KillCompleted()
	{
		killCompleted = true;
		renderer.enabled = false;
	}
	
	public void PlayKillAnimation()
	{
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.FINAL_STR);		
		sheet.SetCurrentAnimation("Kill");
		sheet.activeAnimation.AnimationCompleted += KillCompleted;
	}
}
