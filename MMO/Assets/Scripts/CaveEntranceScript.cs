using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaveEntranceScript : MonoBehaviour
{
	GameObject[] players;
	TestPlayerBehaviour tpb;
	Collider[] colliders1;
	Collider[] colliders2;
	Collider c3;
	
	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "player" && coll.collider.gameObject.GetComponent<PlayerStats> ().teamNumber == 2) {
			colliders1 = coll.gameObject.GetComponents<Collider> ();
			colliders2 = coll.gameObject.GetComponentsInChildren<Collider> ();
			c3 = gameObject.GetComponent<SphereCollider> ();
			foreach (Collider c1 in colliders1) {
				Physics.IgnoreCollision (c1, c3);
			}
			foreach (Collider c2 in colliders2) {
				Physics.IgnoreCollision (c2, c3);
			}
		}
	}
}
