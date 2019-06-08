using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmmisionController : MonoBehaviour {
	ParticleSystem particle;

	void Start() {
		particle = GetComponent<ParticleSystem>();
	}

	void Awake() {
		EventManager.OnSpeedMultChangedEvent += OnSpeedMultChangedEvent;
	}

	void OnDestroy() {
		EventManager.OnSpeedMultChangedEvent -= OnSpeedMultChangedEvent;
	}

	void OnSpeedMultChangedEvent(EventData ed) {
		particle.playbackSpeed = GameManager.Instance.SpeedMult;
	}
}
