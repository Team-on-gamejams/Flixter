using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public Transform[] positions;
	public GameObject[] enemies;

	public void SpawnAll() {
		for(byte i = 0; i < enemies.Length; ++i) {
			var go = Instantiate(enemies[i], positions[i].position, Quaternion.identity, gameObject.transform.parent.parent);
			var ec = go.GetComponent<EnemyController>();
			ec.StartCoroutine(SpawnCoroutine(ec, go.transform.localScale.x * 0.2f));
		}
	}

	IEnumerator SpawnCoroutine(EnemyController ec, float step) {
		ec.transform.transform.localScale = new Vector3(0.1f, 0.1f);

		for (byte i = 1; i <= 5; ++i) {
			ec.transform.localScale = new Vector3(step * i, step * i);
			yield return WaitForTime(0.1f);
		}

		bool moveLeft = Random.Range(0, 2) == 1;
		for (byte i = 1; i <= 10; ++i) {
			ec.transform.Translate(Vector3.down * ec.speed * Time.deltaTime * GameManager.Instance.SpeedMult);
			if(moveLeft)
				ec.transform.Translate(Vector3.left * ec.speed * Time.deltaTime * GameManager.Instance.SpeedMult);
			else
				ec.transform.Translate(Vector3.right * ec.speed * Time.deltaTime * GameManager.Instance.SpeedMult);
			yield return WaitForTime(Time.deltaTime);
		}

		ec.StartMove();
	}

	IEnumerator WaitForTime(float time) {
		while (GameManager.Instance.IsTimeStop)
			yield return new WaitForSeconds(Time.deltaTime);
		yield return new WaitForSeconds(time);
	}
}
