using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyController {
	protected new void Awake() {
		base.Awake();
	}

	protected void Start() {
		//TODO: remove
		StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
		//Move to 3.5
	}

	protected private void OnDestroy() {

	}
}
