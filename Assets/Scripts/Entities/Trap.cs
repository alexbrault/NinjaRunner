using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class Trap : MonoBehaviour {
	public float DeathRadius = 0.5f;
	public float TriggerRadius = 0.8f;
	public float WarnRadius = 1.5f;
	public bool IsRepeating = false;
	public float FuseLength = 0.5f;
	
	private int cPeopleInside = 0;
	private Renderer warningRenderer;
	
	void Start () {
		collider.isTrigger = true;
		((SphereCollider)collider).radius = WarnRadius * 10;
		warningRenderer = GetComponentInChildren<MeshRenderer>();
		warningRenderer.enabled = false;
	}
	
	void OnTriggerEnter(Collider other) {
		cPeopleInside++;
		warningRenderer.enabled = true;
	}
	
	void OnTriggerExit(Collider other) {
		cPeopleInside--;
		warningRenderer.enabled = cPeopleInside > 0;
	}
	
	void FixedUpdate() {
		if (cPeopleInside > 0) {
			var peopleInsideTrigger = from p in Physics.OverlapSphere(transform.position, TriggerRadius)
									  where p.tag == "Player"
									  select p;
			if (peopleInsideTrigger.Count() != 0) {
				cPeopleInside = 0;
				collider.enabled = false;
				StartCoroutine(Blow());
			}
		}
	}
	
	IEnumerator Blow() {
		yield return new WaitForSeconds(FuseLength);
		
		particleSystem.Play();
		
		var peopleInsideDeath = Physics.OverlapSphere(transform.position, DeathRadius);
		yield return new WaitForSeconds(0.5f);
		
		foreach (var person in peopleInsideDeath) {
			person.SendMessage("Respawn", SendMessageOptions.DontRequireReceiver);
		}
		
		if (!IsRepeating)
			Destroy(gameObject);
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DeathRadius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, TriggerRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, WarnRadius);
	}
}
