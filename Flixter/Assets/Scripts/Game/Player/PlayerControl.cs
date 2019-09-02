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

	public int Coins {
		get => _coins;
		set {
			_coins = value;
			EventData data = new EventData("OnCoinsChangedEvent");
			data.Data["coins"] = _coins;
			GameManager.Instance.EventManager.CallOnCoinsChangedEvent(data);
            PlayerPrefs.SetInt("coins", _coins);
		}
	}
	int _coins;

	public string Nickname {
		get {
			if(_nickname == null)
				_nickname = PlayerPrefs.HasKey("nickname") ? PlayerPrefs.GetString("nickname") : null;
			return _nickname;

		}
		set {
			if(_nickname != value) {
				_nickname = value;
				PlayerPrefs.SetString("nickname", _nickname);
			}
		}
	}
	string _nickname;

	public int ServerId {
		get {
			if (!PlayerPrefs.HasKey("serverId"))
				PlayerPrefs.SetInt("serverId", (int)(System.DateTime.Now.Ticks % 1000000));
			return PlayerPrefs.GetInt("serverId");
		}
	}

	internal int currRevivePrice;

	public SkinData player;
	
	int health {
		get => _health;
		set {
			_health = value;
			foreach (var i in hpSliders) {
				LeanTween.cancel(i.gameObject, false);
				LeanTween.value(i.gameObject, i.value, ((float)(_health)) / player.maxHealth, 0.2f)
				.setOnUpdate((float val)=> {
					i.value = val;
				});
			}
		}
	}
	int _health;
	public float blinkTime = 1.0f;
	public int blinkCount = 5;
	float oneBlinkTime;
	float currBlinkTime;

	public static List<BosterBase> activeBoster = new List<BosterBase>();

	[SerializeField] MenuController menuController;
	[SerializeField] UnityEngine.UI.Slider[] hpSliders;

	public int currentBulletStartPosUse;
	private Transform[][] bulletStartPosParsed;

	private Vector3 borders;
	internal Vector3 offset;

	private float shootTime = 0;
	GameObject bulletsHolder;

	private void Awake() {
		oneBlinkTime = blinkTime / blinkCount;

		GameManager.Instance.Player = this;
	}

	void Start() {
		borders = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
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

		if (shootTime >= player.shootSpeed) {
			shootTime -= player.shootSpeed;
			foreach (var currPos in bulletStartPosParsed[currentBulletStartPosUse]) {
				BulletController bc = Instantiate(player.simpleBulletPrefab, currPos.position, Quaternion.identity, bulletsHolder.transform).GetComponent<BulletController>();
				bc.damage = player.bulletDmg;
			}
		}
	}

	public void GetDamage(int damage) {
		if (IsShieldActive() || IsInvinsible())
			return;

		health -= damage;

		if (health <= 0)
			Die();
		else
			StartCoroutine(BlinkOfDamage(player.bodySprite));
	}

	public void ActivateShield(){
		player.shield.ActivateShield();
	}

	public void DeactivateShield() {
		player.shield.DeactivateShield();
	}

	public bool IsShieldActive() {
		return player.shield.IsActive;
	}

	//TODO: Add cool effect on die
	public void Die(){
		GameManager.Instance.IsGameStart = false;
		GameManager.Instance.InGameMenu.Show(false);
		menuController.ToDieMenu();
	}

	//TODO: Add cool effect on revive
	public void Revive(){
		menuController.DieMenu.Hide(Consts.menuAnimationsTime, true);
		menuController.DieMenu.UseRevive();
		health = player.maxHealth;
		GameManager.Instance.IsGameStart = true;
	}

	//TODO: Add cool effect on revive
	public void ReviveForCoins() {
		if(Coins >= currRevivePrice) {
			health = player.maxHealth;
			GameManager.Instance.IsGameStart = true;
			Coins -= currRevivePrice;
			currRevivePrice += Consts.reviveStartPrice;

			menuController.DieMenu.Hide(Consts.menuAnimationsTime, true);
			menuController.DieMenu.UseReviveForCoins();
		}
	}

	public void ReInit(){
		foreach (var i in player.bulletStartPos)
			i.SetActive(true);
		bulletStartPosParsed = new Transform[player.bulletStartPos.Length][];
		for (byte i = 0; i < bulletStartPosParsed.Length; ++i)
			bulletStartPosParsed[i] = player.bulletStartPos[i].GetComponentsInChildren<Transform>().Skip(1).ToArray();

		PlayerPrefs.SetInt("maxScore", PlayerPrefs.HasKey("maxScore") ? Mathf.Max(PlayerPrefs.GetInt("maxScore"), Score) : Score);
		Score = 0;
		Coins = PlayerPrefs.HasKey("coins") ? PlayerPrefs.GetInt("coins") : 0;
		currRevivePrice = Consts.reviveStartPrice;

		menuController.ShowFader();
		LeanTween.delayedCall(Consts.menuAnimationsTime, () => {
			player.transform.position = new Vector2(0, 0);
			health = player.maxHealth;
			GameManager.Instance.bosterDock.Clear();
			menuController.DieMenu.SetDefaults();
		}); 

		currBlinkTime = 0;
		if (bulletsHolder != null)
		    Destroy(bulletsHolder);
		bulletsHolder = new GameObject("Bullets");

		GameManager.Instance.SpawnController.Clear();
	}

	bool IsTouchingBorders(float x, float y) {
		return x < borders.x || x > -borders.x || y < borders.y || y > -borders.y;
	}

	public bool IsInvinsible() {
		return currBlinkTime != 0;
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
