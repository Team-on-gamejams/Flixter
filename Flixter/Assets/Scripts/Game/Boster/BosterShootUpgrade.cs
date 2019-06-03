using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterShootUpgrade : BosterBase {
	public int bulletCount = 3;

	public override void Use() {
		GameManager.Instance.Player.currentBulletStartPosUse = bulletCount - 1;
		base.Use();
	}

	protected override void TimeEnd() {
		GameManager.Instance.Player.currentBulletStartPosUse = 0;
		base.TimeEnd();
	}
}