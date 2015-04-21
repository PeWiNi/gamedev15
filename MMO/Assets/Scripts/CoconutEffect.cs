using UnityEngine;
using System.Collections;

public class CoconutEffect : MonoBehaviour
{
	public bool isCoconutNotConsumed = true;

	public float coconutReappearsTimer = 60;
	public float coconutPickUpTime;
	

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
		
	void OnTriggerStay (Collider coll)
	{
		if (coll.gameObject.tag == "player") {
			if (isCoconutNotConsumed == true) {
				coll.GetComponent<PlayerStats> ().IsInCoconutArea = true;
				if (coll.GetComponent<PlayerStats> ().canPickUpCoconut == true) {
					if (coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
						IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
						while (entities.MoveNext()) {
							if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
								BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
								// Create Event and use the be, if it is the one that is colliding.
								//The Coconut is consumed
								if (be.gameObject == this.gameObject) {
									using (var evnt = CoconutUnavailableEvent.Create(Bolt.GlobalTargets.Everyone)) {							
										evnt.TargEnt = be;
										evnt.isCoconutNotConsumed = false;
										evnt.CoconutPickedUpTime = Time.time;
									}
								}
							}
						}
						StartCoroutine ("coconutReappears");
					}
				}
			}
		}
	}

	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "player") {
			coll.GetComponent<PlayerStats> ().IsInCoconutArea = false;
			if (isCoconutNotConsumed == true) {
				if (coll.GetComponent<PlayerStats> ().hasCoconutEffect == false) {
					coll.GetComponent<PlayerStats> ().stoppedInCoconutConsume = true;
				}
			}
		}
	}

	IEnumerator coconutReappears ()
	{
		yield return new WaitForSeconds (60);
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				// Create Event and use the be, if it is the one that is colliding.
				//The Coconut respawns
				if (be.gameObject == this.gameObject) {
					using (var evnt = CoconutAvailableEvent.Create(Bolt.GlobalTargets.Everyone)) {							
						evnt.TargEnt = be;
						evnt.isCoconutNotConsumed = true;
						evnt.CoconutPickedUpTime = Time.time;
					}
				}
			}
		}
	}
}
