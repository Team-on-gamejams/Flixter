using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {
	public int damage = 1;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			GameManager.Instance.Player.GetDamage(damage);
		}
	}
}
