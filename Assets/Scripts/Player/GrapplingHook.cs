using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {
	
	bool grappled = false;
	Transform grappledTarget;
	
	float angle;
	float grapplingLength;
	
	int player;
	
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<NinjaController>().Player;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(grappled)
		{
			ComputeGrappled();
		}
		
		else if(InputEx.GetButtonDown(NinjaController.KeyNames[player, NinjaController.KeyID.Grappling]))
		{
			if(gameObject.GetComponent<NinjaController>().IsJumping())
			{
				Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 2);
				
				foreach(Collider collider in colliders)
				{
					if(collider.gameObject.tag == "GrapplingHook" && CanGrapple(collider))
					{
						Grapple(collider);
						break;
					}
				}
			}
		}
	}
	
	void ComputeGrappled()
	{
		if(InputEx.GetButtonDown(NinjaController.KeyNames[player, NinjaController.KeyID.Grappling]))
		{
			Ungrapple();
		}
		
		else
		{
			angle += 0.05f;
			
			Vector3 pos = transform.position;
			pos.x = Mathf.Cos(angle) * grapplingLength + grappledTarget.position.x;
			pos.y = -Mathf.Abs (Mathf.Sin(angle)) * grapplingLength + grappledTarget.position.y;
			
			transform.position = pos;
		}
	}
	
	void Grapple(Collider collider)
	{
		grappled = true;
		grappledTarget = collider.gameObject.transform;
		
		Debug.Log("Player : " + gameObject.transform.position);
		Debug.Log("Grappled : " + grappledTarget);
		
		GetComponent<NinjaController>().enabled = false;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.useGravity = false;
		gameObject.collider.enabled = false;
		
		angle = Vector3.Angle(-collider.transform.up, grappledTarget.position - transform.position);
		grapplingLength = (grappledTarget.position - transform.position).magnitude;
	}
	
	void Ungrapple()
	{
		grappled = false;
		GetComponent<NinjaController>().enabled = true;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.useGravity = true;
		gameObject.collider.enabled = true;
		
		angle += 0.05f;
			
		Vector3 pos = transform.position;
		pos.x = Mathf.Cos(angle) * grapplingLength + grappledTarget.position.x;
		
		if(pos.x > transform.position.x)
		{
			rigidbody.AddForce(new Vector3(1, 1, 0) * 250);
		}
		
		else
		{
			rigidbody.AddForce(new Vector3(-1, 1, 0) * 250);
		}
	}
	
	bool CanGrapple(Collider collider)
	{
		if(gameObject.rigidbody.velocity.x > 0 && gameObject.transform.position.x < collider.transform.position.x)
		{
			return true;
		}
		
		if(gameObject.rigidbody.velocity.x < 0 && collider.transform.position.x < gameObject.transform.position.x)
		{
			return true;
		}
		
		return false;
	}
}
