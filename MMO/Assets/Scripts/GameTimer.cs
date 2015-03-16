using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
		public static float gameTimerLimit;
		float gameTimer;
		float timeLimit;
		float decreasingTime;
		public string textTime;
		bool isTimerDecreasing;
	

		// Use this for initialization
		void Start ()
		{	
				gameTimer = 1;
				gameTimerLimit = 40;
		}
	
		// Update is called once per frame
		void Update ()
		{
				timeLimit = gameTimerLimit;
				StartCoroutine ("decreaseTime", timeLimit);
		}

		IEnumerator decreaseTime (float timeLimit)
		{
				if (gameTimerLimit >= 1) {
						isTimerDecreasing = true;
						yield return new WaitForSeconds (1f);
						gameTimerLimit = timeLimit - gameTimer;
						isTimerDecreasing = false;
				}
		}


		

}
