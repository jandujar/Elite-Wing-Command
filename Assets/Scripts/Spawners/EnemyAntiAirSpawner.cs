﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAntiAirSpawner : TargetSpawner
{
	[SerializeField] EnemyAntiAir enemyAntiAir;
	Vector3 spawnLocation;

	void Start()
	{
		spawnLocation = transform.position;
		TargetPrefab = enemyAntiAir.UnitPrefab;
		MaxInGame = enemyAntiAir.MaxInGame;
		TotalRespawns = enemyAntiAir.TotalRespawns;
		SquadSpawn = enemyAntiAir.SquadSpawn;
		SquadSpawnSize = enemyAntiAir.SquadSpawnSize;
	}

	public override void SpawnUnit()
	{
		RespawnNumber++;
		if (RespawnNumber > TotalRespawns)
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
		
		Vector3 randomPosition = new Vector3(Random.Range(spawnLocation.x - 10f, spawnLocation.x + 10f), yPos, Random.Range(spawnLocation.z - 10f, spawnLocation.z + 10f));
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var enemyClone = Instantiate(TargetPrefab, randomPosition, randomRotation);
		enemyClone.name = TargetPrefab.name + " " + NextNameNumber;
		EnemiesInScene.Add(enemyClone.name);
		NextNameNumber++;
	}
}
