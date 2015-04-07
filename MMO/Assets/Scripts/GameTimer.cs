using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
		public static float gameTimerLimit;
		float gameTimer;
		float timeLimit;
		float decreasingTime;
		bool isTimerDecreasing;
		float lastTime = 0;
		GameObject beaconOne;
		GameObject beaconTwo;
		GameObject beaconThree;
		BeaconZone bOne;
		BeaconZone bTwo;
		BeaconZone bThree;

		// Use this for initialization
		void Start ()
		{	
				beaconOne = GameObject.Find ("BeaconZone01");
				beaconTwo = GameObject.Find ("BeaconZone02");
				beaconThree = GameObject.Find ("BeaconZone03");
				bOne = beaconOne.GetComponent<BeaconZone> ();
				bTwo = beaconTwo.GetComponent<BeaconZone> ();
				bThree = beaconThree.GetComponent<BeaconZone> ();
				gameTimer = 1;
				gameTimerLimit = 20;
		}
	
		// Update is called once per frame
		void Update ()
		{
				timeLimit = gameTimerLimit;
				decreaseTime (timeLimit);
		}

		void decreaseTime (float timeLimit)
		{
				checkBeaconActivation ();
				if (gameTimerLimit >= 1) {
						isTimerDecreasing = true;
						if (Time.time > lastTime + 60) {
								if (gameTimerLimit != 0) {
										gameTimerLimit = timeLimit - gameTimer;
										lastTime = Time.time;
								}
								using (var evnt = GameTimerEvent.Create(Bolt.GlobalTargets.Everyone)) {				
										evnt.GameTime = gameTimerLimit; 
								}
						}
						isTimerDecreasing = false;
				}
		}

		void checkBeaconActivation ()
		{
				if (bOne.zoneOneTeamOneActive && bTwo.zoneTwoTeamOneActive && bThree.zoneThreeTeamOneActive || bOne.zoneOneTeamTwoActive && bTwo.zoneTwoTeamTwoActive && bThree.zoneThreeTeamTwoActive) {
						gameTimerLimit = 0;
				}
		}


		

}
