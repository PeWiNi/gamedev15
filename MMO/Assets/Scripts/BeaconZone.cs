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
	
		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerStay (Collider coll)
		{
				calculateTeamOneScore ();
				calculateTeamTwoScore ();
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneOne", zoneOneTeamOneScore);
								zoneOneTeamOneScore = teamOneZoneOneScore;
								
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneOne", zoneOneTeamTwoScore);
								zoneOneTeamTwoScore = teamTwoZoneOneScore;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneTwo", zoneTwoTeamOneScore);
								zoneTwoTeamOneScore = teamOneZoneTwoScore;

						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneTwo", zoneTwoTeamTwoScore);
								zoneTwoTeamTwoScore = teamTwoZoneTwoScore;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneThree", zoneThreeTeamOneScore);
								zoneThreeTeamOneScore = teamOneZoneThreeScore;

						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneThree", zoneThreeTeamTwoScore);
								zoneThreeTeamTwoScore = teamTwoZoneThreeScore;
						}
				}
		}
	

		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerExit (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								zoneOneTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								zoneOneTeamTwoActive = false;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								zoneTwoTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								zoneTwoTeamTwoActive = false;				
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								zoneThreeTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								zoneThreeTeamTwoActive = false;				
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
				zoneOneTeamOneActive = true;
				if (zoneOneTeamTwoActive != true && zoneOneTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneOneScore = zoneOneTeamOneScore + increaseScoreValue;
						}
						zoneOneTeamOneActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team two!");
				}
		}

		/// <summary>
		/// Adds the score team one zone two.
		/// </summary>
		/// <returns>The score team one zone two.</returns>
		/// <param name="ZoneTwoTeamOneScore">Zone two team one score.</param>
		public IEnumerator addScoreTeamOneZoneTwo (float zoneTwoTeamOneScore)
		{
				zoneTwoTeamOneActive = true;
				if (zoneTwoTeamTwoActive != true && zoneTwoTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneTwoScore = zoneTwoTeamOneScore + increaseScoreValue;
						}
						zoneTwoTeamOneActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team two!");
				}
		}


		/// <summary>
		/// Adds the score team one zone three.
		/// </summary>
		/// <returns>The score team one zone three.</returns>
		/// <param name="ZoneThreeTeamOneScore">Zone three team one score.</param>
		public IEnumerator addScoreTeamOneZoneThree (float zoneThreeTeamOneScore)
		{
				zoneThreeTeamOneActive = true;
				if (zoneThreeTeamTwoActive != true && zoneThreeTeamOneActive == true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamOneZoneThreeScore = zoneThreeTeamOneScore + increaseScoreValue;
						}
						zoneThreeTeamOneActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team two!");
				}
		}


		/// <summary>
		/// Adds the score team two zone one.
		/// </summary>
		/// <returns>The score team two zone one.</returns>
		/// <param name="ZoneOneTeamTwoScore">Zone one team two score.</param>
		public IEnumerator addScoreTeamTwoZoneOne (float zoneOneTeamTwoScore)
		{
				zoneOneTeamTwoActive = true;
				if (zoneOneTeamTwoActive == true && zoneOneTeamOneActive != true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneOneScore = zoneOneTeamTwoScore + increaseScoreValue;
						}
						zoneOneTeamTwoActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team one!");
				}
		}


		/// <summary>
		/// Adds the score team two zone two.
		/// </summary>
		/// <returns>The score team two zone two.</returns>
		/// <param name="ZoneTwoTeamTwoScore">Zone two team two score.</param>
		public IEnumerator addScoreTeamTwoZoneTwo (float zoneTwoTeamTwoScore)
		{
				zoneTwoTeamTwoActive = true;
				if (zoneTwoTeamTwoActive == true && zoneTwoTeamOneActive != true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneTwoScore = zoneTwoTeamTwoScore + increaseScoreValue;
						}
						zoneTwoTeamTwoActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team one!");
				}
		}


		/// <summary>
		/// Adds the score team two zone three.
		/// </summary>
		/// <returns>The score team two zone three.</returns>
		/// <param name="ZoneThreeTeamTwoScore">Zone three team two score.</param>
		public IEnumerator addScoreTeamTwoZoneThree (float zoneThreeTeamTwoScore)
		{
				zoneThreeTeamTwoActive = true;
				if (zoneThreeTeamTwoActive == true && zoneThreeTeamOneActive != true) {
						yield return new WaitForSeconds (4f);
						if (GameTimeManager.time >= 1) {
								teamTwoZoneThreeScore = zoneThreeTeamTwoScore + increaseScoreValue;
						}
						zoneThreeTeamTwoActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team one!");
				}
		}
		
}
