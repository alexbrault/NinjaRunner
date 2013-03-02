using UnityEngine;
using System.Collections;

public class FinalBattle : MonoBehaviour {
	
	public GameObject winner;
	public GameObject looser;
	public int maxDeltaScore;
	public int deltaScore;
	
	private GameObject swordFight;
	
	// Use this for initialization
	void Start () {
		swordFight = gameObject.transform.FindChild("SwordFight").gameObject;
		
		swordFight.AddComponent<SwordFight>();
		swordFight.renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Battle()
	{
		float seconds = Mathf.Log(maxDeltaScore - deltaScore);
		
		if(seconds < 0)
			seconds = 0;
		
		if(seconds > 5)
			seconds = 5;
		
		Debug.Log (seconds);
		
		yield return new WaitForSeconds(seconds);
		
		
		Debug.Break();
	}
}
