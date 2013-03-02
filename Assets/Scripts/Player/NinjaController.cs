using UnityEngine;
using System.Collections;

public class NinjaController : MonoBehaviour {
	
	public int JumpForce;
	public int WallJumpForce;
	public int RunSpeed;
	public int MaxSpeed;
	
	public bool enabled;
	
	private bool canJump = true;
	private bool canMove = true;
	private bool onWall = false;
	
	private int nextWallJumpX = -1;
	
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
	
		if(enabled && canMove)
		{
			Run ();
			
			if(canJump && Input.GetButtonDown("Jump"))
				Jump();
		}
		
		if(onWall && Input.GetButtonDown("Jump"))
		{
			WallJump();
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
		gameObject.rigidbody.AddForce(Vector3.up * JumpForce * 100);
		canJump = false;
	}
	
	void WallJump()
	{
		gameObject.rigidbody.AddForce(new Vector3(nextWallJumpX, 1, 0) * WallJumpForce * 50);
		canJump = false;
		onWall = false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Collider contact = collision.collider;

		if(contact.gameObject.tag == "Floor")
		{
			canJump = true;
			canMove = true;
		}
		
		if(contact.gameObject.tag == "Wall")
		{
			canMove = false;
			onWall = true;
			gameObject.rigidbody.velocity = new Vector3(0, 0, 0);
			gameObject.rigidbody.useGravity = false;
			
			if(contact.gameObject.transform.position.x - gameObject.transform.position.x > 0)
				nextWallJumpX = -1;
			
			else
				nextWallJumpX = 1;
			
			StartCoroutine(FallOffWall());
		}
	}
	
	IEnumerator FallOffWall()
	{
	    yield return new WaitForSeconds(1);
		gameObject.rigidbody.useGravity = true;
		onWall = false;
	}
}