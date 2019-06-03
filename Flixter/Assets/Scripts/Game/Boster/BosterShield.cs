using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterShield : BosterBase {
	public override void Use() {
		GameManager.Instance.Player.ActivateShield();
		base.Use();
	}

	protected override void TimeEnd() {
		GameManager.Instance.Player.DeactivateShield();
		base.TimeEnd();
	}
}