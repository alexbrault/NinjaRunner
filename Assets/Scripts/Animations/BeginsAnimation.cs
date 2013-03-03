using UnityEngine;
using System.Collections;

public class BeginsAnimation : MonoBehaviour {

	Spritesheet sheet;
	bool started = false;
	
	// Use this for initialization
	void Start () {
		sheet = new Spritesheet(gameObject);
		
		sheet.Load("Sprites/menuVs");
		
		sheet.CreateAnimation("Anim", 20);
		sheet.AddFrame("Anim", 0, 0, 512, 512);
		sheet.AddFrame("Anim", 0, 512, 512, 512);
		sheet.AddFrame("Anim", 0, 1024, 512, 512);
		sheet.AddFrame("Anim", 0, 1536, 512, 512);
		
		sheet.AddFrame("Anim", 512, 0, 512, 512);
		sheet.AddFrame("Anim", 512, 512, 512, 512);
		sheet.AddFrame("Anim", 512, 1024, 512, 512);
		sheet.AddFrame("Anim", 512, 1536, 512, 512);
		
		sheet.AddFrame("Anim", 1024, 0, 512, 512);
		sheet.AddFrame("Anim", 1024, 512, 512, 512);
		sheet.AddFrame("Anim", 1024, 1024, 512, 512);
		sheet.AddFrame("Anim", 1024, 1536, 512, 512);
		
		sheet.AddFrame("Anim", 1536, 0, 512, 512);
		sheet.AddFrame("Anim", 1536, 512, 512, 512);
		sheet.AddFrame("Anim", 1536, 1024, 512, 512);
		sheet.AddFrame("Anim", 1536, 1536, 512, 512);
		
		
		sheet.CreateAnimation("Idle", 50);
		sheet.AddFrame("Idle", 1536, 1536, 512, 512);
		
		sheet.activeAnimation.AnimationCompleted += AnimEnd;
		
		StartCoroutine(Sounds ());
	}
	
	// Update is called once per frame
	void Update () {
		if(started)
			sheet.Render();
	}
	
	public void AnimEnd()
	{
		sheet.SetCurrentAnimation("Idle");
	}
	
	IEnumerator Sounds()
	{
		yield return new WaitForSeconds(2);
		started = true;
		GameObject.Find ("anim").renderer.enabled = true;
		
		yield return new WaitForSeconds(0.1f);
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.DRAW_SWORD);
	}
}
