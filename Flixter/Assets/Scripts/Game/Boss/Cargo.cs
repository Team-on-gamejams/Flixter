using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : BossBase {
	public GameObject attack;

	protected override void ProcessAttack() {
		LeanTween.cancel(gameObject);
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChanged;
		attack.SetActive(true);
	}
}
