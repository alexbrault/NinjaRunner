using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class GlassPath : MonoBehaviour {
	public Transform Exit;
	
	void Start () {
		collider.isTrigger = true;
	}
	
	IEnumerator OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.SetActive(false);
			yield return new WaitForSeconds(1f);
			other.transform.position = Exit.position;
			other.gameObject.SetActive(true);
			
			Exit.particleSystem.Play();
		}
	}
}
