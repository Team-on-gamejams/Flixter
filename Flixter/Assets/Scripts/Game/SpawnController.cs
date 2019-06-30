using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpawnController : MonoBehaviour {

	public float[] spawnTime;
	public List<GameObject> EnemySinglePrefab;
	public List<GameObject> EnemyGroupPrefab;
	public List<GameObject> EnemyBossPrefab;

	private byte lastSpawnedBossId;
	private List<List<GameObject>> EnemyToSpawn;
	private float[] spawnTimer;

	void Start() {
		lastSpawnedBossId = 0;
		HelperFunctions.Shuffle(EnemyBossPrefab);

		EnemyToSpawn = new List<List<GameObject>>(3);
		EnemyToSpawn.Add(EnemySinglePrefab);
		EnemyToSpawn.Add(EnemyGroupPrefab);
		EnemyToSpawn.Add(EnemyBossPrefab);

		spawnTimer = new float[3];
		for (byte i = 0; i < spawnTimer.Length; ++i)
			spawnTimer[i] = 0;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		for(byte i = 0; i < spawnTimer.Length; ++i) {
			spawnTimer[i] += Time.deltaTime * GameManager.Instance.SpeedMult;

			if(spawnTimer[i] >= spawnTime[i]){
				spawnTimer[i] -= spawnTime[i];
				Spawn(i);
			}
		}
	}

	void Spawn(byte id) {
		int enemyIndex;
		if(id == 2) {
			enemyIndex = lastSpawnedBossId;
			++lastSpawnedBossId;
			if (lastSpawnedBossId >= EnemyBossPrefab.Count)
				lastSpawnedBossId = 0;
		}
		else{
			enemyIndex =  GameManager.Instance.rand.Next(0, EnemyToSpawn[id].Count);
		}

		Instantiate(EnemyToSpawn[id][enemyIndex], HelperFunctions.GetRandSpawnPoint(), Quaternion.identity);
	}
}
