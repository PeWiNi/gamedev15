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
		public static float playerBuffDmg;
		public static bool playerOneIsBuffed;
		public static bool playerTwoIsBuffed;
		public static float lastTimeBuffed;
		public static float tailSlapDmg;
		public static float boomnanaDmg;
		public static bool oneTimeBuffer = true; 

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
						playerBuffDmg = coll.GetComponent<PlayerStats> ().buffDamageFactor;
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								if (isActivatedByTeamTwo == false) {
										StartCoroutine ("SetupTimer");
										isActivatedByTeamOne = true;
										playerOneIsBuffed = true;
										tailSlapDmg = coll.GetComponent<PlayerStats> ().tailSlapDamage;
										boomnanaDmg = coll.GetComponent<PlayerStats> ().boomNanaDamage;
										if (playerOneIsBuffed == true && oneTimeBuffer != false) {
												oneTimeBuffer = false;
												float buffedTailSlapDmg = tailSlapDmg * playerBuffDmg;
												float buffedBoomnanaDmg = boomnanaDmg * playerBuffDmg;
												coll.GetComponent<PlayerStats> ().tailSlapDamage = buffedTailSlapDmg;
												coll.GetComponent<PlayerStats> ().boomNanaDamage = buffedBoomnanaDmg;
												coll.GetComponent<PlayerStats> ().trapBeeHiveBuffed = true;
												StartCoroutine ("OneNoBuff");
												StartCoroutine ("AvailableWaitTimer");
										}
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
										playerTwoIsBuffed = true;
										tailSlapDmg = coll.GetComponent<PlayerStats> ().tailSlapDamage;
										boomnanaDmg = coll.GetComponent<PlayerStats> ().boomNanaDamage;
										if (playerTwoIsBuffed == true && oneTimeBuffer != false) {
												oneTimeBuffer = false;
												float buffedTailSlapDmg = tailSlapDmg * playerBuffDmg;
												float buffedBoomnanaDmg = boomnanaDmg * playerBuffDmg;
												coll.GetComponent<PlayerStats> ().tailSlapDamage = buffedTailSlapDmg;
												coll.GetComponent<PlayerStats> ().boomNanaDamage = buffedBoomnanaDmg;
												coll.GetComponent<PlayerStats> ().trapBeeHiveBuffed = true;
												StartCoroutine ("TwoNoBuff");
												StartCoroutine ("AvailableWaitTimer");
										}
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

		IEnumerator OneNoBuff ()
		{
				yield return new WaitForSeconds (120f);
				playerOneIsBuffed = false;
		}

		IEnumerator TwoNoBuff ()
		{
				yield return new WaitForSeconds (120f);
				playerTwoIsBuffed = false;
		}

		IEnumerator AvailableWaitTimer ()
		{
				//may have the timer to be the same time as for the expire timer which is 5 min.
				yield return new WaitForSeconds (40f);
				oneTimeBuffer = true;
		}
}
