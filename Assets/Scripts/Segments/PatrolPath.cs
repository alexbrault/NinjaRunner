using UnityEngine;
using System.Collections;

public class PatrolPath : MonoBehaviour {
	private void OnDrawGizmos() {
		Gizmos.color = Color.green;	
		
		Transform[] patrolPoints = new Transform[2];
		patrolPoints[0] = transform.GetChild(0);
		patrolPoints[1] = transform.GetChild(1);
		
		Gizmos.DrawLine(patrolPoints[0].position, patrolPoints[1].position);
	}
}
