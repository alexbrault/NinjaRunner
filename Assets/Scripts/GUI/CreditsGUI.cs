using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour {
	
	public Texture2D MenuTexture;
	public Font textFont;
	
	private GUIStyle textStyle;
	
	void Start()
	{		
		textStyle = new GUIStyle();
		textStyle.font = textFont;
		textStyle.fontSize = 64;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuTexture);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 150, 100, 50), "Alexandre Brault-Tremblay", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 230, 100, 50), "Programmeur", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 340, 100, 50), "Frederick Imbeault", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 420, 100, 50), "Programmeur", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 530, 100, 50), "Frederic Boucher", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 610, 100, 50), "Programmeur", textStyle);
		
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(200, 720, 100, 50), "Tommy Sirois", textStyle);
		textStyle.normal.textColor = new Color(0.2f, 0.2f, 0.2f, 1);
		GUI.Label (new Rect(300, 800, 100, 50), "Artiste", textStyle);
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Menu");
		}
	}
}
