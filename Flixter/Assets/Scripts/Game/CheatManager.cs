using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {
	// guarantee this will be always a singleton only - can't use the constructor!
	protected CheatManager() { }

	public bool CheatsOn = true;

	public bool PlayerReceiveDamage = true;

	void Update() {
		if (!CheatsOn)
			return;

		if (Input.GetKeyDown(KeyCode.P))
			GameManager.Instance.IsTimeStop = !GameManager.Instance.IsTimeStop;
	}
}
