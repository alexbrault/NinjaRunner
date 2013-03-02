using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {

	private bool rotate = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate)
			transform.RotateAroundLocal(new Vector3(0, 0, 1), 50);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		particleSystem.Play();
		Destroy(gameObject.rigidbody);
		gameObject.renderer.enabled = false;
		
		rotate = false;
		StartCoroutine(DestroyShuriken());
	}
	
	IEnumerator DestroyShuriken()
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(gameObject);
	}
}
