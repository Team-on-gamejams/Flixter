using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterDock : MonoBehaviour {
	public BosterHolder[] bosterHolders;

	void Start() {
		GameManager.Instance.bosterDock = this;
	}

	public void AddBoster(BosterBase boster) {

	}
}
