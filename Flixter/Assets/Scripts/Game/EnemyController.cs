using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public int livesMax = 3;
	int livesCurr;
	public float speed = 2;

	private SpriteRenderer _spRen;

	void Start() {
		_spRen = GetComponent<SpriteRenderer>();

		livesCurr = livesMax;

		StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
		StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	private void ReciveDamage(int damage) {
		livesCurr -= damage;
		StartCoroutine(HelperFunctions.BlinkOfDamage(_spRen));

		if (livesCurr <= 0) {
			GameManager.Instance.Player.Score += livesMax;
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Bullet") {
			ReciveDamage(collision.GetComponent<BulletController>().damage);
			Destroy(collision.gameObject);
		}
		else if (collision.tag == "Player") {
			//TODO: Подумать над дамагом
			GameManager.Instance.Player.GetDamage(livesCurr);
			Destroy(gameObject);
		}
	}
}
