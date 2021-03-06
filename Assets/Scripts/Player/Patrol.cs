using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public int WalkingSpeed;
	
	private Transform[] patrolPoints;
	private int directionX;
	public int maxHealth = 3;
	private int health;
	
	private NinjaRenderer spriteRenderer;
	
	public void Start()
	{
		spriteRenderer = (NinjaRenderer)gameObject.GetComponent<NinjaRenderer>();
	}
	
	public void StartPatrol(Transform patrol) {	
		
		health = maxHealth;
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
	
	void Update () {
	
		rigidbody.velocity = new Vector3(directionX, 0, 0) * WalkingSpeed;
		
		if(rigidbody.velocity.x > 0)
			spriteRenderer.PlayAnimation("WalkRight");
		
		if(rigidbody.velocity.x < 0)
			spriteRenderer.PlayAnimation("WalkLeft");
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
		if(directionX > 0)
			spriteRenderer.PlayAnimation("IdleRight");
		
		if(directionX < 0)
			spriteRenderer.PlayAnimation("IdleLeft");
		
		yield return new WaitForSeconds(3);
		
		directionX = -directionX;
	}
	
	private void DealtDamage(DamagePacket packet) {
		health -= packet.Damage;
		if (health <= 0) {
			Destroy(gameObject);
			ScoreManager.Instance.AddScore(packet.Source, 50); 
		}
	}
}
