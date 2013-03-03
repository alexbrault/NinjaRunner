using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public int noPlayer = 1;
	public Font textFont;
	public Color playerScoreColor;
	
	private Camera cam;
	private GUIStyle textStyle;

	void Start ()
	{
		noPlayer = this.gameObject.GetComponent<NinjaController>().Player;
		
		//cam = gameObject.transform.FindChild("Camera").gameObject.camera;
		
		textStyle = new GUIStyle();
		textStyle.font = textFont;
		textStyle.fontSize = 28;
		
		if(noPlayer == 0)
			textStyle.normal.textColor = Color.red;
		else
			textStyle.normal.textColor = Color.green;
			//textStyle.normal.textColor = playerScoreColor;
	}
	
 	void OnGUI ()
	{		
		GUI.Label( new Rect(20, 10 + noPlayer*(Screen.height/2), 200, 50),
				"Player " + (noPlayer + 1) + " : " + ScoreManager.Instance.GetScore(noPlayer), textStyle);
	}
}
