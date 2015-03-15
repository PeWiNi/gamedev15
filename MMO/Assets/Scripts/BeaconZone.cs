using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeaconZone : MonoBehaviour
{

		PlayerStats script;
		float gameTimer;
		
		public bool ZoneOneTeamOneActive;
		public bool ZoneTwoTeamOneActive;
		public bool ZoneOneTeamTwoActive;
		public bool ZoneTwoTeamTwoActive;
		public static float ZoneOneTeamOneScore;
		public static float ZoneTwoTeamOneScore;
		public static float ZoneOneTeamTwoScore;
		public static float ZoneTwoTeamTwoScore;
		public static float TotalScoreTeamOne;
		public static float TotalScoreTeamTwo;
		float scoreSpeed = 5;
		float score;
		string fightZone;

		// Use this for initialization
		void Start ()
		{
				ZoneOneTeamOneActive = false;
				ZoneTwoTeamOneActive = false;
				ZoneOneTeamTwoActive = false;
				ZoneTwoTeamTwoActive = false;
				script = gameObject.GetComponent<PlayerStats> ();
				gameTimer = Time.time;
		}
	
		// Update is called once per frame
		void Update ()
		{
				gameTimer += Time.deltaTime;
		}

		void OnTriggerStay (Collider coll)
		{
				if (ZoneOneTeamOneActive == true && ZoneOneTeamTwoActive == true || ZoneTwoTeamOneActive == true && ZoneTwoTeamTwoActive == true) {
						fightZone = "FIGHT!";
				} else {
						addScorePoints ();
				}

				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneOneTeamOneActive = true;
								Debug.Log ("Zone one Team one score: " + ZoneOneTeamOneScore);
								Debug.Log ("" + fightZone);
								Debug.Log ("teamOne active at Zone 1: " + ZoneOneTeamOneActive);
								//addScorePointsTeamOne ();
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneOneTeamTwoActive = true;
								Debug.Log ("Zone one Team one score: " + ZoneOneTeamTwoScore);
								Debug.Log ("teamTwo active at Zone 1: " + ZoneOneTeamTwoActive);
								//addScorePointsTeamTwo ();
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneTwoTeamOneActive = true;
								Debug.Log ("Zone Two Team one score: " + ZoneTwoTeamOneScore);
								Debug.Log ("teamOne active at Zone 2: " + ZoneTwoTeamOneActive);
								//addScorePointsTeamOne ();
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneTwoTeamTwoActive = true;	
								Debug.Log ("teamTwo active at Zone 2: " + ZoneTwoTeamTwoActive);
								//addScorePointsTeamTwo ();
						}
				}
		}

		void OnTriggerExit (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone01") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneOneTeamOneActive = false;
								Debug.Log ("teamOne not active at Zone 1: " + ZoneOneTeamOneActive);
								//addScorePointsTeamOne ();
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneOneTeamTwoActive = false;
								Debug.Log ("teamTwo not active at Zone 1: " + ZoneOneTeamTwoActive);
								//addScorePointsTeamTwo ();
						}
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "BeaconZone02") {
						if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
								ZoneTwoTeamOneActive = false;
								Debug.Log ("teamOne not active at Zone 2: " + ZoneTwoTeamOneActive);
								//addScorePointsTeamOne ();
						} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
								ZoneTwoTeamTwoActive = false;				
								Debug.Log ("teamTwo not active at Zone 2: " + ZoneTwoTeamTwoActive);
								//addScorePointsTeamTwo ();
						}
				}
		}

		void addScorePoints ()
		{
				score = Time.deltaTime * scoreSpeed;
				if (ZoneOneTeamOneActive == true) {
						ZoneOneTeamOneScore += score;
				}
				if (ZoneTwoTeamOneActive == true) {
						ZoneTwoTeamOneScore += score;	
				}
				if (ZoneOneTeamTwoActive == true) {
						ZoneOneTeamTwoScore += score;
				} 
				if (ZoneTwoTeamTwoActive == true) {
						ZoneTwoTeamTwoScore += score;
				}
		}

//		void TotalScoreOne ()
//		{
//				ScoreManager.score = ZoneOneTeamOneScore + ZoneTwoTeamOneScore;
//		}
//
//		void TotalScoreTwo ()
//		{
//				TotalScoreTeamTwo = ZoneOneTeamTwoScore + ZoneTwoTeamTwoScore;
//		}

		
		
}
