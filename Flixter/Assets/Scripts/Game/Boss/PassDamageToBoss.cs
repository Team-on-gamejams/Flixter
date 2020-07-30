using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassDamageToBoss : MonoBehaviour {
	public BossBase boss;
	public int DamageOnPlayerCollision = 1;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Bullet") {
			boss.ReciveDamage(collision.GetComponent<BulletController>().damage, false);
			Destroy(collision.gameObject);
		}
		else if (collision.tag == "Player" && !GameManager.Instance.Player.IsInvinsible()) {
			//TODO: Подумать над дамагом
			//Особенно від боссів
			GameManager.Instance.Player.GetDamage(DamageOnPlayerCollision);
			boss.ReciveDamage(boss.livesMax, true);
		}
	}
}
