using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	public Texture2D MenuTexture;
	public Texture2D CursorTexture;
	public Font textFont;
	
	private Rect[] cursorPositions = new Rect[4];
	private Rect cursorTextCoord;
	private int cursorPosIndex = 0;
	
	private GUIStyle textStyle;
	
	void Start()
	{
		cursorPositions[0] = new Rect(60, 115, CursorTexture.width, CursorTexture.height);
		cursorPositions[1] = new Rect(60, 195, CursorTexture.width, CursorTexture.height);
		cursorPositions[2] = new Rect(60, 275, CursorTexture.width, CursorTexture.height);
		cursorPositions[3] = new Rect(60, 355, CursorTexture.width, CursorTexture.height);
		
		textStyle = new GUIStyle();
		textStyle.font = textFont;
		textStyle.fontSize = 64;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuTexture);
		
		textStyle.normal.textColor = Color.gray;
		GUI.Label (new Rect(400, 150, 100, 50), "Time Attack", textStyle);
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(400, 230, 100, 50), "Hotseat", textStyle);
		textStyle.normal.textColor = Color.gray;
		GUI.Label (new Rect(400, 310, 100, 50), "Network", textStyle);
		textStyle.normal.textColor = Color.black;
		GUI.Label (new Rect(400, 390, 100, 50), "Credits", textStyle);
		
		GUI.DrawTexture(cursorPositions[cursorPosIndex], CursorTexture);
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.W) && cursorPosIndex > 0)
		{
			cursorPosIndex--;
		}
		
		if(Input.GetKeyDown(KeyCode.S) && cursorPosIndex < 3)
		{
			cursorPosIndex++;
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			switch(cursorPosIndex)
			{
			case 0:
				break;
				
			case 1:
				Application.LoadLevel("Begins");
				break;
				
			case 2:
				break;
				
			case 3:
				break;
			}
		}
	}
}
