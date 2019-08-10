using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCannon : BossBase {
	protected new void Awake() {
		base.Awake();
	}

	protected new void Start() {
		base.Start();

	}

	protected new private void OnDestroy() {
		base.OnDestroy();
	}

	public override string GetBossName() => "Lazer cannon";
}
