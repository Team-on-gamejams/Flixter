using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {
	public ParticleSystem ParticleSystem;

	void Update() {
		if (!ParticleSystem.IsAlive())
			Destroy(gameObject);
	}
}
