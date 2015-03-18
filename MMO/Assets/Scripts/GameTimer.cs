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
				decreaseTime (timeLimit);
		}

		void decreaseTime (float timeLimit)
		{
				if (gameTimerLimit >= 1) {
						isTimerDecreasing = true;
						if (Time.time > lastTime + 60) {
								gameTimerLimit = timeLimit - gameTimer;
								lastTime = Time.time;
								using (var evnt = GameTimerEvent.Create(Bolt.GlobalTargets.Everyone)) {				
										evnt.GameTime = gameTimerLimit; 
								}
						}
						isTimerDecreasing = false;
				}
		}


		

}
