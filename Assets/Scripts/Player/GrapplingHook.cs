using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.G))
		{
			if(gameObject.GetComponent<NinjaController>().IsJumping())
			{
				Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 30);
				
				foreach(Collider collider in colliders)
				{
					if(collider.gameObject.tag == "GrapplingHook")
					{
						if(gameObject.rigidbody.velocity.x > 0 && gameObject.transform.position.x < collider.transform.position.x)
						{
							//gameObject.GetComponent<NinjaController>().enabled = false;
							float angle = Vector3.Angle (Vector3.up, collider.transform.position - gameObject.transform.position);
							
							gameObject.transform.RotateAroundLocal(new Vector3(0, 0, -1), angle);
							Debug.Log ("Grapples");
							break;
						}
						
						else if(gameObject.rigidbody.velocity.x < 0 && collider.transform.position.x < gameObject.transform.position.x)
						{
							float angle = Vector3.Angle (Vector3.up, collider.transform.position - gameObject.transform.position);
							
							gameObject.transform.RotateAroundLocal(new Vector3(0, 0, -1), angle);
							
							Debug.Log ("Grapples");
							break; 
						}
					}
				}
			}
		}
	}
}
