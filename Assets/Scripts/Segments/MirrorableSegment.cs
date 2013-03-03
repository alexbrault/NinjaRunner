using UnityEngine;
using System.Collections;

public class MirrorableSegment : MonoBehaviour {
	public Transform Left;
	public Transform Right;
	
	private static Transform parent;
	
	public float SegmentWidth { get { return Left.GetComponent<Segment>().SegmentWidth; } }
	
	private void Start() {   
		if (parent == null) {
			parent = GameObject.Find("Globals").transform;
		}
		transform.parent = parent;
	}
	
	public void PlaceSegments(float absPosition) {
		Left.position = new Vector3(-absPosition - SegmentWidth / 2, 0, 0);
		Right.position = new Vector3(absPosition + SegmentWidth / 2, 0, 0);
	}
}
