using UnityEngine;
using System.Collections;

public class BeginsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(VS ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator VS()
	{
		yield return new WaitForSeconds(2);		
		StartCoroutine(NextScene ());
	}
	
	IEnumerator NextScene()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel("Hotseat");
	}
}
