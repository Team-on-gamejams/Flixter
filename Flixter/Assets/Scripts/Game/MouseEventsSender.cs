using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventsSender : MonoBehaviour {
	public MonoBehaviour sendTo;

	void OnMouseDown() {
		sendTo.Invoke("OnMouseDown", 0);
	}

	void OnMouseDrag() {
		sendTo.Invoke("OnMouseDrag", 0);
	}
}
