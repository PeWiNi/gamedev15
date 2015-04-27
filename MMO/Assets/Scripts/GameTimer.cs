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
	GameObject beacon01;
	GameObject beacon02;
	GameObject beacon03;
	GameObject beacon04;

	// Use this for initialization
	void Start ()
	{	
		beacon01 = GameObject.Find ("BeaconZone01");
		beacon02 = GameObject.Find ("BeaconZone02");
		beacon03 = GameObject.Find ("BeaconZone03");
		beacon04 = GameObject.Find ("BeaconZone04");
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
		if (beacon01.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon02.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon03.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon04.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl) {
			gameTimerLimit = 0;
		} else if (beacon01.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon02.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon03.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon04.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl) {
			gameTimerLimit = 0;
		}
	}


		

}
