using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float speed = 8.0f;
	public int damage = 1;

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		transform.Translate(Vector3.up * speed * Time.deltaTime);
		Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPosition.y > 1.1 || viewportPosition.y < -0.1 ||
			viewportPosition.x > 1.1 || viewportPosition.x < -0.1) {
			Destroy(this.gameObject);
		}
	}
}
