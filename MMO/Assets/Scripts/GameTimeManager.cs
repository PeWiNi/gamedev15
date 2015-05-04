using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
	public static float time;
	Text text;
	bool isSlowed;
	bool wonByBeacon = false;
	bool wonByScore = false;
	GameObject beacon01;
	GameObject beacon02;
	GameObject beacon03;
	GameObject beacon04;

	void Awake ()
	{
		text = GetComponent<Text> ();
	}

	void Start ()
	{
		time = 20;
		beacon01 = GameObject.Find ("BeaconNorth");
		beacon02 = GameObject.Find ("BeaconSouth");
		beacon03 = GameObject.Find ("BeaconWest");
		beacon04 = GameObject.Find ("BeaconEast");
	}


	// Update is called once per frame
	void Update ()
	{
		if (!wonByScore) {
			checkWinningTeamByBeacon ();
		}
		if (!wonByBeacon) {
			if (time > 0) {
				setGameTimer (time);
				text.text = "Game Ends In: " + time + " minutes";
			}
			checkWinningTeamByScore ();
		}
	}

	/// <summary>
	/// Sets the game timer.
	/// </summary>
	/// <param name="timer">Timer.</param>
	public static void setGameTimer (float timer)
	{
		time = timer;
	}

	public void checkWinningTeamByBeacon ()
	{
		if (beacon01.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon02.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon03.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl && beacon04.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl) {
			time = 0;
			text.text = "TEAM FISH WINS, THEY HAVE CAPTURED ALL BEACONS!";
			wonByBeacon = true;
		} else if (beacon01.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon02.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon03.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl && beacon04.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl) {
			time = 0;
			text.text = "TEAM BANANA WINS, THEY HAVE CAPTURED ALL BEACONS!";
			wonByBeacon = true;
		}
	}

	/// <summary>
	/// Checks the winning team.
	/// </summary>
	public void checkWinningTeamByScore ()
	{
		// what should happen if it's a tie?
		if (time == 0) {
			if (ScoreOneManager.totalOneScore > ScoreTwoManager.totalTwoScore) {
				text.text = "TEAM FISH WINS ON POINTS!";
				wonByScore = true;
			} else if (ScoreOneManager.totalOneScore < ScoreTwoManager.totalTwoScore) {
				text.text = "TEAM BANANA WINS ON POINTS!";
				wonByScore = true;
			} 
		}
	}
}