using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeaconZone : MonoBehaviour
{

		PlayerStats script;
		float gameTimer;
		
		public bool ZoneOneTeamOneActive;
		public bool ZoneTwoTeamOneActive;
		public bool ZoneThreeTeamOneActive;
		public bool ZoneOneTeamTwoActive;
		public bool ZoneTwoTeamTwoActive;
		public bool ZoneThreeTeamTwoActive;
		public static float ZoneOneTeamOneScore;
		public static float ZoneTwoTeamOneScore;
		public static float ZoneThreeTeamOneScore;
		public static float ZoneOneTeamTwoScore;
		public static float ZoneTwoTeamTwoScore;
		public static float ZoneThreeTeamTwoScore;
		public static float TotalScoreTeamOne;
		public static float TotalScoreTeamTwo;
		float increaseScoreValue;
		float score;
		bool BeaconActive;
		string fightZone;
		float TeamOneZoneOneScore;
		float TeamTwoZoneOneScore;
		float TeamOneZoneTwoScore;
		float TeamTwoZoneTwoScore;
		float TeamOneZoneThreeScore;
		float TeamTwoZoneThreeScore;

		// Use this for initialization
		void Start ()
		{
				increaseScoreValue = 15;
				ZoneOneTeamOneActive = false;
				ZoneTwoTeamOneActive = false;
				ZoneThreeTeamOneActive = false;
				ZoneOneTeamTwoActive = false;
				ZoneTwoTeamTwoActive = false;
				ZoneThreeTeamTwoActive = false;
				script = gameObject.GetComponent<PlayerStats> ();
				gameTimer = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
				gameTimer += Time.deltaTime;
		}
	
		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerStay (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneOne", ZoneOneTeamOneScore);
								ZoneOneTeamOneScore = TeamOneZoneOneScore;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneOne", ZoneOneTeamTwoScore);
								ZoneOneTeamTwoScore = TeamTwoZoneOneScore;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneTwo", ZoneTwoTeamOneScore);
								ZoneTwoTeamOneScore = TeamOneZoneTwoScore;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneTwo", ZoneTwoTeamTwoScore);
								ZoneTwoTeamTwoScore = TeamTwoZoneTwoScore;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								StartCoroutine ("addScoreTeamOneZoneThree", ZoneThreeTeamOneScore);
								ZoneThreeTeamOneScore = TeamOneZoneThreeScore;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								StartCoroutine ("addScoreTeamTwoZoneThree", ZoneThreeTeamTwoScore);
								ZoneThreeTeamTwoScore = TeamTwoZoneThreeScore;
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
								ZoneOneTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneOneTeamTwoActive = false;
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneTwoTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneTwoTeamTwoActive = false;				
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone03") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneThreeTeamOneActive = false;
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneThreeTeamTwoActive = false;				
						}
				}
		}
	
		/// <summary>
		/// Adds the score team one zone one.
		/// </summary>
		/// <returns>The score team one zone one.</returns>
		/// <param name="ZoneOneTeamOneScore">Zone one team one score.</param>
		public IEnumerator addScoreTeamOneZoneOne (float ZoneOneTeamOneScore)
		{
				ZoneOneTeamOneActive = true;
				if (ZoneOneTeamTwoActive != true && ZoneOneTeamOneActive == true) {
						yield return new WaitForSeconds (5f);
						TeamOneZoneOneScore = ZoneOneTeamOneScore + increaseScoreValue;
						ZoneOneTeamOneActive = false;
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
		public IEnumerator addScoreTeamOneZoneTwo (float ZoneTwoTeamOneScore)
		{
				ZoneTwoTeamOneActive = true;
				if (ZoneTwoTeamTwoActive != true && ZoneTwoTeamOneActive == true) {
						yield return new WaitForSeconds (5f);
						TeamOneZoneTwoScore = ZoneTwoTeamOneScore + increaseScoreValue;
						ZoneTwoTeamOneActive = false;
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
		public IEnumerator addScoreTeamOneZoneThree (float ZoneThreeTeamOneScore)
		{
				ZoneThreeTeamOneActive = true;
				if (ZoneThreeTeamTwoActive != true && ZoneThreeTeamOneActive == true) {
						yield return new WaitForSeconds (5f);
						TeamOneZoneThreeScore = ZoneThreeTeamOneScore + increaseScoreValue;
						ZoneThreeTeamOneActive = false;
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
		public IEnumerator addScoreTeamTwoZoneOne (float ZoneOneTeamTwoScore)
		{
				ZoneOneTeamTwoActive = true;
				if (ZoneOneTeamTwoActive == true && ZoneOneTeamOneActive != true) {
						yield return new WaitForSeconds (5f);
						TeamTwoZoneOneScore = ZoneOneTeamTwoScore + increaseScoreValue;
						ZoneOneTeamTwoActive = false;
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
		public IEnumerator addScoreTeamTwoZoneTwo (float ZoneTwoTeamTwoScore)
		{
				ZoneTwoTeamTwoActive = true;
				if (ZoneTwoTeamTwoActive == true && ZoneTwoTeamOneActive != true) {
						yield return new WaitForSeconds (5f);
						TeamTwoZoneTwoScore = ZoneTwoTeamTwoScore + increaseScoreValue;
						ZoneTwoTeamTwoActive = false;
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
		public IEnumerator addScoreTeamTwoZoneThree (float ZoneThreeTeamTwoScore)
		{
				ZoneThreeTeamTwoActive = true;
				if (ZoneThreeTeamTwoActive == true && ZoneThreeTeamOneActive != true) {
						yield return new WaitForSeconds (5f);
						TeamTwoZoneThreeScore = ZoneThreeTeamTwoScore + increaseScoreValue;
						ZoneThreeTeamTwoActive = false;
				} else {
						// DO NOTHING!
						//	Debug.Log ("Fight team one!");
				}
		}
		
}
