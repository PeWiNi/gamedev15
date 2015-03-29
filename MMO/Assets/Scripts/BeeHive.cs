using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeHive : MonoBehaviour
{
		bool isActivatedByTeamOne = false;
		bool isActivatedByTeamTwo = false;
		bool setUpTimer = false;
		bool isTrapDestroyed = false;
		public static float maxHealth;
		public static float healthRemain;
		public static float health;
		public static float damageDeal;
		

		// Use this for initialization
		void Start ()
		{	
		}
	
		// Update is called once per frame
		void Update ()
		{
				StartCoroutine ("DeactivateTrap");
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
												ForestAreaScript.beeHives.Remove (this.gameObject);
												Destroy (this.gameObject);
												isTrapDestroyed = true;
												if (isTrapDestroyed == true) {
														// if the damage isn't the same as 10 %, note; use a bool.
														using (var evnt = BeeHiveTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
																IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
																while (playerEntities.MoveNext()) {
																		if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																				BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
																				if (be.gameObject == coll.gameObject) {
																						evnt.TargEnt = be;
																				}
																		}
																}
																evnt.TrapDamage = (coll.GetComponent<PlayerStats> ().maxHealth * 10) / 100;
														} 
												}
										}
								}
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								if (isActivatedByTeamOne == false) {
										StartCoroutine ("SetupTimer");
										isActivatedByTeamTwo = true;
								} else if (isActivatedByTeamOne == true) {
										if (setUpTimer == true) {
												ForestAreaScript.beeHives.Remove (this.gameObject);
												Destroy (this.gameObject);
												isTrapDestroyed = true;
												if (isTrapDestroyed == true) {
														// if the damage isn't the same as 10 %, note; use a bool.
														using (var evnt = BeeHiveTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
																IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
																while (playerEntities.MoveNext()) {
																		if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																				BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
																				if (be.gameObject == coll.gameObject) {
																						evnt.TargEnt = be;
																				}
																		}
																}
																evnt.TrapDamage = (coll.GetComponent<PlayerStats> ().maxHealth * 10) / 100;
														} 
												}
										}
								}
						}	
				}
		}

		IEnumerator DestoryTrapAfterTrigger ()
		{
				yield return new WaitForSeconds (10);
				Destroy (this.gameObject);
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
						ForestAreaScript.beeHives.Remove (this.gameObject);
						Destroy (this.gameObject);
				}
		}

		
}
