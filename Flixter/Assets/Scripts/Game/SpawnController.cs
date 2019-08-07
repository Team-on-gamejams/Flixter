using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour {

	public float[] spawnTime;
	public List<GameObject> EnemySinglePrefab;
	public List<GameObject> EnemyGroupPrefab;
	public List<GameObject> EnemyBossPrefab;

	private byte lastSpawnedBossId;
	private List<List<GameObject>> EnemyToSpawn;
	private float[] spawnTimer;

	private bool suspendBossSpawn;

	void Awake() {
		lastSpawnedBossId = 0;
		HelperFunctions.Shuffle(EnemyBossPrefab);

		EnemyToSpawn = new List<List<GameObject>>(3);
		EnemyToSpawn.Add(EnemySinglePrefab);
		EnemyToSpawn.Add(EnemyGroupPrefab);
		EnemyToSpawn.Add(EnemyBossPrefab);

		spawnTimer = new float[3];
		for (byte i = 0; i < spawnTimer.Length; ++i)
			spawnTimer[i] = 0;

		suspendBossSpawn = false;

		EventManager.OnBossSpawned += OnBossSpawned;
		EventManager.OnBossKilled += OnBossKilled;

		GameManager.Instance.SpawnController = this;
	}

	private void OnDestroy() {
		EventManager.OnBossSpawned -= OnBossSpawned;
		EventManager.OnBossKilled -= OnBossKilled;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		for(byte i = 0; i < spawnTimer.Length; ++i) {
			if (i == Consts.bossIdSpawner && suspendBossSpawn)
				continue;

			spawnTimer[i] += Time.deltaTime * GameManager.Instance.SpeedMult;

			if(spawnTimer[i] >= spawnTime[i]){
				spawnTimer[i] -= spawnTime[i];
				Spawn(i);
			}
		}
	}

	void Spawn(byte id) {
		int enemyIndex;
		if(id == Consts.bossIdSpawner) {
			enemyIndex = lastSpawnedBossId;
			++lastSpawnedBossId;
			if (lastSpawnedBossId >= EnemyBossPrefab.Count)
				lastSpawnedBossId = 0;
			Instantiate(EnemyToSpawn[id][enemyIndex], new Vector3(0, 6f, 0), Quaternion.identity, transform);
			GameManager.Instance.EventManager.CallOnBossSpawned();
		}
		else {
			enemyIndex = Random.Range(0, EnemyToSpawn[id].Count);
			Instantiate(EnemyToSpawn[id][enemyIndex], HelperFunctions.GetRandSpawnPoint(), Quaternion.identity, transform);
		}
	}

	public void Clear(){
		const float fadeTime = 1.0f;
		var childs = transform.GetComponentsInChildren<Transform>();
		foreach (var child in childs) {
			if (child.gameObject.GetInstanceID() != gameObject.GetInstanceID()) {
				LeanTween.alpha(child.gameObject, 0, fadeTime)
				.setOnComplete(()=> Destroy(child.gameObject));
			}
		}
			
		for (byte i = 0; i < spawnTimer.Length; ++i)
			spawnTimer[i] = 0;
	}


	void OnBossSpawned(EventData data) {
		suspendBossSpawn = true;
	}

	void OnBossKilled(EventData data) {
		suspendBossSpawn = false;
	}
}
