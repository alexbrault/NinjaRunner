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
		
		
		
		sheet.CreateAnimation("OnWallRight", 200);
		sheet.AddFrame("OnWallRight", 512, 384, 128, 128);
		
		sheet.CreateAnimation("OnWallLeft", 200);
		sheet.AddFrame("OnWallRight", 640, 384, 128, 128);
		
		sheet.CreateAnimation("WallJumpLeft", 200);
		sheet.AddFrame("WallJumpLeft", 512, 512, 128, 128);
		sheet.AddFrame("WallJumpLeft", 512, 640, 128, 128);
		sheet.AddFrame("WallJumpLeft", 512, 768, 128, 128);
		sheet.AddFrame("WallJumpLeft", 512, 896, 128, 128);
		
		sheet.CreateAnimation("WallJumpRight", 200);
		sheet.AddFrame("WallJumpRight", 640, 512, 128, 128);
		sheet.AddFrame("WallJumpRight", 640, 640, 128, 128);
		sheet.AddFrame("WallJumpRight", 640, 768, 128, 128);
		sheet.AddFrame("WallJumpRight", 640, 896, 128, 128);
		
		
		
		sheet.CreateAnimation("ThrowShurikenRight", 50);
		sheet.AddFrame("ThrowShurikenRight", 768, 0, 128, 128);
		sheet.AddFrame("ThrowShurikenRight", 768, 128, 128, 128);
		sheet.AddFrame("ThrowShurikenRight", 768, 256, 128, 128);
		sheet.AddFrame("ThrowShurikenRight", 768, 384, 128, 128);
		
		sheet.CreateAnimation("ThrowShurikenLeft", 50);
		sheet.AddFrame("ThrowShurikenLeft", 896, 0, 128, 128);
		sheet.AddFrame("ThrowShurikenLeft", 896, 128, 128, 128);
		sheet.AddFrame("ThrowShurikenLeft", 896, 256, 128, 128);
		sheet.AddFrame("ThrowShurikenLeft", 896, 384, 128, 128);
		
		
		
		sheet.CreateAnimation("BattleRight", 200);
		sheet.AddFrame("BattleRight", 1024, 0, 128, 128);
		sheet.AddFrame("BattleRight", 1024, 128, 128, 128);
		sheet.AddFrame("BattleRight", 1024, 256, 128, 128);
		sheet.AddFrame("BattleRight", 1024, 384, 128, 128);
		sheet.AddFrame("BattleRight", 1024, 512, 128, 128);
		
		sheet.CreateAnimation("BattleLeft", 200);
		sheet.AddFrame("BattleLeft", 1280, 0, 128, 128);
		sheet.AddFrame("BattleLeft", 1280, 128, 128, 128);
		sheet.AddFrame("BattleLeft", 1280, 256, 128, 128);
		sheet.AddFrame("BattleLeft", 1280, 384, 128, 128);
		sheet.AddFrame("BattleLeft", 1280, 512, 128, 128);
		
		
		
		sheet.CreateAnimation("DashRight", 200);
		sheet.AddFrame("DashRight", 1408, 0, 128, 128);
		sheet.AddFrame("DashRight", 1408, 128, 128, 128);
		sheet.AddFrame("DashRight", 1408, 256, 128, 128);
		sheet.AddFrame("DashRight", 1408, 384, 128, 128);
		sheet.AddFrame("DashRight", 1408, 512, 128, 128);
		
		sheet.CreateAnimation("DashLeft", 200);
		sheet.AddFrame("DashLeft", 1536, 0, 128, 128);
		sheet.AddFrame("DashLeft", 1536, 128, 128, 128);
		sheet.AddFrame("DashLeft", 1536, 256, 128, 128);
		sheet.AddFrame("DashLeft", 1536, 384, 128, 128);
		sheet.AddFrame("DashLeft", 1536, 512, 128, 128);
	}
	
	// Update is called once per frame
	void Update () {
		sheet.Render();
	}
	
	public void PlayAnimation(string anim)
	{
		sheet.SetCurrentAnimation(anim);
	}
	
	public void AddActiveAnimationDelegate(SpriteAnimation.AnimationCompletedDelegate del)
	{
		sheet.activeAnimation.AnimationCompleted += del;
	}
	
	public void RemoveActiveAnimationDelegate(SpriteAnimation.AnimationCompletedDelegate del)
	{
		sheet.activeAnimation.AnimationCompleted -= del;
	}
}
