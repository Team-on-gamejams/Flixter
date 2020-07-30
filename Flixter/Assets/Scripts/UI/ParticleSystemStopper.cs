using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStopper : MonoBehaviour {
	public ParticleSystem ParticleSystem;

	void Awake() {
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
		EventManager.OnGameStartChangedEvent += OnGameStartChangedEvent;
	}

	void Start() {
		OnGameStartChangedEvent(null);
	}

	void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
		EventManager.OnGameStartChangedEvent -= OnGameStartChangedEvent;
	}

	void OnTimeStopChangedEvent(EventData data) {
		if (GameManager.Instance.IsTimeStop)
			ParticleSystem.Pause();
		else
			ParticleSystem.Play();
	}

	void OnGameStartChangedEvent(EventData data) {
		if (GameManager.Instance.IsGameStart)
			ParticleSystem.Play();
		else
			ParticleSystem.Pause();
	}
}
