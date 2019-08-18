using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCannon : BossBase {
	public GameObject attack;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorStopper animatorStopper;

	protected new void Awake() {
		base.Awake();
	}

	protected new void Start() {
		base.Start();
	}

	protected new private void OnDestroy() {
		base.OnDestroy();
	}

	void OnReloadAnimationEnd() {
		animator.enabled = false;
		animatorStopper.enabled = false;
		ProcessAttack();
	}

	public override void ProcessMove() {
		if (!animator.enabled) {
			animator.enabled = true;
			animatorStopper.enabled = true;
		}
		base.ProcessMove();
	}

	protected override void ProcessAttack() {
		LeanTween.cancel(gameObject);
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChanged;
		attack.SetActive(true);
	}
}
