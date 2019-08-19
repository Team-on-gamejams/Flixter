using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public int livesMax = 3;
	private int livesCurr;
	public float speed = 2;
	private Coroutine moveCoroutine;

	private SpriteRenderer _spRen;

	protected void Awake() {
		_spRen = GetComponent<SpriteRenderer>();
		livesCurr = livesMax;
	}

	void Start() {
		StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Bullet") {
			ReciveDamage(collision.GetComponent<BulletController>().damage, false);
			Destroy(collision.gameObject);
		}
		else if (collision.tag == "Player" && !GameManager.Instance.Player.IsInvinsible()) {
			//TODO: Подумать над дамагом
			//Особенно від боссів
			GameManager.Instance.Player.GetDamage(livesCurr);
			ReciveDamage(livesMax, true);
		}
	}

	public void ReciveDamage(int damage, bool isPlayerCollision) {
		if (livesCurr <= 0)
			return;

		livesCurr -= damage;
		StartCoroutine(HelperFunctions.BlinkOfDamage(_spRen));

		if (this is BossBase) {
			EventData data = new EventData("OnBossGetDamage");
			data["livesMax"] = livesMax;
			data["livesCurr"] = livesCurr;
			GameManager.Instance.EventManager.CallOnBossGetDamage(data);
		}

		if (livesCurr <= 0) {
			if (!isPlayerCollision) {
				GameManager.Instance.Player.Score += livesMax / 10;
				if (this is BossBase)
					GetComponent<CoinsDropper>().Drop();
			}

			if (this is BossBase)
				GameManager.Instance.EventManager.CallOnBossKilled();

			Destroy(this.gameObject);
		}
	}

	public void StartMove() {
		moveCoroutine = StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
	}

	public void StopMove() {
		StopCoroutine(moveCoroutine);
	}
}
