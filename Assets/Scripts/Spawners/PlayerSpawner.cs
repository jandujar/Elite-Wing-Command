﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] int totalRespawns = 3;
	[SerializeField] GameObject playerPrefab2Slots;
	[SerializeField] GameObject playerPrefab3Slots;
	[SerializeField] GameObject playerPrefab4Slots;
	[SerializeField] GameObject playerPrefab5Slots;
	GameObject playerPrefab;
	bool gameOver = false;
	int respawnNumber = 1;
	int playerIterations = 1;
	List<string> playerInScene;
	Vector3 spawnPosition;
	Quaternion spawnRotation;
	bool canSpawn = true;
	public bool GameOver { get { return gameOver; }}

	void Awake()
	{
		switch(PlayerPrefs.GetInt("Weapon Slots"))
		{
		case 2:
			playerPrefab = playerPrefab2Slots;
			break;
		case 3:
			playerPrefab = playerPrefab3Slots;
			break;
		case 4:
			playerPrefab = playerPrefab4Slots;
			break;
		case 5:
			playerPrefab = playerPrefab5Slots;
			break;
		default:
			Debug.LogError("Invalid Player Spawn Prefab: " + transform.name);
			playerPrefab = playerPrefab2Slots;
			break;
		}
	}
	
	void Start()
	{
		playerInScene = new List<string>();

		if (missionManager != null)
		{
			spawnPosition = transform.position;
			spawnRotation = transform.rotation;
		}
		else
		{
			spawnPosition = new Vector3(0f, 0f, 0f);
			spawnRotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}
	
	void Update()
	{
		int playerCount = playerInScene.Count;

		if (!gameOver && playerCount < playerIterations && canSpawn)
		{
			SpawnPlayer();
		}
	}

	void SpawnPlayer()
	{
		canSpawn = false;
		var playerClone = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
		MapConstraint mapConstraint = playerClone.GetComponent<MapConstraint>();
		MapAircraftMover mapAircraftMover = playerClone.GetComponentInChildren<MapAircraftMover>();

		if (missionManager != null)
		{
			mapConstraint.enabled = false;
			mapAircraftMover.gameObject.SetActive(false);
		}

		playerClone.name = "Player Aircraft";
		playerInScene.Add(playerClone.name);
		Debug.Log("Lives Remaining: " + ((totalRespawns + 1) - respawnNumber));
	}

	public void PlayerDeath()
	{
		playerInScene.Clear();
		respawnNumber++;
		if (respawnNumber > totalRespawns)
		{
			canSpawn = false;
			Debug.Log("No more lives....");
			gameOver = true;

			if (missionManager == null)
				Application.LoadLevel(0);
		}
		else
			StartCoroutine(RespawnTimer(2f));
	}

	IEnumerator RespawnTimer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		canSpawn = true;
	}
}