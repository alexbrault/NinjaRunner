using UnityEngine;
using System.Collections;

public class CameraFollower : CollisionListener {
	public Transform target;
	private Vector3 delta;
	
	private float currentY;
	
	// Use this for initialization
	void Awake () {
		delta = transform.position - target.position;
		
		currentY = transform.position.y + delta.y;
		Debug.Log (transform.position.y);
	}
	
	void Start()
	{
		target.gameObject.GetComponent<NinjaController>().AddListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x + delta.x, currentY, target.position.z + delta.z);
	}
	
	public override void Notify(Collider coll1, Collider coll2)
	{
		if(coll2.gameObject.tag == "Floor")
		{
			//currentY = transform.position.y + delta.y;
			Debug.Log (transform.position.y);
		}
	}
}
