using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{


	StateController sc;
	PlayerStats ps;
	float lastTick = 0;
	float tickTimer;
	float currentTimer;
    
	// Use this for initialization
	void Start ()
	{
		sc = this.gameObject.GetComponentInParent<StateController> ();
		ps = this.gameObject.GetComponentInParent<PlayerStats> ();
		tickTimer = sc.globalCooldownTime;
	}

	// Update is called once per frame
	void Update ()
	{
		currentTimer = Time.time;
	}

	void OnTriggerStay (Collider coll)
	{
		if ((currentTimer - lastTick) > tickTimer) {
			if (!this.gameObject.GetComponentInParent<StateController> ().isDead) { 
				if (Input.GetMouseButtonDown (0)) {
					IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
					if (coll.gameObject.tag == "player") {
						while (entities.MoveNext()) {
							if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
								BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
								// Create Event and use the be, if it is the one that is colliding.
								if (be.gameObject == coll.gameObject) { // Check for enemy, deal full damage

									if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != this.gameObject.GetComponentInParent<PlayerStats> ().teamNumber) {
										// deal full damage!!!
										var evnt = AutoAttackEvent.Create(Bolt.GlobalTargets.Everyone);
										evnt.TargEnt = be;
                                        evnt.Damage = (2.0f);
                                        evnt.Send();
										Debug.Log ("AutoAttacking");
									}
									lastTick = currentTimer;
									sc.initiateCombat ();
								}
							}
						}
					}
				}
			}
		}
	}
}
