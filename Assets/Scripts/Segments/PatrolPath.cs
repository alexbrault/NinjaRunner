using UnityEngine;
using System.Collections;

public class PatrolPath : MonoBehaviour {
	private Transform[] patrolPoints;
	private void Awake() {
		patrolPoints = new Transform[2];
		patrolPoints[0] = transform.GetChild(0);
		patrolPoints[1] = transform.GetChild(1);
	}
	private void OnDrawGizmos() {
		Gizmos.color = Color.green;	
		
		Gizmos.DrawLine(patrolPoints[0].position, patrolPoints[1].position);
		Gizmos.DrawCube(patrolPoints[0].position, Vector3.one);
		Gizmos.DrawCube(patrolPoints[1].position, Vector3.one);
	}
}
