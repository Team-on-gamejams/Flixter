using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEnabler : MonoBehaviour {
	public Behaviour component;

	public void Enable() {
		component.enabled = true;
	}

	public void Disable() {
		component.enabled = false;
	}
}
