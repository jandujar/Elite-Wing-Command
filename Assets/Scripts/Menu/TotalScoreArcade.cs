﻿using UnityEngine;
using System.Collections;

public class TotalScoreArcade : MonoBehaviour
{
	[SerializeField] UILabel totalScoreObject;
	[SerializeField] ScoreTextNumbersArcade scoreNumbersArcade;
	int totalScore;

	void Start ()
	{
		Cursor.visible = true;
		StartCoroutine(SetTotalScore());
	}

	IEnumerator SetTotalScore()
	{
		yield return new WaitForSeconds(0.1f);
		totalScore = scoreNumbersArcade.TotalScore;
		totalScoreObject.text = totalScore.ToString("N0");

		if (Everyplay.IsRecording())
			Everyplay.SetMetadata("score", totalScore.ToString("N0"));

		PlayerPrefs.Save();
		var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");

		if (gameCenterObject != null)
		{
			EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();

			gameCenterScript.StoreAndSubmitScore(totalScore);
			gameCenterScript.SubmitAchievement("complete_arcade_mode_round", 100f);

			float totalAirUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Air Units Destroyed", 0f);
			float airUnitsDestroyedProgress = (totalAirUnitsDestroyed/5000f) * 100f;
			gameCenterScript.SubmitAchievement("destroy_5000_air_units", airUnitsDestroyedProgress);
			
			float totalGroundUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Ground Units Destroyed", 0f);
			float groundUnitsDestroyedProgress = (totalGroundUnitsDestroyed/1000f) * 100f;
			gameCenterScript.SubmitAchievement("destroy_1000_ground_units", groundUnitsDestroyedProgress);
		}
	}
}
