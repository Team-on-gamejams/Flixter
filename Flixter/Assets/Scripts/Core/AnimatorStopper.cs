using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStopper : MonoBehaviour {
	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Awake() {
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	void OnTimeStopChangedEvent(EventData ed){
		animator.enabled = !GameManager.Instance.IsTimeStop;
	}
}
