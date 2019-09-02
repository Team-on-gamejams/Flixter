using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {
	// guarantee this will be always a singleton only - can't use the constructor!
	protected CheatManager() { }

	//public bool CheatsOn = true;

	//public bool PlayerIgnoreDamage = false;

	//void Update() {
	//	if (!CheatsOn)
	//		return;

	//	if (Input.GetKeyDown(KeyCode.P))
	//		GameManager.Instance.IsTimeStop = !GameManager.Instance.IsTimeStop;

	//	if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) {
	//		var player = GameManager.Instance.Player;
	//		++player.currentBulletStartPosUse;
	//		if (player.bulletStartPos.Length == player.currentBulletStartPosUse)
	//			--player.currentBulletStartPosUse;
	//	}

	//	if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) {
	//		var player = GameManager.Instance.Player;
	//		if (player.currentBulletStartPosUse != 0)
	//			--player.currentBulletStartPosUse;
	//	}
	//}
}
