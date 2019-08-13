using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControl : MonoBehaviour {
	public int Score{
		get => _score;
		set{
			_score = value;
			EventData data = new EventData("OnScoreChangedEvent");
			data.Data["score"] = _score;
			GameManager.Instance.EventManager.CallOnScoreChangedEvent(data);
		}
	}
	int _score;

	public GameObject player;

	public float shootSpeed = 0.2f;
	public int maxHealth = 10;
	int health;
	public float blinkTime = 1.0f;
	public int blinkCount = 5;
	float oneBlinkTime;
	float currBlinkTime;


	public static List<BosterBase> activeBoster = new List<BosterBase>();

	CoolShieldEffect shield;

    public SpriteRenderer playerBody;

    public int currentBulletStartPosUse;
	public GameObject simpleBulletPrefab;
	public GameObject[] bulletStartPos;
	private Transform[][] bulletStartPosParsed;

	private Vector3 borders;
	internal Vector3 offset;

	private float shootTime = 0;
	private CheatManager cheat;
	private MenuController menuController;

	GameObject bulletsHolder;

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
		menuController = GameObject.FindObjectOfType<MenuController>();

		oneBlinkTime = blinkTime / blinkCount;

		Reload();
    }

	void OnMouseDown() {
		if (!GameManager.Instance.IsGameStart)
			return;
		GameManager.Instance.InGameMenu.Hide();

		offset = player.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z)));
	}

	void OnMouseDrag() {
		if (GameManager.Instance.IsTimeStop || !GameManager.Instance.IsGameStart)
			return;

		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

		if (!IsTouchingBorders(0, cursorPosition.y))
			player.transform.position = new Vector3(player.transform.position.x, cursorPosition.y, player.transform.position.z);

		if (!IsTouchingBorders(cursorPosition.x, 0))
			player.transform.position = new Vector3(cursorPosition.x, player.transform.position.y, player.transform.position.z);
	}

	private void OnMouseUp() {
		if (!GameManager.Instance.IsGameStart)
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
				Instantiate(simpleBulletPrefab, currPos.position, Quaternion.identity, bulletsHolder.transform);
		}
	}

	public void GetDamage(int damage) {
		if (shield.IsActive || cheat.PlayerIgnoreDamage || currBlinkTime != 0)
			return;

		health -= damage;

        if (health <= 0)
			Die();
		else
			StartCoroutine(BlinkOfDamage(playerBody));
	}

	public void ActivateShield(){
		shield.ActivateShield();
	}

	public void DeactivateShield() {
		shield.DeactivateShield();
	}

	//TODO: Add cool effect on die
	public void Die(){
		GameManager.Instance.IsGameStart = false;
		GameManager.Instance.InGameMenu.Show(false);
		menuController.ToDieMenu();
	}

	//TODO: Add cool effect on revive
	public void Revive(){
		menuController.HideDieMenu(true);
		health = maxHealth;
		GameManager.Instance.IsGameStart = true;
	}

	public void Reload(){
		health = maxHealth;
		Score = 0;
		currBlinkTime = 0;
		if (bulletsHolder != null)
		    Destroy(bulletsHolder);
		bulletsHolder = new GameObject("Bullets");

		player.transform.position = new Vector2(0, 0);
		GameManager.Instance.SpawnController.Clear();
		GameManager.Instance.bosterDock.Clear();
	}

	bool IsTouchingBorders(float x, float y) {
		return x < borders.x || x > -borders.x || y < borders.y || y > -borders.y;
	}

    IEnumerator BlinkOfDamage(SpriteRenderer sr){
		currBlinkTime = 0;
		Color tmp = sr.color;
		while ((currBlinkTime += oneBlinkTime) <= blinkTime) {
			while (GameManager.Instance.IsTimeStop) 
				yield return new WaitForSeconds(oneBlinkTime);

			tmp.a = 0.75f;
			sr.color = tmp;
			yield return new WaitForSeconds(oneBlinkTime);

			while (GameManager.Instance.IsTimeStop)
				yield return new WaitForSeconds(oneBlinkTime);

			tmp.a = 1f;
			sr.color = tmp;
			yield return new WaitForSeconds(oneBlinkTime);
		}
		tmp.a = 1f;
		sr.color = tmp;
		currBlinkTime = 0;
	}
}
