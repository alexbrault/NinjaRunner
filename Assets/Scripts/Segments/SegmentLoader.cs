using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SegmentLoader : MonoBehaviour {
	public Transform boundary;
	private List<Transform> segments;
	private System.Random random = new System.Random();
	private float nextSegmentX = 0;
	
	private const int numSegments = 1;
	
	private static SegmentLoader _instance;
	public static SegmentLoader Instance {
		get { return _instance; }
	}
	
	public void SetSeed(int seed) {
		random = new System.Random(seed);
	}
	
	IEnumerator Start () {
		var resources = from r in ResourcesEx.LoadAll<Transform>("MapSegments")
						where r.HasComponent<MirrorableSegment>()
						select r;
		segments = resources.ToList();
		if (_instance == null) {
			_instance = this;
		}
		
		for (int i = 0; i < numSegments; ++i) {
			AddSegmentToLevel();
		}
		AddBoundary();
		
		var players = (from p in GameObject.FindGameObjectsWithTag("Player")
					  orderby p.GetComponent<NinjaController>().Player
					  select p).ToList();
		var boundaryObject = boundary.GetComponent<MirrorableSegment>();
		players[0].SendMessage("EnteredSegment", boundaryObject.Left.GetComponent<Segment>());
		players[0].SendMessage("Respawn");
		players[1].SendMessage("EnteredSegment", boundaryObject.Right.GetComponent<Segment>());
		players[1].SendMessage("Respawn");
		yield break;
	}
	
	void OnDestroy() {
		if (_instance == this) {
			_instance = null;
		}
	}

	public void AddSegmentToLevel() {
		var segmentTemplate = GetRandomSegmentTemplate();
		var segmentWidth = segmentTemplate.GetComponent<MirrorableSegment>().SegmentWidth;
		Transform mirror = Instantiate(segmentTemplate) as Transform;
		mirror.GetComponent<MirrorableSegment>().PlaceSegments(nextSegmentX);
		nextSegmentX += segmentWidth;
	}
	
	public void AddBoundary() {
		var segmentTemplate = boundary;
		var segmentWidth = segmentTemplate.GetComponent<MirrorableSegment>().SegmentWidth;
		Transform mirror = Instantiate(segmentTemplate) as Transform;
		mirror.GetComponent<MirrorableSegment>().PlaceSegments(nextSegmentX);
		nextSegmentX += segmentWidth;
		boundary = mirror;
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
