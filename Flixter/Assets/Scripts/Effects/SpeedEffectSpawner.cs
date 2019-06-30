using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffectSpawner : MonoBehaviour {
	public GameObject starPrefab;

	public float timeForSpawn = 0.5f;
	float currTime = 0.0f;

	List<Slider> start;
	
	void Start() {
		start = new List<Slider>();
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		currTime += Time.deltaTime * GameManager.Instance.SpeedMult;
		while(timeForSpawn <= currTime){
			currTime -= timeForSpawn;
			Instantiate(starPrefab, HelperFunctions.GetRandSpawnPointForStars(), Quaternion.identity, transform);
		}
	}
}
