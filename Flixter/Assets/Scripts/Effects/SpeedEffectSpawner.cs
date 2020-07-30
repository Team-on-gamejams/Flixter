using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffectSpawner : MonoBehaviour {
	public GameObject starPrefab;

	public float timeForSpawn = 0.5f;
	float currTime = 0.0f;

	List<Slider> start;
	
	IEnumerator Start() {
		start = new List<Slider>();

		for(byte i = 0; i < 100; ++i){
			Instantiate(starPrefab, HelperFunctions.GetRandSpawnPointForInit(), Quaternion.identity, transform);
			if (i % 10 == 0)
				yield return null;
		}
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		if(GameManager.Instance.SpeedMult == 1)
			currTime += Time.deltaTime;
		else
			currTime += Time.deltaTime * GameManager.Instance.SpeedMult * 4;
		while (timeForSpawn <= currTime){
			currTime -= timeForSpawn;
			Instantiate(starPrefab, HelperFunctions.GetRandSpawnPointForStars(), Quaternion.identity, transform);
		}
	}
}
