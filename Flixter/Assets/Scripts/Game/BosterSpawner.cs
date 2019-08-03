using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterSpawner : MonoBehaviour {
	public GameObject[] bosters;

	public float timeForSpawn = 0.5f;
	float currTime = 0.0f;

	List<Slider> start;

	void Awake() {
		start = new List<Slider>();
		GameManager.Instance.BosterSpawner = this;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		currTime += Time.deltaTime * GameManager.Instance.SpeedMult;
		while (timeForSpawn <= currTime) {
			currTime -= timeForSpawn;
			Instantiate(bosters[Random.Range(0, bosters.Length)], HelperFunctions.GetRandSpawnPoint(), Quaternion.identity, transform);
		}
	}
}
