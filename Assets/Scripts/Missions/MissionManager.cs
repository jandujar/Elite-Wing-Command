﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
	public enum MissionType
	{
		Base_Attack,
		Base_Defense,
		Base_vs_Base
	}

	public enum PlayerObjectivesType
	{
		Complete_Ally_Objectives,
		Prevent_Enemy_Objectives,
		Complete_and_Prevent
	}
	
	[SerializeField] PlayerSpawner playerSpawner;
	public MissionType missionType;
	public PlayerObjectivesType playerObjectivesType;
	List<string> allyObjectivesInScene;
	bool gameOver = false;
	public List<string> AllyObjectivesList { get { return allyObjectivesInScene; }}

	void Start()
	{
		switch(missionType.ToString())
		{
		case "Base_Attack":
			BaseAttack();
			break;
		case "Base_Defense":
			Debug.Log("Mission Type: Base Defense");
			break;
		case "Base_vs_Base":
			Debug.Log("Mission Type: Base vs Base");
			break;
		}
	}

	void Update()
	{
		if (!gameOver)
		{
			switch(playerObjectivesType.ToString())
			{
			case "Complete_Ally_Objectives":
				if (AllyObjectivesList.Count > 0)
				{
					if (playerSpawner.GameOver)
					{
						Debug.Log("GAME OVER");
						StartCoroutine(LoadMenu());
						gameOver = true;
					}
				}
				else
				{
					Debug.Log("MISSION COMPLETE");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				break;
			case "Prevent_Enemy_Objectives":
				Debug.Log("Mission Type: Base Defense");
				break;
			case "Complete_and_Prevent":
				Debug.Log("Mission Type: Base vs Base");
				break;
			}
		}
	}

	void BaseAttack()
	{
		allyObjectivesInScene = new List<string>();
		GameObject[] objectives = GameObject.FindGameObjectsWithTag("Enemy");

		if (objectives.Length > 0)
		{
			foreach (GameObject objective in objectives)
			{
				var unitTag = objective.GetComponent<ObjectIdentifier>();

				if (unitTag != null && unitTag.ObjectType == "Ally Objective")
				{
					allyObjectivesInScene.Add(objective.transform.name);
				}
			}
		}
		else
			Debug.LogError("No Ally Objectives!");

		Debug.Log("Remaining Objectives: " + AllyObjectivesList.Count);
	}

	public void AllyObjectiveDestroyed(string objectiveName)
	{
		allyObjectivesInScene.Remove(objectiveName);
		Debug.Log("Remaining Objectives: " + AllyObjectivesList.Count);
	}

	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(0);
	}
}