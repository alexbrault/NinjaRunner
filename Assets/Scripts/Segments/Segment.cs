using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {
	public Vector3 SafeSpawn { get; private set; }
	public float SegmentWidth = 10.0f;
	
	private static Transform parent;
	
	private void Start() {
		if (parent == null) {
			parent = GameObject.Find("Globals").transform;
		}
		
		transform.parent = parent;
		SafeSpawn = GetComponentInChildren<SafeSpawn>().transform.position;
		
		BoxCollider bc = gameObject.AddComponent<BoxCollider>();
		bc.isTrigger = true;
		bc.size = new Vector3(SegmentWidth, 100, 1);
		bc.center = new Vector3(SegmentWidth / 2, 0, 0);
	}
	
	private void OnTriggerEnter(Collider other) {
		other.SendMessage("EnteredSegment", this, SendMessageOptions.DontRequireReceiver);
	}
}
