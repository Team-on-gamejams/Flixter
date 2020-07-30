using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEnabler : MonoBehaviour {
	public GameObject gameObject;

	public UnityEngine.Events.UnityEvent OnDisable;

	public void EnableGameObject() {
		gameObject.SetActive(true);
	}

	public void DisableGameObject() {
		gameObject.SetActive(false);
		OnDisable?.Invoke();
	}
}
