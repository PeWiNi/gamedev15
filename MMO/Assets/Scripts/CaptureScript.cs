using UnityEngine;
using System.Collections;

public class CaptureScript : MonoBehaviour
{


		private int scoreTeamOne = 0;
		private int scoreTeamTwo = 0;
		private int scoreThreshold = 1;
		private int winningTeam = 0;
		public int teamNumber;
		public GameObject theNut;
		//	GameObject theHolder;
		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		void OnTriggerEnter (Collider coll)
		{
				//Debug.Log (theNut.name);
				if (coll.gameObject.name == "Coconut") {
						//Debug.Log("IT MATCHES");
						Coconut nutScript = coll.GetComponent<Coconut> ();
						if (nutScript.getHolder ().GetComponent<PlayerStats> ().teamNumber == teamNumber) {
								int winner = addScore (nutScript.getHolder ().GetComponent<PlayerStats> ().teamNumber);
								if (winner != 0) {
										Debug.Log ("Winner = " + winner + "!");
								}
								Vector3 origin = nutScript.startPos;
								//setScore +1 for ScoreScript.score(theHolder.teamNumber, 1);
								nutScript.removeCapture (origin);
								nutScript.transform.position = origin;
						}

				}
		}

		public int addScore (int teamNo)
		{
				if (teamNo == 1) {
						Debug.Log (addScoreTeamOne ());
						return addScoreTeamOne ();
				} else if (teamNo == 2) {
						Debug.Log (addScoreTeamTwo ());
						return addScoreTeamTwo ();
				}
				return 0;
		}

		public int addScoreTeamOne ()
		{
				scoreTeamOne++;
				return checkForWinner ();
		}

		public int addScoreTeamTwo ()
		{
				scoreTeamTwo++;
				return checkForWinner ();
		}

		int checkForWinner ()
		{
				bool winner = false;
				if (scoreTeamOne >= scoreThreshold) {
						winningTeam = 1;
						winner = true;
				}
				if (scoreTeamTwo >= scoreThreshold) {
						winningTeam = 2;
						winner = true;
				}
				if (winner) {
						return declareWinner (winningTeam);
				}
				return 0;
		}

		int declareWinner (int teamNo)
		{

				scoreTeamOne = 0;
				scoreTeamTwo = 0;
				winningTeam = 0;
				return teamNo;
		}
}