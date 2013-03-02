using UnityEngine;
using System.Collections;

public class SafeSpawn : MonoBehaviour {
	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 0.5f);
	}
}
