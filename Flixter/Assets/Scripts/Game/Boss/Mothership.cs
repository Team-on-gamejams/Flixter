using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : BossBase {
	public GameObject attack;

	bool alreadySpawn = true;
	const float maxTimer = 5.0f;
	float currTimer = 0.0f;

	private void Update() {
		if (currTimer >= maxTimer) {
			if(Mathf.Abs(transform.position.x) <= 0.05f) {
				ProcessAttack();
				currTimer = -4.0f;
			}
		}
		else {
			currTimer += Time.deltaTime;
		}
	}

	protected override void ProcessAttack() {
		LeanTween.cancel(gameObject);
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChanged;
		attack.SetActive(true);
	}
}
