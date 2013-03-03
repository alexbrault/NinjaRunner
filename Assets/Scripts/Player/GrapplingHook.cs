using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {
	
	Transform grappledTarget = null;
	int xForce = 0;
	bool grappled = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if(grappledTarget != null)
		{
			ComputeGrappled();
		}
		
		else if(Input.GetKeyDown(KeyCode.G))
		{
			if(gameObject.GetComponent<NinjaController>().IsJumping())
			{
				Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 30);
				
				foreach(Collider collider in colliders)
				{
					Grapple(collider);
				}
			}
		}
	}
	
	void ComputeGrappled()
	{
		float speed = xForce * Time.deltaTime;
		Debug.Log (speed);
		
		if(xForce > 0)
		{
			speed -= 0.5f;
			
			if(speed < 0)
				xForce = -100;
			
			gameObject.transform.RotateAround(grappledTarget.position, new Vector3(0, 0, -1), -speed);
		}
		
		else
		{
			speed += 0.5f;
			
			if(speed > 0)
				xForce = 100;
			
			gameObject.transform.RotateAround(grappledTarget.position, new Vector3(0, 0, -1), speed);
		}
		
		
		//float angle = Vector3.Angle (gameObject.transform.up, gameObject.transform.up - (grappledTarget.transform.position - gameObject.transform.position));		
		//gameObject.transform.RotateAroundLocal(new Vector3(0, 0, -1), angle);
	}
	
	void Grapple(Collider collider)
	{
		if(collider.gameObject.tag == "GrapplingHook")
		{
			if(gameObject.rigidbody.velocity.x > 0 && gameObject.transform.position.x < collider.transform.position.x)
			{
				gameObject.GetComponent<NinjaController>().enabled = false;
				gameObject.rigidbody.useGravity = false;
				gameObject.rigidbody.velocity = Vector3.zero;
				
				float angle = Vector3.Angle (Vector3.up, collider.transform.position - gameObject.transform.position);
				
				gameObject.transform.RotateAroundLocal(new Vector3(0, 0, -1), angle);
				grappledTarget = collider.transform;
				xForce = 100;
			}
			
			else if(gameObject.rigidbody.velocity.x < 0 && collider.transform.position.x < gameObject.transform.position.x)
			{
				gameObject.GetComponent<NinjaController>().enabled = false;
				gameObject.rigidbody.useGravity = false;
				gameObject.rigidbody.velocity = Vector3.zero;
				
				float angle = Vector3.Angle (Vector3.up, collider.transform.position - gameObject.transform.position);
				
				gameObject.transform.RotateAroundLocal(new Vector3(0, 0, -1), angle);
				grappledTarget = collider.transform;
				xForce = -100;
			}
		}
	}
}
