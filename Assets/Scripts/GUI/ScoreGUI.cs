using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public int noPlayer = 1;
	public Font textFont;
	
	private Camera cam;
	private GUIStyle textStyle;

	void Start ()
	{
		noPlayer = this.gameObject.GetComponent<NinjaController>().Player;
		
		cam = gameObject.transform.FindChild("Camera").gameObject.camera;
		
		textStyle = new GUIStyle();
		textStyle.font = textFont;
		textStyle.fontSize = 28;
		textStyle.normal.textColor = Color.red;
	}
	
 	void OnGUI ()
	{		
		GUI.Label( new Rect(20, Mathf.Abs(cam.pixelRect.y - cam.pixelHeight) + 10, 200, 50),
				"Player " + (noPlayer + 1) + " : " + ScoreManager.Instance.GetScore(noPlayer), textStyle);
	}
}
