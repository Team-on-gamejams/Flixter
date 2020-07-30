using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
	public float speed = 1;

	private SpriteRenderer _spRen;

	protected void Awake() {
		_spRen = GetComponent<SpriteRenderer>();
	}

	void Start() {
		StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
		StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			GameManager.Instance.Player.Coins += 1;
			Destroy(this.gameObject);
		}
	}
}
