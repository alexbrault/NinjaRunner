using UnityEngine;
using System.Collections.Generic;

public class Spritesheet {
	GameObject gameObject;
	Texture2D spritesheet = null;
	
	Dictionary<string, SpriteAnimation> animations = new Dictionary<string, SpriteAnimation>();
	public SpriteAnimation activeAnimation = null;
	
	public Spritesheet(GameObject o) {
		gameObject = o;
	}
	
	public void Render() {
		gameObject.renderer.material.mainTexture = activeAnimation.CurrentFrame.GetTexture(spritesheet);
	}
	
	public bool Load(string sprite) {
		spritesheet = ResourcesEx.Load<Texture2D>(sprite);
		
		if (spritesheet == null) {
			return false;
		}
		
		gameObject.renderer.material.shader = Shader.Find("Unlit/Transparent");
		return true;
	}
	
	public void Reset() {
		foreach (var anim in animations.Values) {
			anim.Reset();
		}
	}
	
	public void CreateAnimation(string name, int framerate) {
		var animation = new SpriteAnimation(framerate);
		animations.Add(name, animation);
		if (animations.Count == 1) {
			SetCurrentAnimation(name);
		}
	}
	
	public bool AddFrame(string animationName, int x, int y, int width, int height) {
		if (animations.ContainsKey(animationName)) {
			var frame = new AnimationFrame(x, y, width, height);
			animations[animationName].Add(frame);
				
				return true;
		}
				
		return false;
	}
	
	public void SetCurrentAnimation(string animation) {
		if (animations.ContainsKey(animation)) {
			activeAnimation = animations[animation];
		}
	}
	
	public SpriteAnimation this[string animation] {
		get {
			if (animations.ContainsKey(animation)) {
				return animations[animation];
			}
			
			return null;
		}
	}
}
