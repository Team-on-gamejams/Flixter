using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEnabler : MonoBehaviour {
	public Behaviour component;

	public void EnableComponent() {
		component.enabled = true;
	}

	public void DisableComponent() {
		component.enabled = false;
	}
}
