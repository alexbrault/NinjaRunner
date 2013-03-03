using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	
	public IDictionary dictioSound;
	
	public AudioClip[] audiClipSoundArray;
	public enum soundEnum  {SHIRUKEN = 0, DRAW_SWORD, SWORD_FIGHT, FINAL_STR};
	
	private static AudioManager _instance;
	public static AudioManager Instance 
	{
		get { return _instance; }
	}
	
	private void Start() 
	{
		if (_instance == null) 
		{
			_instance = this;
			
			int audioClipSoundArraySize = audiClipSoundArray.Length;
			
			dictioSound = new Dictionary<soundEnum, AudioClip>();
			
			for(int i = 0; i < audioClipSoundArraySize; i++)
			{
				dictioSound.Add((soundEnum)(i), audiClipSoundArray[i]);
			}
		}
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
		gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)(dictioSound[mySound]), 1.0f);
	}
}
