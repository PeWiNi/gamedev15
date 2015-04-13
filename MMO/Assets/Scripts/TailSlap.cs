using UnityEngine;
using System.Collections;

public class TailSlap : MonoBehaviour
{

		bool available = true;
		float lastUsed;
		StateController sc;
		PlayerStats ps;
		TestPlayerBehaviour tpb;

		void start ()
		{
				sc = this.gameObject.GetComponentInParent<StateController> ();
				ps = this.gameObject.GetComponentInParent<PlayerStats> ();
				tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
		}

		void Update ()
		{
				if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats> ().tailSlapCooldown) {
						available = true;
				}
		}

		void OnTriggerStay (Collider coll)
		{

				IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
				if (coll.gameObject.tag == "player") {
						sc = gameObject.GetComponentInParent<StateController> ();
						ps = gameObject.GetComponentInParent<PlayerStats> ();
						if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
					
								GameObject go = GameObject.Find ("Canvas");
								HUDScript hs = go.GetComponentInChildren<HUDScript> ();
				
								hs.dmgDealt.text = "Miss";
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.

						if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
														if (available) {
																Debug.Log ("SLAPPING DA TAIL");
																if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != this.gameObject.GetComponentInParent<PlayerStats> ().teamNumber) {
																		// deal full damage!!!
																		using (var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone)) {
																				hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																				evnt.TargEnt = be; 
																				evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																		}
																} else { // check for friendly player, deal 50% dmg.
																		// deal half damage!!!
																		using (var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone)) {
																				hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage / 2;
																				evnt.TargEnt = be;
																				evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage / 2;
																		}
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
				if (coll.gameObject.name == "BeaconZone01") {
						sc = gameObject.GetComponentInParent<StateController> ();
						ps = gameObject.GetComponentInParent<PlayerStats> ();
						if (ps.teamNumber == 1) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneOneTeamTwoActive == true) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
						
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				//Debug.Log ("loloololollloo");
																				//Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
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
						if (ps.teamNumber == 2) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneOneTeamOneActive) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
								
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																				}
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
				if (coll.gameObject.name == "BeaconZone02") {
						sc = gameObject.GetComponentInParent<StateController> ();
						ps = gameObject.GetComponentInParent<PlayerStats> ();	
						if (ps.teamNumber == 1) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneTwoTeamTwoActive) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
								
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
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
						if (ps.teamNumber == 2) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneTwoTeamOneActive) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
								
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																				}
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
				if (coll.gameObject.name == "BeaconZone03") {
						sc = gameObject.GetComponentInParent<StateController> ();
						ps = gameObject.GetComponentInParent<PlayerStats> ();	
						if (ps.teamNumber == 1) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneThreeTeamTwoActive) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
								
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
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
						if (ps.teamNumber == 2) {
								if (coll.gameObject.GetComponent<BeaconZone> ().zoneThreeTeamOneActive) {
										if (Input.GetKeyDown (KeyCode.Mouse0) && ! sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
												GameObject go = GameObject.Find ("Canvas");
												HUDScript hs = go.GetComponentInChildren<HUDScript> ();
						
												hs.dmgDealt.text = "Miss";
												while (entities.MoveNext()) {
														if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
																BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
																// Create Event and use the be, if it is the one that is colliding.
								
								if (be.gameObject == coll.gameObject && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().gameObject) { // Check for enemy, deal full damage
																		if (available) {
																				Debug.Log ("SLAPPING DA TAIL");
																				using (var evnt = BeaconEvent.Create(Bolt.GlobalTargets.Everyone)) {
																						hs.dmgDealt.text = "" + this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																						evnt.TargEnt = be; 
																						evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats> ().tailSlapDamage;
																				}
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

}
