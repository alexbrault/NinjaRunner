using UnityEngine;
using System.Collections;

public class FinalBattle : MonoBehaviour {
	
	public GameObject player1;
	public GameObject player2;
	public GameObject winner;
	public GameObject looser;
	public float maxDeltaScore;
	public float deltaScore;
	public Font textFont;
	private GUIStyle textStyle = new GUIStyle();
	
	private GameObject p1Dash;
	private GameObject p2Dash;
	private GameObject blood;
	private GameObject bloodTraces;
	private GameObject swordFight;
	
	private Vector3 player1BattlePos;
	private Vector3 player2BattlePos;
	
	private bool inBattle = false;
	private float timer = 0;
	private float nextBloodTrace = 0;
	
	private System.Random random = new System.Random();
	
	private string topText = "";
	private string bottomText = "";
	
	// Use this for initialization
	void Start () {
		p1Dash = gameObject.transform.FindChild("Player1Dash").gameObject;
		p2Dash = gameObject.transform.FindChild("Player2Dash").gameObject;
		swordFight = gameObject.transform.FindChild("SwordFight").gameObject;
		blood = gameObject.transform.FindChild("Blood").gameObject;
		bloodTraces = gameObject.transform.FindChild("BloodTraces").gameObject;
		
		player1BattlePos = player1.transform.position;
		player2BattlePos = player2.transform.position;
		
		swordFight.AddComponent<SwordFight>();
		swordFight.renderer.enabled = true;
		
		blood.GetComponent<ParticleSystem>().enableEmission = false;
		Vector3 pos = looser.transform.position;
		pos.z -= 1;
		blood.transform.position = pos;
		
		swordFight.renderer.enabled = false;
		p1Dash.GetComponent<ParticleSystem>().enableEmission = false;
		p2Dash.GetComponent<ParticleSystem>().enableEmission = false;
		
		StartCoroutine(Player1Dash ());
	}
	
	// Update is called once per frame
	void Update () {
		
		if(inBattle)
		{
			timer += Time.deltaTime;
			
			if(timer >= nextBloodTrace)
			{
				bloodTraces.particleSystem.Play();
				nextBloodTrace = random.Next(2, 12) / 10.0f;
				timer = 0;
			}
		}
	}
	
	IEnumerator Player1Dash()
	{
		yield return new WaitForSeconds(0.2f); // Like, HUGE bug fix important
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.DRAW_SWORD);		
		
		p1Dash.GetComponent<ParticleSystem>().enableEmission = true;
		player2.renderer.enabled = false;
		
		Vector3 pos = Camera.mainCamera.transform.position;
		pos.z = 0;
		
		player1.transform.position = pos;
		player1.transform.localScale = new Vector3(1, 2, 1);
		
		yield return new WaitForSeconds(3);
		StartCoroutine(Player2Dash ());
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.DRAW_SWORD);		
						
		player1.transform.position = player1BattlePos;
		player1.transform.localScale = new Vector3(0.5f, 1, 1);
	}
	
	IEnumerator Player2Dash()
	{
		p1Dash.GetComponent<ParticleSystem>().enableEmission = false;
		p2Dash.GetComponent<ParticleSystem>().enableEmission = true;
		
		player2.renderer.enabled = true;
		player1.renderer.enabled = false;
		
		Vector3 pos = Camera.mainCamera.transform.position;
		pos.z = 0;
		
		player2.transform.position = pos;
		player2.transform.localScale = new Vector3(1, 2, 1);
		
		yield return new WaitForSeconds(3);

		player2.transform.position = player2BattlePos;
		player2.transform.localScale = new Vector3(0.5f, 1, 1);
		player1.renderer.enabled = true;
		p1Dash.GetComponent<ParticleSystem>().enableEmission = true;
		
		StartCoroutine(Battle ());
	}
	
	IEnumerator Battle()
	{
		GameObject.Find("fond").renderer.enabled = true;
		
		player1.GetComponent<FinalBattleNinja>().UpdateBattleRender();
		player2.GetComponent<FinalBattleNinja>().UpdateBattleRender();
		
		inBattle = true;
		nextBloodTrace = random.Next(2, 12) / 10.0f;
		
		SoundManager.Instance.gameObject.GetComponent<AudioSource>().loop = true;			
		SoundManager.Instance.PlaySound(SoundManager.soundEnum.SWORD_FIGHT);		
		
		float seconds = Mathf.Log(maxDeltaScore - deltaScore);
		
		if(seconds < 0)
			seconds = 0;
		
		if(seconds > 5)
			seconds = 5;
		
		Debug.Log (seconds);
		
		swordFight.renderer.enabled = true;
		yield return new WaitForSeconds(seconds);
		
		player1.GetComponent<FinalBattleNinja>().UpdateFinalRender(player1 == looser);
		player2.GetComponent<FinalBattleNinja>().UpdateFinalRender(player2 == looser);
		
		p1Dash.GetComponent<ParticleSystem>().enableEmission = false;
		p2Dash.GetComponent<ParticleSystem>().enableEmission = false;
		
		inBattle = false;

		SoundManager.Instance.gameObject.GetComponent<AudioSource>().loop = false;
		SoundManager.Instance.gameObject.GetComponent<AudioSource>().Stop();
		
		swordFight.GetComponent<SwordFight>().PlayKillAnimation();
		//looser.transform.RotateAroundLocal(new Vector3(0, 0, -1), 90);
		blood.GetComponent<ParticleSystem>().enableEmission = true;
		
		
		yield return new WaitForSeconds(1.5f);
		topText = "PLAYER " + ((int)winner.GetComponent<FinalBattleNinja>().player + 1);
		yield return new WaitForSeconds(1.5f);
		bottomText = "WINS";
		yield return new WaitForSeconds(0.75f);
		gameObject.AddComponent<ReturnToMenu>().style = textStyle;
	}
	
	private void OnGUI() {
		textStyle.alignment = TextAnchor.MiddleCenter;
		textStyle.fontSize = 64;
		textStyle.font = textFont;
		textStyle.normal.textColor = Color.red;
		GUI.Label (new Rect(0, 10, Screen.width, 70), topText, textStyle);
		GUI.Label (new Rect(0, Screen.height - 80, Screen.width, 70), bottomText, textStyle);
	}
}
