using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SegmentLoader : MonoBehaviour {
	private List<Transform> segments;
	private System.Random random = new System.Random();
	private float nextSegmentX = 0;
	
	private static SegmentLoader _instance;
	public static SegmentLoader Instance {
		get { return _instance; }
	}
	
	public void SetSeed(int seed) {
		random = new System.Random(seed);
	}
	
	void Start () {
		var resources = from r in ResourcesEx.LoadAll<Transform>("MapSegments")
						where r.HasComponent<MirrorableSegment>()
						select r;
		segments = resources.ToList();
		if (_instance == null) {
			_instance = this;
		}
		
		AddSegmentToLevel();
		AddSegmentToLevel();
		AddSegmentToLevel();
		AddSegmentToLevel();
	}
	
	void OnDestroy() {
		if (_instance == this) {
			_instance = null;
		}
	}

	public float AddSegmentToLevel() {
		var segmentTemplate = GetRandomSegmentTemplate();
		var segmentWidth = segmentTemplate.GetComponent<MirrorableSegment>().SegmentWidth;
		Transform mirror = Instantiate(segmentTemplate) as Transform;
		mirror.GetComponent<MirrorableSegment>().PlaceSegments(nextSegmentX);
		nextSegmentX += segmentWidth;
		return segmentWidth;
	}

	private Vector3 NextSegmentLocation(float width) {
		var position = new Vector3(nextSegmentX, -10, 0);
		nextSegmentX += width;
		return position;
	}

	private Transform GetRandomSegmentTemplate()
	{
		return segments[random.Next(segments.Count)];
	}
}
