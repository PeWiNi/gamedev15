using UnityEngine;
using System.Collections;

public class CprScript : MonoBehaviour
{
	/*
     * Costs banana resources
     * Has 4.5 sec CD
     */

	float lastUsed;
	bool available = true;
	TestPlayerBehaviour tpb;

	void start ()
	{
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
	}

	void Update ()
	{
       
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
        
		if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats> ().cprCooldown) {
			available = true;
		}


		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
		if (Input.GetKeyDown (tpb.cprKey)) {
			if (available) {
//				BoltEntity self = this.GetComponentInParent<TestPlayerBehaviour>().entity;
//				int resources = this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas;
//				using (var evnt = CprEvent.Create(Bolt.GlobalTargets.Everyone)) {
//					evnt.TargEnt = self;
//				}
//				available = false;
//				this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas--;
					IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
					//int teammates = 0;
					//BoltEntity self = this.GetComponentInParent<TestPlayerBehaviour>().entity;
					while (entities.MoveNext()) {

						if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
							BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
							// Create Event and use the be, if it is the one that is colliding.
						if(be.isOwner){
							using (var evnt = CprEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
							}
							available = false;
							this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas--;
						}
							if (be.gameObject == this.gameObject.GetComponentInParent<PlayerStats>().gameObject) {
								
							}
						}

				}
			}
		}		
	}

//	void OnTriggerStay (Collider coll)
//	{
//		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
//		if (Input.GetKeyDown (tpb.cprKey)) {
//			if (available) {
//				int resources = this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas;
//				IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
//				int teammates = 0;
//				BoltEntity self = new BoltEntity ();
//				while (entities.MoveNext()) {
//					if (coll.gameObject.tag == "player" && resources > 0) {
//						if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
//							BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
//							// Create Event and use the be, if it is the one that is colliding.
//							if (be.gameObject == this.gameObject) {
//								self = be;
//							}
//							if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour> ().gameObject) {
//								if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber == this.gameObject.GetComponentInParent<PlayerStats> ().teamNumber) {
//									teammates++;
//									using (var evnt = CprEvent.Create(Bolt.GlobalTargets.Everyone)) {
//										evnt.TargEnt = be;
//									}
//									available = false;
//									this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas--;
//								}
//							}
//						}
//					}
//
//					//coll.gameObject.GetComponentInChildren<CprScript>().ress();
//				}
//				if (teammates == 0) {
//					BoltEntity be2 = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ().entity;
//					using (var evnt = CprEvent.Create(Bolt.GlobalTargets.Everyone)) {
//						evnt.TargEnt = be2;
//					}
//					//this.gameObject.GetComponentInParent<PlayerStats>().hp = this.gameObject.GetComponentInParent<PlayerStats>().maxHealth;
//					available = false;
//					this.gameObject.GetComponentInParent<PlayerStats> ().cprBananas--;
//				}
//
//			}
//		}
//	}

	public void ress ()
	{
		StateController sc = this.gameObject.GetComponentInParent<StateController> ();
		Debug.Log ("I AM BEING RESSED!");
		this.gameObject.GetComponentInParent<PlayerStats> ().hp = this.gameObject.GetComponentInParent<PlayerStats> ().maxHealth;
		sc.isDead = false;
		sc.isStunned = false;
		sc.ressStarted = false;
		sc.canMove = true;

		// CALL DEATHSPAWNER -> CANCEL RESPAWN...
		this.gameObject.GetComponentInParent<DeathSpawner> ().cancelRespawn ();
	}
}
