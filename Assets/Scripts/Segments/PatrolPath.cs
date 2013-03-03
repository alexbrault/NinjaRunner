using UnityEngine;
using System.Collections;

public class PatrolPath : MonoBehaviour {	
	private void OnDrawGizmos() {
		Transform[] patrolPoints = new Transform[2];
		var points = GetComponentsInChildren<PatrolPoint>() as PatrolPoint[];
		patrolPoints[0] = points[0].transform;
		patrolPoints[1] = points[1].transform;
		
		Gizmos.color = Color.green;	
		
		Gizmos.DrawLine(patrolPoints[0].position, patrolPoints[1].position);
		Gizmos.DrawCube(patrolPoints[0].position, Vector3.one*0.1f);
		Gizmos.DrawCube(patrolPoints[1].position, Vector3.one*0.1f);
	}
}
