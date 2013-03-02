using UnityEngine;
using System.Linq;
using System.Collections;

public class Segment : MonoBehaviour {
	public Vector3 SafeSpawn { get; private set; }
	public float SegmentWidth = 10.0f;
	public static Transform[] patrollers;
	
	private static Transform parent;
	
	private void Start() {
		InitStatics();
		InitParent();
				
		FindSafeSpawn();
		
		var patrolPaths = GetComponentsInChildren<PatrolPath>();
		foreach (var path in patrolPaths) {
			var patroller = Instantiate(patrollers[0], path.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))) as Transform;
			patroller.GetComponent<Patrol>().StartPatrol(path.transform);
		}
		
		AddTrigger();
	}
	
	private void OnTriggerEnter(Collider other) {
		other.SendMessage("EnteredSegment", this, SendMessageOptions.DontRequireReceiver);
	}
	
	private void InitStatics() {
		if (parent == null) {
			parent = GameObject.Find("Globals").transform;
		}
		if (patrollers == null) {
			var resources = from r in ResourcesEx.LoadAll<Transform>("Patrollers")
							where r.HasComponent<Patrol>()
							select r;
			patrollers = resources.ToArray();
		}
	}

	void InitParent() {
		transform.parent = parent;
	}

	void FindSafeSpawn() {
		SafeSpawn = GetComponentInChildren<SafeSpawn>().transform.position;
	}

	void AddTrigger() {
		BoxCollider bc = gameObject.AddComponent<BoxCollider>();
		bc.isTrigger = true;
		bc.size = new Vector3(SegmentWidth, 100, 1);
		bc.center = new Vector3(SegmentWidth / 2, 0, 0);
	}
}
