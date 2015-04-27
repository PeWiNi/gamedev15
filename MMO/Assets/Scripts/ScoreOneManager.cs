using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreOneManager : MonoBehaviour
{
	public static float totalOneScore;
	public static float totalKillScore;
	Text textOne;

	void Awake ()
	{
		textOne = GetComponent<Text> ();
		totalOneScore = 0;
        totalKillScore = 0;
        if (MenuScript.hasPickedTeamOne)
            textOne.color = new Color(248f / 255f, 190f / 255f, 2f / 255f, 1f);
	}

	
	// Update is called once per frame
	void Update ()
	{	
		setTeamOneTotalScore (totalOneScore);
		textOne.text = "Team Fish Score: " + totalOneScore;			
	}

	public static void addDeathPoints (float score)
	{
		totalKillScore += score;
		totalOneScore += totalKillScore;
	}
	/// <summary>
	/// Sets the team one total score.
	/// </summary>
	/// <param name="totalScore">Total score.</param>
	public static void setTeamOneTotalScore (float totalScore)
	{
		totalOneScore = totalScore;
		totalOneScore += totalKillScore;
	}
	
}
