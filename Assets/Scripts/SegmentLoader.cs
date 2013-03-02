using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SegmentLoader : MonoBehaviour {
	private List<Transform> segments;
	private System.Random random = new System.Random();
	private int nextSegmentX = 0;
	public const int SegmentWidth = 10;
	
	void Start () {
		segments = ResourcesEx.LoadAll<Transform>("MapSegments").ToList();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Instantiate(GetRandomSegment(), new Vector3(nextSegmentX, 0, 0), Quaternion.identity);
			nextSegmentX += SegmentWidth;
		}
	}

	private Transform GetRandomSegment()
	{
		return segments[random.Next(segments.Count)];
	}
}
