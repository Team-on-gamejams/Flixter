using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOnReloadEnd : MonoBehaviour {
	public GameObject attack;

	public void Attack() {
		attack.SetActive(true);
		gameObject.SetActive(false);
	}
}
