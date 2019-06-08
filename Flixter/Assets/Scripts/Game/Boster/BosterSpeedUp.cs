using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterSpeedUp : BosterBase {
	public float newSpeedMult = 2.0f;
	
	public override void Use() {
		GameManager.Instance.SpeedMult = newSpeedMult;
		base.Use();
	}

	protected override void TimeEnd() {
		GameManager.Instance.SpeedMult = 1.0f;
		base.TimeEnd();
	}
}
