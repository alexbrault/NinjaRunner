using UnityEngine;
using System.Collections;

public class FinalBattleNinja : MonoBehaviour {
	
	public enum Player
	{
		PLAYER_1,
		PLAYER_2
	}
	
	public Player player;
	private NinjaRenderer spriteRenderer;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<NinjaRenderer>();
		
		if(player == Player.PLAYER_1)
		{
			spriteRenderer.PlayAnimation("DashRight");
			transform.RotateAroundLocal(new Vector3(0,0,-1), 45);
		}
		
		else
		{
			spriteRenderer.PlayAnimation("DashLeft");
			transform.RotateAroundLocal(new Vector3(0,0,-1), -45);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateBattleRender()
	{
		if(player == Player.PLAYER_1)
		{
			spriteRenderer.PlayAnimation("BattleRight");
			transform.RotateAroundLocal(new Vector3(0,0,-1), -45);
		}
		
		else
		{
			spriteRenderer.PlayAnimation("BattleLeft");
			transform.RotateAroundLocal(new Vector3(0,0,-1), 45);
		}
	}
	
	public void UpdateFinalRender(bool looser)
	{
		if(player == Player.PLAYER_1)
		{
			spriteRenderer.PlayAnimation("IdleRight");
			
			if(looser)
			{				
				transform.RotateAroundLocal(new Vector3(0,0,-1), 45);
			}
		}
		
		if(player == Player.PLAYER_2)
		{
			spriteRenderer.PlayAnimation("IdleLeft");
			
			if(looser)
			{
				transform.RotateAroundLocal(new Vector3(0,0,-1), -45);
			}
		}
	}
}
