using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour {
	private System.Random _rand = new System.Random();
	private Vector3 _pointToSpawn;

	public List<GameObject> EnemyPrefabs;

	private List<GameObject> listOfEnemy;

	private float spawnDelta = 1.0f;
	private float spawnTimer = 0;

	void Start() {
		listOfEnemy = EnemyPrefabs;
		listOfEnemy.Reverse();
		listOfEnemy = listOfEnemy.Concat(EnemyPrefabs).ToList();
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		spawnTimer += Time.deltaTime;

		if (spawnTimer >= spawnDelta) {
			spawnTimer -= spawnDelta;
			Spawn();
		}
	}

	void Spawn() {
		int enemyIndex = (_rand.Next(0, listOfEnemy.Count - 1) + _rand.Next(0, listOfEnemy.Count - 1)) / 2;
		Instantiate(listOfEnemy[enemyIndex], GetRandSpawnPoint(), Quaternion.identity);
	}

	Vector3 GetRandSpawnPoint() {
		return Camera.main.ViewportToWorldPoint(
			new Vector3(
				_rand.Next(1, 10) / 10.0f,
				1.2f,
				-1 * (Camera.main.transform.position.z)));
	}
}
