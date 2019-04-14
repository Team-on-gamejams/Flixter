using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public int lives = 3;
	public float _speed = 2;

	private SpriteRenderer _spRen;
	private Color _startColor;

	void Start() {
		_spRen = GetComponent<SpriteRenderer>();
		_startColor = _spRen.color;

		StartCoroutine(MoveRoutine());
		StartCoroutine(CheckOutBorders());
	}

	private void ReciveDamage(int damage) {
		lives -= damage;
		StartCoroutine(BlinkOfDamage());

		if (lives <= 0) 
			Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Bullet") {
			ReciveDamage(collision.GetComponent<BulletController>().damage);
			Destroy(collision.gameObject);
		}
		else if (collision.tag == "Player") {
			//TODO: Подумать над дамагом
			GameManager.Instance.Player.Damage(lives);
			Destroy(gameObject);
		}
	}

	IEnumerator BlinkOfDamage() {
		_spRen.color = new Color(
				_startColor.r,
				_startColor.g,
				_startColor.b,
				_startColor.a * 0.5f);
		yield return new WaitForSeconds(0.01f);
		_spRen.color = _startColor;
	}

	IEnumerator MoveRoutine() {
		while (true) {
			if(!GameManager.Instance.IsTimeStop)
				transform.Translate(Vector3.down * _speed * Time.deltaTime);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

	IEnumerator CheckOutBorders() {
		var buttom = Camera.main.ViewportToWorldPoint(new Vector3(0, -0.1f));

		while (true) {
			if (transform.position.y < buttom.y) 
				Destroy(this.gameObject);
			yield return new WaitForSeconds(3);
		}
	}
}
