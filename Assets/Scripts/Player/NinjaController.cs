using UnityEngine;
using System.Collections;

public class NinjaController : MonoBehaviour {
	
	public int JumpForce;
	public int RunSpeed;
	public int MaxSpeed;
	
	public bool enabled;
	
	private bool canJump = true;
	
	// Use this for initialization
	void Start () {
		
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach(GameObject go in players)
		{
			if(gameObject != go)
				Physics.IgnoreCollision(gameObject.collider, go.collider, true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(enabled)
		{
			Run ();
			
			if(Input.GetButtonDown("Jump"))
				Jump();
		}
	}
	
	void Run()
	{
		if(Input.GetKey(KeyCode.A))
		{
			gameObject.rigidbody.AddForce(Vector3.left * RunSpeed * 100);
		}
		
		else if (Input.GetKey(KeyCode.D))
		{
			gameObject.rigidbody.AddForce(Vector3.right * RunSpeed * 100);
		}
		
		if(gameObject.rigidbody.velocity.magnitude > MaxSpeed)
		{
			Vector3 newVel = new Vector3(0, 0, 0);
			float velY = gameObject.rigidbody.velocity.y;
			
			newVel = gameObject.rigidbody.velocity.normalized * MaxSpeed;
			newVel.y = velY;
			
			gameObject.rigidbody.velocity = newVel;
		}
	}
	
	void Jump()
	{
		if(canJump)
		{
			gameObject.rigidbody.AddForce(Vector3.up * JumpForce * 100);
			canJump = false;
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Collider contact = collision.collider;

		if(contact.gameObject.tag == "Floor")
			canJump = true;
	}
}