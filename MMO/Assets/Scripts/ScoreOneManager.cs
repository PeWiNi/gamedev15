using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreOneManager : MonoBehaviour
{
		public static float oneScore;
		Text textOne;


		void Awake ()
		{
				textOne = GetComponent<Text> ();
				oneScore = 0;
		}

	
		// Update is called once per frame
		void Update ()
		{	
				TeamOneTotalScore ();
				textOne.text = "Team One Score: " + oneScore;			
		}

		public float TeamOneTotalScore ()
		{
				oneScore = BeaconZone.ZoneOneTeamOneScore + BeaconZone.ZoneTwoTeamOneScore + BeaconZone.ZoneThreeTeamOneScore;
				return oneScore;
		}
	
}
