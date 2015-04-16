using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreOneManager : MonoBehaviour
{
	public static float totalOneScore;
	Text textOne;

	void Awake ()
	{
		textOne = GetComponent<Text> ();
		totalOneScore = 0;
	}

	
	// Update is called once per frame
	void Update ()
	{	
		setTeamOneTotalScore (totalOneScore);
		textOne.text = "Team One Score: " + totalOneScore;			
	}

	/// <summary>
	/// Sets the team one total score.
	/// </summary>
	/// <param name="totalScore">Total score.</param>
	public static void setTeamOneTotalScore (float totalScore)
	{
		totalOneScore = totalScore;
	}
	
}
