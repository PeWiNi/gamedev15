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
		float beaconActivationTimer = 60;	
		float beaconOneTimer;
		float beaconTwoTimer;
		float beaconThreeTimer;
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
				if (zoneOneTeamOneActive == true) {
						if (Time.time - beaconOneTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneOne", zoneOneTeamOneScore);
								zoneOneTeamOneScore = teamOneZoneOneScore;
						} else {
								zoneOneTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
				if (zoneTwoTeamOneActive == true) {
						if (Time.time - beaconTwoTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneTwo", zoneTwoTeamOneScore);
								zoneTwoTeamOneScore = teamOneZoneTwoScore;
						} else {
								zoneTwoTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
				if (zoneThreeTeamOneActive == true) {
						if (Time.time - beaconThreeTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamOneZoneThree", zoneThreeTeamOneScore);
								zoneThreeTeamOneScore = teamOneZoneThreeScore;
						} else {
								zoneThreeTeamOneActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
				if (zoneOneTeamTwoActive == true) {
						if (Time.time - beaconOneTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneOne", zoneOneTeamTwoScore);
								zoneOneTeamTwoScore = teamTwoZoneOneScore;
						} else {
								zoneOneTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
				if (zoneTwoTeamTwoActive == true) {
						if (Time.time - beaconOneTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneTwo", zoneTwoTeamTwoScore);
								zoneTwoTeamTwoScore = teamTwoZoneTwoScore;
						} else {
								zoneTwoTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
				if (zoneThreeTeamTwoActive == true) {
						if (Time.time - beaconOneTimer <= beaconActivationTimer) {
								StartCoroutine ("addScoreTeamTwoZoneThree", zoneThreeTeamTwoScore);
								zoneThreeTeamTwoScore = teamTwoZoneThreeScore;
						} else {
								zoneThreeTeamTwoActive = false;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 0;
								//this.gameObject.collider.isTrigger = true;
						}
				}
		}


		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerEnter (Collider coll)
		{
				calculateTeamOneScore ();
				calculateTeamTwoScore ();
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneOneTeamOneActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//this.gameObject.collider.isTrigger = false;
								beaconOneTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneOneTeamTwoActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//this.gameObject.collider.isTrigger = false;
								beaconOneTimer = Time.time;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneTwoTeamOneActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;//this.gameObject.collider.isTrigger = false;
								beaconTwoTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneTwoTeamTwoActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//this.gameObject.collider.isTrigger = false;
								beaconTwoTimer = Time.time;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneThreeTeamOneActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//this.gameObject.collider.isTrigger = false;
								beaconThreeTimer = Time.time;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2 && coll.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
								zoneThreeTeamTwoActive = true;
								this.gameObject.GetComponent<BeaconZone> ().beaconHealth = 2000;
								//this.gameObject.collider.isTrigger = false;
								beaconThreeTimer = Time.time;
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
				if (zoneOneTeamTwoActive != true && zoneOneTeamOneActive == true) {
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
				if (zoneTwoTeamTwoActive != true && zoneTwoTeamOneActive == true) {
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
				if (zoneThreeTeamTwoActive != true && zoneThreeTeamOneActive == true) {
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
				if (zoneOneTeamTwoActive == true && zoneOneTeamOneActive != true) {
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
				if (zoneTwoTeamTwoActive == true && zoneTwoTeamOneActive != true) {
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
				if (zoneThreeTeamTwoActive == true && zoneThreeTeamOneActive != true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneThreeScore = zoneThreeTeamTwoScore + increaseScoreValue;
						}
				}
		}
}
