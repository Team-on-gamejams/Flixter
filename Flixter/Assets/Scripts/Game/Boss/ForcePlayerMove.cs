using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlayerMove : MonoBehaviour {
	public Vector3 move;

	private void OnTriggerStay2D(Collider2D collision) {
		if (!GameManager.Instance.IsTimeStop && collision.tag == "Player") {
			GameManager.Instance.Player.player.transform.localPosition += move * Time.deltaTime;
			GameManager.Instance.Player.offset += move * Time.deltaTime;
		}
	}
}
