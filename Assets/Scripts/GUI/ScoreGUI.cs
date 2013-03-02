using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public int noPlayer = 1;
	
	// Use this for initialization
	void Start () 
	{
		noPlayer = this.gameObject.GetComponent<NinjaController>().Player;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
 	void OnGUI ()
	{
		GUI.Label( new Rect(0, gameObject.transform.FindChild("Camera").gameObject.camera.pixelRect.y, 200, 50),
				"Player " + (noPlayer+1) + " : " + ScoreManager.Instance.GetScore(noPlayer+1));
				
		//GUI.Label(new Rect(0,Screen.height/noPlayer + 20, 200, 50), "Time : " + playerTime);
	}
}
