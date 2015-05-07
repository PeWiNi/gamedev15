using UnityEngine;
using System.Collections;

public class TailSlap : MonoBehaviour
{

	bool available = true;
	float lastUsed;
	StateController sc;
	PlayerStats ps;
	TestPlayerBehaviour tpb;
	public bool TailSlapUsedInHidingGrass;

	void start ()
	{
		sc = this.gameObject.GetComponentInParent<StateController> ();
		ps = this.gameObject.GetComponentInParent<PlayerStats> ();
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
	}

	void Update ()
	{
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
		sc = this.gameObject.GetComponentInParent<StateController> ();
		if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats> ().tailSlapCooldown) {
			available = true;
		}
		if (Input.GetKeyDown (tpb.tailSlapKey) && !sc.isStunned && !sc.isChanneling && !sc.isDead) {
			var evnt = TailAnimEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.TargEnt = GetComponentInParent<TestPlayerBehaviour>().entity;
            evnt.Send();
//			GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_TS");
		}
	}

	void OnTriggerStay (Collider coll)
	{

		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (coll.gameObject.tag == "grass") {
			if (Input.GetKeyDown (tpb.tailSlapKey)) {
				TailSlapUsedInHidingGrass = true;
			}
		}
		if (coll.gameObject.tag == "player") {
			sc = gameObject.GetComponentInParent<StateController> ();
			ps = gameObject.GetComponentInParent<PlayerStats> ();
			if (Input.GetKeyDown (tpb.tailSlapKey) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
					

				GameObject go = GameObject.Find ("Canvas");
				HUDScript hs = go.GetComponentInChildren<HUDScript> ();

                hs.announcementText.text = "Miss";
				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						// Create Event and use the be, if it is the one that is colliding.

						if (be.gameObject == coll.gameObject) { // Check for enemy, deal full damage
							if (available) {
								Debug.Log ("SLAPPING DA TAIL");
								if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != this.gameObject.GetComponentInParent<PlayerStats> ().teamNumber) {
									// deal full damage!!!
									var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone);
                                    hs.announcementText.text = "" + this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage;
									evnt.TargEnt = be;
                                    evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage;
                                    evnt.Send();
								} else if (!be.isOwner) {
									//Do nothing.
								} else { // check for friendly player, deal 50% dmg.
									// deal half damage!!!
									var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone);
                                    hs.announcementText.text = "" + this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage / 2;
									evnt.TargEnt = be;
                                    evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage / 2;
                                    evnt.Send();
								}

								available = false;
								lastUsed = Time.time;
							}

							//  Debug.Log("BoltEntity.gameObject matches coll.gameObject");

						}
					}
				}
			}
		}
	}
}
