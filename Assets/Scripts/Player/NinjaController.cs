using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NinjaController : MonoBehaviour {
	
	public int JumpForce;
	public int WallJumpForce;
	public float RunAcceleration;
	public int MaxSpeed;
	public NetworkPlayer netPlayer;
	
	public const int ForceMod = 10;
	
	private List<CollisionListener> listeners;
	
	private int shootShurikenCount = 0;
	private bool shooting = false;
		
	public enum PlayerID : int {
		Player1 = 0,
		Player2 = 1,
		PlayerNone = 2,
		NumPlayer
	}
	
	private Segment currentSegment;
	
	public PlayerID player;
	public int Player {
		get { return (int) player; }
	}
	
	public void SetPlayer(PlayerID pid) {
		player = pid;
	}
	
	public class KeyID {
		public const int Left = 0;
		public const int Right = 1;
		public const int Jump = 2;
		public const int Shoot = 3;
		public const int Grappling = 4;
	}
	
	public static string[,] KeyNames = {
									{"P1Left", "P1Right", "P1Jump", "P1Shoot", "P1Grappling"}, 
									{"P2Left", "P2Right", "P2Jump", "P2Shoot", "P2Grappling"}, 
									{"null", "null", "null", "null", "null"},
								};
	private int facing = 1;
	public GameObject Shuriken;
	
	private int jumpsAvailable = 1;
	private bool canMove = true;
	private bool onWall = false;
	private bool canShoot = true;
	
	private NinjaRenderer spriteRenderer;
	
	private int nextWallJumpX = -1;
	
	void Awake()
	{
		listeners = new List<CollisionListener>();
	}
	
	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Player"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("IgnorePlayerCollision"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapons"),LayerMask.NameToLayer("IgnorePlayerCollision"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapons"),LayerMask.NameToLayer("Weapons"));
		
		spriteRenderer = (NinjaRenderer)gameObject.GetComponent<NinjaRenderer>();
		
		if(Player == 0)
			spriteRenderer.PlayAnimation("IdleRight");
		
		else
			spriteRenderer.PlayAnimation("IdleLeft");
	}
	
	public void AddListener(CollisionListener listener)
	{
		listeners.Add (listener);
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			Run ();
		}
		
		if(jumpsAvailable != 0 && InputEx.GetButtonDown(KeyNames[Player, KeyID.Jump]))
			Jump();
		
		if(onWall && InputEx.GetButtonDown(KeyNames[Player, KeyID.Jump]))
			WallJump();
		
		if(canShoot && InputEx.GetButtonDown(KeyNames[Player, KeyID.Shoot]))
			StartCoroutine(ShootShurikens());
	}
	
	public bool IsJumping()
	{
		return jumpsAvailable < 1;
	}
	
	void Run()
	{		
		if(InputEx.GetButton(KeyNames[Player, KeyID.Left]))
		{			
			rigidbody.AddForce(Vector3.left * RunAcceleration * ForceMod);
			facing = -1;
		}
		
		else if (InputEx.GetButton(KeyNames[Player, KeyID.Right]))
		{
			rigidbody.AddForce(Vector3.right * RunAcceleration * ForceMod);
			facing = +1;
		}
		
		if(Mathf.Abs(rigidbody.velocity.x) > MaxSpeed)
		{
			Vector3 newVel = new Vector3(0, 0, 0);
			float velY = rigidbody.velocity.y;
			
			newVel = rigidbody.velocity.normalized * MaxSpeed;
			newVel.y = velY;
			
			rigidbody.velocity = newVel;
		}
		
		if(!shooting)
		{
			if(rigidbody.velocity.x > 1)
				spriteRenderer.PlayAnimation("RunRight");
			
			else if(rigidbody.velocity.x < -1)
				spriteRenderer.PlayAnimation("RunLeft");
			
			else
			{
				if(Player == 0)
					spriteRenderer.PlayAnimation("IdleRight");
				
				else
					spriteRenderer.PlayAnimation("IdleLeft");
			}
		}
	}
	
	void Jump()
	{
		rigidbody.AddForce(Vector3.up * JumpForce * ForceMod );
		canMove = true;
		jumpsAvailable--;
	}
	
	void WallJump()
	{
		StopCoroutine("FallOffWall");
		rigidbody.AddForce(new Vector3(nextWallJumpX, 1, 0) * WallJumpForce * ForceMod);
		jumpsAvailable = 0;
		facing = -facing;
		canMove = false;
		onWall = false;
		rigidbody.useGravity = true;
		
		if(nextWallJumpX > 0)
		{
			spriteRenderer.PlayAnimation("WallJumpRight");
		}
		
		else
		{
			spriteRenderer.PlayAnimation("WallJumpLeft");
		}
	}
	
	IEnumerator ShootShurikens()
	{
		shooting = true;
		
		if(transform.rigidbody.velocity.x >= 0)
			spriteRenderer.PlayAnimation("ThrowShurikenRight");
		
		else
			spriteRenderer.PlayAnimation("ThrowShurikenLeft");
		
		spriteRenderer.AddActiveAnimationDelegate(ResumeJump);
		
		canShoot = false;
		
		Vector3 direction = new Vector3(2.5f * facing, -0.5f, 0);
		
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.SHIRUKEN);
		
		for(int i = 0; i < 3; i++)
		{
			GameObject throwed = (GameObject)GameObject.Instantiate(Shuriken, gameObject.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
			Shuriken shuriken = throwed.GetComponent<Shuriken>();
			shuriken.packet = new DamagePacket(1, Player);
			throwed.rigidbody.AddForce(direction * 200);
			
			Physics.IgnoreCollision(gameObject.collider, throwed.collider, true);
			yield return new WaitForSeconds(0.1f);
		}
		
		StartCoroutine(ReloadShurikens());
	}
	
	IEnumerator ReloadShurikens()
	{
		yield return new WaitForSeconds(0.5f);
		canShoot = true;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		shooting = false;
		foreach(CollisionListener listener in listeners)
		{
			listener.Notify(gameObject.collider, collision.collider);
		}
		
		Collider contact = collision.collider;

		if(contact.gameObject.tag == "Floor")
		{
			if (collision.relativeVelocity.y > 0){
				jumpsAvailable = 1;
				canMove = true;
			}
		}
		
		if(contact.gameObject.tag == "Wall" && jumpsAvailable != 1)
		{
			if(collision.transform.position.x > transform.position.x)
			{
				spriteRenderer.PlayAnimation("OnWallLeft");
			}
			
			else
			{
				spriteRenderer.PlayAnimation("OnWallRight");
			}
			
			canMove = false;
			onWall = true;
			rigidbody.velocity = new Vector3(0, 0, 0);
			rigidbody.useGravity = false;
			
			if(contact.gameObject.transform.position.x - gameObject.transform.position.x > 0)
				nextWallJumpX = -1;
			
			else
				nextWallJumpX = 1;
			
			StartCoroutine(FallOffWall());
		}
		
		if (contact.gameObject.tag == "Water") {
			Respawn();
		}
	}
	
	IEnumerator FallOffWall()
	{
	    yield return new WaitForSeconds(2);
		rigidbody.useGravity = true;
		onWall = false;
		
		if(player == PlayerID.Player1)
		{
			spriteRenderer.PlayAnimation("IdleRight");
		}
		
		else
		{
			spriteRenderer.PlayAnimation("IdleLeft");
		}
	}
	
	private void EnteredSegment(Segment s) {
		currentSegment = s;
	}
	
	public void Respawn() {
		transform.position = currentSegment.SafeSpawn.position;
		canMove = false;
		jumpsAvailable = 0;
		rigidbody.velocity = new Vector3(0, 0, 0);
		rigidbody.useGravity = true;
	}
	
	public void ResumeJump()
	{
		shootShurikenCount++;
		
		if(shootShurikenCount > 2)
		{
			shootShurikenCount = 0;
			shooting = false;
			
			if(transform.rigidbody.velocity.x >= 0)
				spriteRenderer.PlayAnimation("RunRight");
			
			else
				spriteRenderer.PlayAnimation("RunLeft");
			
			spriteRenderer.RemoveActiveAnimationDelegate(ResumeJump);
		}
	}
}