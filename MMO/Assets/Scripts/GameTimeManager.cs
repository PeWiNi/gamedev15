using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
		public static float time;
		Text text;
		bool isSlowed;
	
		void Awake ()
		{
				text = GetComponent<Text> ();
		}

		void Start ()
		{
				time = 40;
		}

		// Update is called once per frame
		void Update ()
		{
				setGameTimer (time);
				if (time >= 0) {
						text.text = "Game Ends In: " + time;
				}
				checkWinningTeam ();
		}

		/// <summary>
		/// Sets the game timer.
		/// </summary>
		/// <param name="timer">Timer.</param>
		public static void setGameTimer (float timer)
		{
				time = timer;
		}

		/// <summary>
		/// Checks the winning team.
		/// </summary>
		public void checkWinningTeam ()
		{
				if (time == 0) {
						if (ScoreOneManager.totalOneScore > ScoreTwoManager.totalTwoScore) {
								text.text = "Team one WON!!!";
						} else if (ScoreOneManager.totalOneScore < ScoreTwoManager.totalTwoScore) {
								text.text = "Team two WON!!!";
						} else if (ScoreOneManager.totalOneScore == ScoreTwoManager.totalTwoScore) {
								text.text = "Last Team Standing (just an idea ;D)";
						}
				}
		}
}