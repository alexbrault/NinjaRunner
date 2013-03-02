using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SegmentLoader : MonoBehaviour {
	private List<Transform> segments;
	private System.Random random = new System.Random();
	private float nextSegmentX = 0;
	public const int SegmentWidth = 10;
	
	void Start () {
		var resources = from r in ResourcesEx.LoadAll<Transform>("MapSegments")
						where r.HasComponent<Segment>()
						select r;
		segments = resources.ToList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var newSegment = Instantiate(GetRandomSegment(), new Vector3(nextSegmentX, 0, 0), Quaternion.identity) as Transform;
			nextSegmentX += newSegment.GetComponent<Segment>().SegmentWidth;
		}
	}

	private Transform GetRandomSegment()
	{
		return segments[random.Next(segments.Count)];
	}
}
