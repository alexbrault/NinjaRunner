using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour {
	public GUIStyle style;
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var players = GameObject.FindGameObjectsWithTag("Player");
			foreach (var p in players) {
				Destroy(p);
			}
			
			Debug.Break();
			
			Application.LoadLevel("Menu");
		}
	}
	
	void OnGUI() {
		style.fontSize = 20;
		GUI.Label(new Rect(0, Screen.height - 20, Screen.width, 20), "Press [SPACE] to continue", style);
	}
}
