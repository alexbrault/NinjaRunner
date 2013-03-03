using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	public AudioClip[] audiClipSoundArray;
	public enum soundEnum  {SHIRUKEN = 0, DRAW_SWORD, SWORD_FIGHT, FINAL_STR, BONG};
	
	private static SoundManager _instance;
	public static SoundManager Instance 
	{
		get { return _instance; }
	}
	
	private void Start() 
	{
		if (_instance == null) 
		{
			_instance = this;
		}
		
		GameObject.DontDestroyOnLoad(transform);
	}
	
	private void Destroy() 
	{
		if (_instance == this) 
		{
			_instance = null;
		}
	}
	
	public void PlaySound(soundEnum mySound)
	{
		GetComponent<AudioSource>().PlayOneShot(audiClipSoundArray[(int)mySound], 1.0f);
	}
}
