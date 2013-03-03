using UnityEngine;
using System.Collections;

public abstract class CollisionListener : MonoBehaviour {
	
	public abstract void Notify(Collider coll1, Collider coll2);
}
