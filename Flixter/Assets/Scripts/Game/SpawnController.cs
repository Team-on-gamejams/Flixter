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
		spawnTimer = new float[3];

		ReInit();

		EnemyToSpawn = new List<List<GameObject>>(3);
		EnemyToSpawn.Add(EnemySinglePrefab);
		EnemyToSpawn.Add(EnemyGroupPrefab);
		EnemyToSpawn.Add(EnemyBossPrefab);


		EventManager.OnBossSpawned += OnBossSpawned;
		EventManager.OnBossKilled += OnBossKilled;

		GameManager.Instance.SpawnController = this;
	}

	private void OnDestroy() {
		EventManager.OnBossSpawned -= OnBossSpawned;
		EventManager.OnBossKilled -= OnBossKilled;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop || suspendBossSpawn)
			return;

		for(byte i = 0; i < spawnTimer.Length; ++i) {
			//if (i == Consts.bossIdSpawner && suspendBossSpawn)
			//	continue;

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

			EventData data = new EventData("BossSpawn");
			data["name"] = EnemyToSpawn[id][enemyIndex].GetComponent<BossBase>().GetBossName();
			GameManager.Instance.EventManager.CallOnBossSpawned(data);
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

		ReInit();
	}

	void ReInit() {
		for (byte i = 0; i < spawnTimer.Length; ++i)
			spawnTimer[i] = 0;

		lastSpawnedBossId = 0;
		HelperFunctions.Shuffle(EnemyBossPrefab);

		suspendBossSpawn = false;
	}


	void OnBossSpawned(EventData data) {
		suspendBossSpawn = true;
	}

	void OnBossKilled(EventData data) {
		suspendBossSpawn = false;
	}
}
