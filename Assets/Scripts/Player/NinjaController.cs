using UnityEngine;
using System.Collections;

public class NinjaController : MonoBehaviour {
	
	public int JumpForce;
	public int WallJumpForce;
	public int RunAcceleration;
	public int MaxSpeed;
	public NetworkPlayer netPlayer;	
		
	public enum PlayerID : int {
		Player1 = 0,
		Player2 = 1,
		PlayerNone = 2,
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
	}
	
	public string[,] KeyNames = {
									{"P1Left", "P1Right", "P1Jump", "P1Shoot"}, 
									{"P2Left", "P2Right", "P2Jump", "P2Shoot"}, 
									{"null", "null", "null", "null"},
								};
	
	public GameObject Shuriken;
	
	private int jumpsAvailable = 2;
	private bool canMove = true;
	private bool onWall = false;
	private bool canShoot = true;
	
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
	
	void Run()
	{
		if(InputEx.GetButton(KeyNames[Player, KeyID.Left]))
		{
			gameObject.rigidbody.AddForce(Vector3.left * RunAcceleration * 100);
		}
		
		else if (InputEx.GetButton(KeyNames[Player, KeyID.Right]))
		{
			gameObject.rigidbody.AddForce(Vector3.right * RunAcceleration * 100);
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
		gameObject.rigidbody.AddForce(Vector3.up * JumpForce * 100 * jumpsAvailable / 2);
		canMove = true;
		jumpsAvailable--;
	}
	
	void WallJump()
	{
		gameObject.rigidbody.AddForce(new Vector3(nextWallJumpX, 1, 0) * WallJumpForce * 100);
		jumpsAvailable = 1;
		canMove = false;
		onWall = false;
	}
	
	IEnumerator ShootShurikens()
	{
		canShoot = false;
		
		Vector3 direction = new Vector3(2.5f, -0.5f, 0);
		
		if(gameObject.rigidbody.velocity.x < 0)
			direction.x = -2.5f;
		
		GameObject[] shurikens = new GameObject[3];
		
		for(int i = 0; i < 3; i++)
		{
			GameObject throwed = (GameObject)GameObject.Instantiate(Shuriken, gameObject.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
			throwed.rigidbody.AddForce(direction * 1000);
			
			Physics.IgnoreCollision(gameObject.collider, throwed.collider, true);
			
			for(int j = 0; j < i; j++)
			{
				Debug.Log (i);
				Physics.IgnoreCollision(throwed.collider, shurikens[j].collider, true);
			}
			
			shurikens[i] = throwed;
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
		Collider contact = collision.collider;

		if(contact.gameObject.tag == "Floor")
		{
			jumpsAvailable = 2;
			canMove = true;
		}
		
		if(contact.gameObject.tag == "Wall" && jumpsAvailable != 2)
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
	
	private void EnteredSegment(Segment s) {
		currentSegment = s;
	}
	
	public void Respawn() {
		transform.position = currentSegment.SafeSpawn;
		canMove = false;
		jumpsAvailable = 0;
		gameObject.rigidbody.velocity = new Vector3(0, 0, 0);
		gameObject.rigidbody.useGravity = true;
	}
}