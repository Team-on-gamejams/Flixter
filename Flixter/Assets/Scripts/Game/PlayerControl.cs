using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControl : MonoBehaviour {
	public GameObject player;

	public float shootSpeed = 0.2f;
	public int health = 10;

	public static List<BosterBase> activeBoster = new List<BosterBase>();

	CoolShieldEffect shield;

	public int currentBulletStartPosUse;
	public GameObject simpleBulletPrefab;
	public GameObject[] bulletStartPos;
	private Transform[][] bulletStartPosParsed;

	private Vector3 borders;
	private Vector3 offset;

	private float shootTime = 0;
	private CheatManager cheat;

	void Start() {
		borders = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
		cheat = GetComponent<CheatManager>();

		GameManager.Instance.Player = this;

		foreach (var i in bulletStartPos)
			i.SetActive(true);
		bulletStartPosParsed = new Transform[bulletStartPos.Length][];
		for(byte i = 0; i < bulletStartPosParsed.Length; ++i)
			bulletStartPosParsed[i] = bulletStartPos[i].GetComponentsInChildren<Transform>().Skip(1).ToArray();

		shield = GetComponentInChildren<CoolShieldEffect>();
	}

	void OnMouseDown() {
		if (GameManager.Instance.ClickOnButton)
			return;
		GameManager.Instance.InGameMenu.Hide();

		offset = player.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z)));
	}

	void OnMouseDrag() {
		if (GameManager.Instance.IsTimeStop || GameManager.Instance.ClickOnButton)
			return;

		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

		if (!IsTouchingBorders(0, cursorPosition.y))
			player.transform.position = new Vector3(player.transform.position.x, cursorPosition.y, player.transform.position.z);

		if (!IsTouchingBorders(cursorPosition.x, 0))
			player.transform.position = new Vector3(cursorPosition.x, player.transform.position.y, player.transform.position.z);
	}

	private void OnMouseUp() {
		if (GameManager.Instance.ClickOnButton)
			return;
		GameManager.Instance.InGameMenu.Show();
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		ShootUpdate(Time.deltaTime);
	}

	void ShootUpdate(float deltaTIme){
		shootTime += deltaTIme;

		if (shootTime >= shootSpeed) {
			shootTime -= shootSpeed;
			foreach (var currPos in bulletStartPosParsed[currentBulletStartPosUse])
				Instantiate(simpleBulletPrefab, currPos.position, Quaternion.identity);
		}
	}

	public void GetDamage(int damage) {
		if (shield.IsActive || cheat.PlayerIgnoreDamage)
			return;

		health -= damage;

		if (health <= 0)
			Die();
	}

	public void ActivateShield(){
		shield.ActivateShield();
	}

	public void DeactivateShield() {
		shield.DeactivateShield();
	}

	//TODO: Add cool effect on die
	public void Die(){
		Destroy(gameObject);
		GameManager.Instance.IsTimeStop = true;
	}

	bool IsTouchingBorders(float x, float y) {
		return x < borders.x || x > -borders.x || y < borders.y || y > -borders.y;
	}
}
