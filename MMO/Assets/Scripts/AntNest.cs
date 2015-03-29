using UnityEngine;
using System.Collections;

public class AntNest : MonoBehaviour
{
		bool isActivatedByTeamOne = false;
		bool isActivatedByTeamTwo = false;
		bool setUpTimer = false;
		
		// Use this for initialization
		void Start ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								if (isActivatedByTeamTwo == false) {
										StartCoroutine ("SetupTimer");
										isActivatedByTeamOne = true;
								} else if (isActivatedByTeamTwo == true) {
										if (setUpTimer == true) {
												ForestAreaScript.antNests.Remove (this.gameObject);
												Destroy (this.gameObject);
												// if the damage isn't the same as 10 %, note; use a bool.
												using (var evnt = AntNestTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
														IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
														while (playerEntities.MoveNext()) {
																if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																		BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
																		if (be.gameObject == coll.gameObject) {
																				evnt.TargEnt = be;
																		}
																}
														}
														evnt.TrapStunDuration = (coll.GetComponent<PlayerStats> ().ccDuration + 2);
												} 
										}
								}
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								if (isActivatedByTeamOne == false) {
										StartCoroutine ("SetupTimer");
										isActivatedByTeamTwo = true;
								} else if (isActivatedByTeamOne == true) {
										if (setUpTimer == true) {
												ForestAreaScript.antNests.Remove (this.gameObject);
												Destroy (this.gameObject);
												// if the damage isn't the same as 10 %, note; use a bool.
												using (var evnt = AntNestTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
														IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
														while (playerEntities.MoveNext()) {
																if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																		BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
																		if (be.gameObject == coll.gameObject) {
																				evnt.TargEnt = be;
																		}
																}
														}
														evnt.TrapStunDuration = (coll.GetComponent<PlayerStats> ().ccDuration + 2);
												} 
										}
								}
						}	
				}
		}

		/// <summary>
		/// Setups the timer.
		/// </summary>
		/// <returns>The timer.</returns>
		IEnumerator SetupTimer ()
		{
				yield return new WaitForSeconds (3.5f);
				setUpTimer = true;
		}
	
		/// <summary>
		/// Deactivates the trap.
		/// </summary>
		/// <returns>The trap.</returns>
		IEnumerator DeactivateTrap ()
		{
				if (isActivatedByTeamOne == true || isActivatedByTeamTwo == true) {
						//should be 5 min wait time.
						yield return new WaitForSeconds (100);
						ForestAreaScript.antNests.Remove (this.gameObject);
						Destroy (this.gameObject);
				}
		}
}
