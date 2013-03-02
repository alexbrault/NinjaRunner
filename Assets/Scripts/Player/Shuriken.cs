using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		particleSystem.Play();
		Destroy(gameObject.rigidbody);
		gameObject.renderer.enabled = false;
		
		StartCoroutine(DestroyShuriken());
	}
	
	IEnumerator DestroyShuriken()
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(gameObject);
	}
}
