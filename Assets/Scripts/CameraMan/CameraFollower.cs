using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	public Transform target;
	private Vector3 delta;
	// Use this for initialization
	void Awake () {
		delta = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + delta;
	}
}
