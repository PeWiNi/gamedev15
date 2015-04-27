using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTwoManager : MonoBehaviour
{
	public static float totalTwoScore;
	public static float totalKillScore;
	Text textTwo;


	// Use this for initialization
	void Start ()
	{
		textTwo = GetComponent<Text> ();
		totalTwoScore = 0;
		totalKillScore = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		setTeamTwoTotalScore (totalTwoScore);
		textTwo.text = "Team Banana Score: " + totalTwoScore;
	}

	public static void addDeathPoints (float score)
	{
		totalKillScore += score;
		totalTwoScore += totalKillScore;
	}
	/// <summary>
	/// Sets the team two total score.
	/// </summary>
	/// <param name="totalScore">Total score.</param>
	public static void setTeamTwoTotalScore (float totalScore)
	{
		totalTwoScore = totalScore;
		totalTwoScore += totalKillScore;
	}
}

