using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour {
	
	public Texture2D MenuTexture;
	public Font textFont;
	private float scrollTop = -100;
	
	private GUIStyle textStyle;
	
	void Start()
	{		
		textStyle = new GUIStyle();
		textStyle.font = textFont;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuTexture);
				
		textStyle.fontSize = 72;
		textStyle.normal.textColor = Color.cyan;
		GUI.Label (new Rect(180, 20 - scrollTop, 100, 60), "Credits", textStyle);
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(178, 18 - scrollTop, 100, 60), "Credits", textStyle);
		
		textStyle.fontSize = 64;
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 150 - scrollTop, 100, 50), "Alexandre Brault-Tremblay", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 230 - scrollTop, 100, 50), "Master of Keys", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 340 - scrollTop, 100, 50), "Frederick Imbeault", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 420 - scrollTop, 100, 50), "Programmer", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 530 - scrollTop, 100, 50), "Frederic Boucher", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 610 - scrollTop, 100, 50), "Programmer", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 720 - scrollTop, 100, 50), "Tommy Sirois", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 800 - scrollTop, 100, 50), "Artist", textStyle);
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Menu");
		}
		
		scrollTop += Time.deltaTime * 75;
		
		if (scrollTop >= 900) {
			Application.LoadLevel("Menu");			
		}
	}
}
