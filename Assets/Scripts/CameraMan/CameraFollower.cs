using UnityEngine;
using System.Collections;

public class CameraFollower : CollisionListener {
	public Transform target;
	private Vector3 delta;
	
	private float lastY;
	private float currentY;
	private float targetY;
	
	private float timer = 0;
	
	// Use this for initialization
	void Awake () {
		delta = transform.position - target.position;
		
		lastY = targetY = currentY = transform.position.y + delta.y;
		Debug.Log (transform.position.y);
	}
	
	void Start()
	{
		//target.gameObject.GetComponent<NinjaController>().AddListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		//timer += Time.deltaTime;
		//transform.position = new Vector3(target.position.x + delta.x, Mathf.Lerp (lastY, targetY, timer), target.position.z + delta.z);
	
		transform.position = new Vector3(target.position.x + delta.x, target.position.y + delta.y, target.position.z + delta.z);
		/*
		float cameraTop = transform.position.y + camera.orthographicSize;
		
		if(cameraTop - target.position.y < camera.orthographicSize)
			pos.y += 0.02f;
		
		pos.y = Mathf.Lerp (transform.position.y, target.position.y + delta.y, timer);
		
		transform.position = pos;*/
	}
	
	public override void Notify(Collider coll1, Collider coll2)
	{
		if(coll2.gameObject.tag == "Floor")
		{
			timer = 0;
			lastY = transform.position.y;
			targetY = coll2.transform.position.y + delta.y + 0.5f;
			Debug.Log (coll1.transform.position.y);
		}
	}
}
