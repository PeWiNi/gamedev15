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
	GameObject beaconOne;
	GameObject beaconTwo;
	GameObject beaconThree;
	BeaconZone bOne;
	BeaconZone bTwo;
	BeaconZone bThree;

	void Awake ()
	{
		text = GetComponent<Text> ();
	}

	void Start ()
	{
		time = 20;
		beaconOne = GameObject.Find ("BeaconZone01");
		beaconTwo = GameObject.Find ("BeaconZone02");
		beaconThree = GameObject.Find ("BeaconZone03"); 
		bOne = beaconOne.GetComponent<BeaconZone> ();
		bTwo = beaconTwo.GetComponent<BeaconZone> ();
		bThree = beaconThree.GetComponent<BeaconZone> ();
	}


	// Update is called once per frame
	void Update ()
	{
		if (!wonByScore) {
			checkWinningReamByBeacon ();
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

	public void checkWinningReamByBeacon ()
	{
		if (bOne.zoneOneTeamOneActive && bTwo.zoneTwoTeamOneActive && bThree.zoneThreeTeamOneActive) {
			time = 0;
			text.text = "Team one WON!!!";
			wonByBeacon = true;
		} else if (bOne.zoneOneTeamTwoActive && bTwo.zoneTwoTeamTwoActive && bThree.zoneThreeTeamTwoActive) {
			time = 0;
			text.text = "Team two WON!!!";
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
				text.text = "Team one WON!!!";
				wonByScore = true;
			} else if (ScoreOneManager.totalOneScore < ScoreTwoManager.totalTwoScore) {
				text.text = "Team two WON!!!";
				wonByScore = true;
			} 
		}
	}
}