using UnityEngine;
using System.Collections;

public class CoconutEffect : MonoBehaviour
{
		public bool isCoconutNotConsumed = true; 
	

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
						if (isCoconutNotConsumed == true)
								coll.GetComponent<PlayerStats> ().IsInCoconutArea = true;
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
//						}
//						IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
//						while (entities.MoveNext()) {
//								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
//										BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
//										// Create Event and use the be, if it is the one that is colliding.
//										if (be.gameObject == coll.gameObject) {
//												using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
//														evnt.TargEnt = be;
//														if (coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
//																evnt.isAffectedByCoconut = true;
//														}
//														evnt.StoppedInCoconutConsume = true;
//												}
//										}
//								}
				}
		}
}

