﻿using UnityEngine;
using System.Collections;

public class TotalScoreArcade : MonoBehaviour
{
	[SerializeField] UILabel totalScoreObject;
	[SerializeField] ScoreTextNumbersArcade scoreNumbersArcade;
	int totalScore;

	void Start ()
	{
		StartCoroutine(SetTotalScore());
	}

	IEnumerator SetTotalScore()
	{
		yield return new WaitForSeconds(0.1f);
		totalScore = scoreNumbersArcade.TotalScore;
		totalScoreObject.text = totalScore.ToString();
	}
}
