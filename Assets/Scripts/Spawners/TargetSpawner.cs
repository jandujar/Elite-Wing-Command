﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
	[SerializeField] GameObject targetPrefab;
	[SerializeField] int maxInGame = 20;
	[SerializeField] int totalRespawns = 50;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize = 5;
	[SerializeField] bool spawnTurret = false;
	int respawnNumber = 0;
	int respawnSquadCount = 1;
	bool canSpawn = true;
	int nextNameNumber = 1;
	List<string> enemiesInScene;
	public GameObject TargetPrefab { get { return targetPrefab; } set { targetPrefab = value; }}
	public int MaxInGame { get { return maxInGame; } set { maxInGame = value; }}
	public int TotalRespawns { get { return totalRespawns; } set { totalRespawns = value; }}
	public bool SquadSpawn { get { return squadSpawn; } set { squadSpawn = value; }}
	public int SquadSpawnSize { get { return squadSpawnSize; } set { squadSpawnSize = value; }}
	public bool SpawnTurret { get { return spawnTurret; } set { spawnTurret = value; }}
	public int RespawnNumber { get { return respawnNumber; } set { respawnNumber = value; }}
	public int RespawnSquadCount { get { return respawnSquadCount; } set { respawnSquadCount = value; }}
	public bool CanSpawn { get { return canSpawn; } set { canSpawn = value; }}
	public int NextNameNumber { get { return nextNameNumber; } set { nextNameNumber = value; }}
	public float yPos { get; set; }
	public List<string> EnemiesInScene { get { return enemiesInScene; } set { enemiesInScene = value; }}
	
	void Awake()
	{
		EnemiesInScene = new List<string>();
	}
	
	void Update()
	{
		if (canSpawn)
		{
			int totalEnemyCount = EnemiesInScene.Count;

			if (totalEnemyCount < MaxInGame)
				SpawnUnit();
			else
				canSpawn = false;
		}
	}

	public virtual void SpawnUnit()
	{
		respawnNumber++;
		if (respawnNumber > TotalRespawns)
			return;

		if (SpawnTurret)
			yPos = -7.5f;
		else
		{
			float heightRangeSelector = Random.Range(0, 2);

			if (heightRangeSelector == 0)
				yPos = Random.Range(-5, 0);
			else
				yPos = Random.Range(1, 35);
		}

		Vector3 randomPosition = new Vector3(Random.Range(-90f, 90f), yPos, Random.Range(-90f, 90f));
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var enemyClone = Instantiate(TargetPrefab, randomPosition, randomRotation);
		enemyClone.name = TargetPrefab.name + " " + nextNameNumber;
		EnemiesInScene.Add(enemyClone.name);
		nextNameNumber++;
	}

	public void RemoveFromList(string enemyType)
	{
		EnemiesInScene.Remove(enemyType);

		if (SquadSpawn)
		{
			if (respawnSquadCount < SquadSpawnSize)
				respawnSquadCount++;
			else
			{
				canSpawn = true;
				respawnSquadCount = 0;
			}
		}
		else
			canSpawn = true;
	}
}
