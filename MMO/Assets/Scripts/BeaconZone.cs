using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeaconZone : MonoBehaviour
{
		public bool zoneOneTeamOneActive;
		public bool zoneTwoTeamOneActive;
		public bool zoneThreeTeamOneActive;
		public bool zoneOneTeamTwoActive;
		public bool zoneTwoTeamTwoActive;
		public bool zoneThreeTeamTwoActive;
		public static float zoneOneTeamOneScore;
		public static float zoneTwoTeamOneScore;
		public static float zoneThreeTeamOneScore;
		public static float zoneOneTeamTwoScore;
		public static float zoneTwoTeamTwoScore;
		public static float zoneThreeTeamTwoScore;
		public static float totalScoreTeamOne;
		public static float totalScoreTeamTwo;
		float increaseScoreValue;
		float teamOneZoneOneScore;
		float teamTwoZoneOneScore;
		float teamOneZoneTwoScore;
		float teamTwoZoneTwoScore;
		float teamOneZoneThreeScore;
		float teamTwoZoneThreeScore;
		float lastTimeOne = 0;
		float lastTimeTwo = 0;
		float activeTime = 600f;
		public float beaconOneActivationTimer;
		public float beaconTwoActivationTimer;
		public float beaconThreeActivationTimer;
		public float beaconOneTimer;
		public float beaconTwoTimer;
		public float beaconThreeTimer;
		//beacon health stats
		public float beaconHealth;
		public float beaconOneHealthTeamOne;
		public float beaconTwoHealthTeamOne;
		public float beaconThreeHealthTeamOne;
		public float beaconOneHealthTeamTwo;
		public float beaconTwoHealthTeamTwo;
		public float beaconThreeHealthTeamTwo; 


		// Use this for initialization
		void Start ()
		{
				increaseScoreValue = 1;
				zoneOneTeamOneActive = false;
				zoneTwoTeamOneActive = false;
				zoneThreeTeamOneActive = false;
				zoneOneTeamTwoActive = false;
				zoneTwoTeamTwoActive = false;
				zoneThreeTeamTwoActive = false;
		}


		void Update ()
		{
				calculateTeamOneScore ();
				calculateTeamTwoScore ();
				CheckForBeaconActivation ();
		}

		void CheckForBeaconActivation ()
		{
				if (zoneOneTeamOneActive == true) {
						if (Time.time - beaconOneTimer <= beaconOneActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneOne", zoneOneTeamOneScore);
								zoneOneTeamOneScore = teamOneZoneOneScore;
						} else if (Time.time - beaconOneTimer >= beaconOneActivationTimer) {
								zoneOneTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				} else if (zoneOneTeamTwoActive == true) {
						if (Time.time - beaconOneTimer <= beaconOneActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneOne", zoneOneTeamTwoScore);
								zoneOneTeamTwoScore = teamTwoZoneOneScore;
						} else if (Time.time - beaconOneTimer >= beaconOneActivationTimer) {
								zoneOneTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				}
				if (zoneTwoTeamOneActive == true) {
						if (Time.time - beaconTwoTimer <= beaconTwoActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneTwo", zoneTwoTeamOneScore);
								zoneTwoTeamOneScore = teamOneZoneTwoScore;
						} else if (Time.time - beaconTwoTimer >= beaconTwoActivationTimer) {
								zoneTwoTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				} else if (zoneTwoTeamTwoActive == true) {
						if (Time.time - beaconTwoTimer <= beaconTwoActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneTwo", zoneTwoTeamTwoScore);
								zoneTwoTeamTwoScore = teamTwoZoneTwoScore;
						} else if (Time.time - beaconTwoTimer >= beaconTwoActivationTimer) {
								zoneTwoTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				}
				if (zoneThreeTeamOneActive == true) {
						if (Time.time - beaconThreeTimer <= beaconThreeActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneThree", zoneThreeTeamOneScore);
								zoneThreeTeamOneScore = teamOneZoneThreeScore;
						} else if (Time.time - beaconThreeTimer >= beaconThreeActivationTimer) {
								zoneThreeTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				} else if (zoneThreeTeamTwoActive == true) {
						if (Time.time - beaconThreeTimer <= beaconThreeActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneThree", zoneThreeTeamTwoScore);
								zoneThreeTeamTwoScore = teamTwoZoneThreeScore;
						} else if (Time.time - beaconThreeTimer >= beaconThreeActivationTimer) {
								zoneThreeTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
						}
				}
		}

		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerEnter (Collider coll)
		{
				IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
				calculateTeamOneScore ();
				calculateTeamTwoScore ();
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconOneCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconOne = be;
																evnt.ZoneOneTeamOne = true;
																evnt.ZoneOneTeamTwo = false;
																evnt.BeaconOneTimer = Time.time;
																evnt.ActiveOneTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//beaconOneTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconOneCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconOne = be;
																evnt.ZoneOneTeamTwo = true;
																evnt.ZoneOneTeamOne = false;
																evnt.BeaconOneTimer = Time.time;
																evnt.ActiveOneTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//	beaconOneTimer = Time.time;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconTwoCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconTwo = be;
																evnt.ZoneTwoTeamOne = true;
																evnt.ZoneTwoTeamTwo = false;
																evnt.BeaconTwoTimer = Time.time;
																evnt.ActiveTwoTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//beaconTwoTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconTwoCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconTwo = be;
																evnt.ZoneTwoTeamTwo = true;
																evnt.ZoneTwoTeamOne = false;
																evnt.BeaconTwoTimer = Time.time;
																evnt.ActiveTwoTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//beaconTwoTimer = Time.time;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconThreeCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconThree = be;
																evnt.ZoneThreeTeamOne = true;
																evnt.ZoneThreeTeamTwo = false;
																evnt.BeaconThreeTimer = Time.time;
																evnt.ActiveThreeTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//beaconThreeTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								while (entities.MoveNext()) {
										if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
												BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
												// Create Event and use the be, if it is the one that is colliding.
												if (be.gameObject == this.gameObject) {
														using (var evnt = BeaconThreeCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
																evnt.BeaconThree = be;
																evnt.ZoneThreeTeamTwo = true;
																evnt.ZoneThreeTeamOne = false;
																evnt.BeaconThreeTimer = Time.time;
																evnt.ActiveThreeTimer = activeTime;
														}
												}
										}
								}
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//beaconThreeTimer = Time.time;
						}
				}
		}


		/// <summary>
		/// Calculates the team one score.
		/// </summary>
		public void calculateTeamOneScore ()
		{
				if (Time.time > lastTimeOne + 2) {
						totalScoreTeamOne = zoneOneTeamOneScore + zoneTwoTeamOneScore + zoneThreeTeamOneScore;
						lastTimeOne = Time.time;
						using (var evnt = TeamOneScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TeamOneTotalScore = totalScoreTeamOne;
						}
				}
		}

		/// <summary>
		/// Calculates the team two score.
		/// </summary>
		public void calculateTeamTwoScore ()
		{
				if (Time.time > lastTimeTwo + 2) {
						totalScoreTeamTwo = zoneOneTeamTwoScore + zoneTwoTeamTwoScore + zoneThreeTeamTwoScore;
						lastTimeTwo = Time.time;
						using (var evnt = TeamTwoScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TeamTwoTotalScore = totalScoreTeamTwo;
						}
				}
		}

		/// <summary>
		/// Adds the score team one zone one.
		/// </summary>
		/// <returns>The score team one zone one.</returns>
		/// <param name="ZoneOneTeamOneScore">Zone one team one score.</param>
		public IEnumerator addScoreTeamOneZoneOne (float zoneOneTeamOneScore)
		{
				if (zoneOneTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneOneScore = zoneOneTeamOneScore + increaseScoreValue;
						}
				}
		}

		/// <summary>
		/// Adds the score team one zone two.
		/// </summary>
		/// <returns>The score team one zone two.</returns>
		/// <param name="ZoneTwoTeamOneScore">Zone two team one score.</param>
		public IEnumerator addScoreTeamOneZoneTwo (float zoneTwoTeamOneScore)
		{
				if (zoneTwoTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneTwoScore = zoneTwoTeamOneScore + increaseScoreValue;
						}
				} 
		}


		/// <summary>
		/// Adds the score team one zone three.
		/// </summary>
		/// <returns>The score team one zone three.</returns>
		/// <param name="ZoneThreeTeamOneScore">Zone three team one score.</param>
		public IEnumerator addScoreTeamOneZoneThree (float zoneThreeTeamOneScore)
		{
				if (zoneThreeTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneThreeScore = zoneThreeTeamOneScore + increaseScoreValue;
						}
				} 
		}


		/// <summary>
		/// Adds the score team two zone one.
		/// </summary>
		/// <returns>The score team two zone one.</returns>
		/// <param name="ZoneOneTeamTwoScore">Zone one team two score.</param>
		public IEnumerator addScoreTeamTwoZoneOne (float zoneOneTeamTwoScore)
		{
				if (zoneOneTeamTwoActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneOneScore = zoneOneTeamTwoScore + increaseScoreValue;
						}
				}
		}


		/// <summary>
		/// Adds the score team two zone two.
		/// </summary>
		/// <returns>The score team two zone two.</returns>
		/// <param name="ZoneTwoTeamTwoScore">Zone two team two score.</param>
		public IEnumerator addScoreTeamTwoZoneTwo (float zoneTwoTeamTwoScore)
		{
				if (zoneTwoTeamTwoActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneTwoScore = zoneTwoTeamTwoScore + increaseScoreValue;
						}
				} 
		}


		/// <summary>
		/// Adds the score team two zone three.
		/// </summary>
		/// <returns>The score team two zone three.</returns>
		/// <param name="ZoneThreeTeamTwoScore">Zone three team two score.</param>
		public IEnumerator addScoreTeamTwoZoneThree (float zoneThreeTeamTwoScore)
		{
				if (zoneThreeTeamTwoActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneThreeScore = zoneThreeTeamTwoScore + increaseScoreValue;
								Debug.Log (teamTwoZoneThreeScore.ToString ());
						}
				}
		}
}
