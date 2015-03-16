using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
		public static float time;
		Text text;
	
		void Awake ()
		{
				text = GetComponent<Text> ();
		}

		// Update is called once per frame
		void Update ()
		{
				getGameTimer ();
				if (time >= 0) {
						text.text = "Game Ends In: " + time;
				}
				checkWinningTeam ();
		}

		public float getGameTimer ()
		{
				time = GameTimer.gameTimerLimit;
				return time;
		}
	
		public void checkWinningTeam ()
		{
				if (time == 0) {
						if (ScoreOneManager.oneScore > ScoreTwoManager.twoScore) {
								text.text = "Team one WON!!!";
						} else if (ScoreOneManager.oneScore < ScoreTwoManager.twoScore) {
								text.text = "Team two WON!!!";
						} else if (ScoreOneManager.oneScore == ScoreTwoManager.twoScore) {
								text.text = "Last Team Standing (just an idea ;D)";
						}
				}
		}
}