using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTwoManager : MonoBehaviour
{
		public static float twoScore;
		Text textTwo;


		// Use this for initialization
		void Start ()
		{
				textTwo = GetComponent<Text> ();
				twoScore = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
				TeamTwoTotalScore ();
				textTwo.text = "Team Two Score: " + twoScore;
		}

		public float TeamTwoTotalScore ()
		{
				twoScore = BeaconZone.ZoneOneTeamTwoScore + BeaconZone.ZoneTwoTeamTwoScore + BeaconZone.ZoneThreeTeamTwoScore;	
				return twoScore;
		}
}

