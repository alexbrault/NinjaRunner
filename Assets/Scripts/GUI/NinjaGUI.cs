using UnityEngine;
using System.Collections;

public class NinjaGUI : MonoBehaviour {
	public Texture2D BorderTexture;
	
	void OnGUI() {
		Rect border = new Rect(0, Screen.height/2 - BorderTexture.height/2, Screen.width, BorderTexture.height);
		Rect texCoords = new Rect(0, 0, Screen.width / BorderTexture.width, 1);
		GUI.DrawTextureWithTexCoords(border, BorderTexture, texCoords);
	}
}
