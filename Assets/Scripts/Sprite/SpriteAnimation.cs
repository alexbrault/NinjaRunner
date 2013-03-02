using UnityEngine;
using System.Collections.Generic;

public class SpriteAnimation {
	private List<AnimationFrame> frames = new List<AnimationFrame>();
	
	public int FrameRate { get; private set; }
	
	private float rawFrame = 0;
	private int currentFrameIndex = 0;
	
	public delegate void AnimationCompletedDelegate();

	public event AnimationCompletedDelegate AnimationCompleted;
	
	public SpriteAnimation(int framerate) {
		FrameRate = framerate;
	}
	
	public void Add(AnimationFrame frame) {
		frames.Add(frame);
	}
	
	public void Reset() {
		rawFrame = 0;
		currentFrameIndex = 0;
	}
	
	public AnimationFrame CurrentFrame {
		get {
			rawFrame += Time.deltaTime / Time.timeScale * 1000;
			
			if (rawFrame >= FrameRate) {
				rawFrame -= FrameRate;
				currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
				if (currentFrameIndex == 0) {
					AnimationCompletedDelegate d = AnimationCompleted;
					if (d != null) {
						d();
					}
				}
			}
			
			return frames[currentFrameIndex];
		}
	}
}
