using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public GameObject player;
	public float shootSpeed = 0.2f;
	public int health = 10;

	[SerializeField]
	private GameObject simpleBulletPrefab;

	private Vector3 borders;
	private Vector3 offset;

	private float shootTime = 0;
	private CheatManager cheat;

	void Start() {
		borders = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f));
		cheat = GetComponent<CheatManager>();

		GameManager.Instance.Player = this;
	}

	void OnMouseDown() {
		if (GameManager.Instance.IsTimeStop)
			return;

		offset = player.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z)));
	}

	void OnMouseDrag() {
		if (GameManager.Instance.IsTimeStop)
			return;

		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

		if (!IsTouchingBorders(0, cursorPosition.y))
			player.transform.position = new Vector3(player.transform.position.x, cursorPosition.y, player.transform.position.z);

		if (!IsTouchingBorders(cursorPosition.x, 0))
			player.transform.position = new Vector3(cursorPosition.x, player.transform.position.y, player.transform.position.z);
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		shootTime += Time.deltaTime;

		if(shootTime >= shootSpeed){
			shootTime -= shootSpeed;
			Instantiate(simpleBulletPrefab, player.transform.position + new Vector3(0.044f, 1.2f), Quaternion.identity);
		}
	}

	public void GetDamage(int damage) {
		if(cheat.PlayerReceiveDamage)
			health -= damage;

		if (health <= 0)
			Die();
	}

	//TODO: Add cool effect on die
	public void Die(){
		Destroy(gameObject);
	}

	bool IsTouchingBorders(float x, float y) {
		return x < borders.x || x > -borders.x || y < borders.y || y > -borders.y;
	}
}
