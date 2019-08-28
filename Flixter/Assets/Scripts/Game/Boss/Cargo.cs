using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : BossBase {
	public GameObject reload;

	protected override void ProcessAttack() {
		reload.SetActive(true);
	}
}
