using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public int WalkingSpeed;
	
	private Transform[] patrolPoints;
	private int directionX;
	
	// Use this for initialization
	void Start () {
	
		Transform patrol = GameObject.Find("Patrol").transform;
		
		patrolPoints = new Transform[2];
		patrolPoints[0] = patrol.GetChild(0);
		patrolPoints[1] = patrol.GetChild(1);
		
		transform.position = patrol.position;
		transform.parent = patrol;
		
		if(patrolPoints[0].position.x < transform.position.x)
			directionX = -1;
		
		else
			directionX = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		rigidbody.velocity = new Vector3(directionX, 0, 0) * WalkingSpeed * 10;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "PatrolPoint")
		{
			rigidbody.velocity = new Vector3(0, 0, 0);
			StartCoroutine (Wait());
		}
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		
		directionX = -directionX;
	}
}
