using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCannon : BossBase {
	public GameObject attack;

	protected new void Awake() {
		base.Awake();
	}

	protected new void Start() {
		base.Start();

	}

	protected new private void OnDestroy() {
		base.OnDestroy();
	}

	protected override void ProcessAttack() {
		LeanTween.cancel(gameObject);
		attack.SetActive(true);
	}
}
