using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour {
	public enum CurrMenu : byte { MainMenu, PreGameMenu, InGameMenu, DieMenu, Leaderboard }
	public CurrMenu currMenu = CurrMenu.MainMenu;

	public Image fader;

	public GameObject MainMenu;
	public RectTransform Back1;
	public RectTransform Back2;
	Vector3 Back1Start;
	Vector3 Back2Start;
	public RectTransform[] Buttons;
	RectTransformSaver[] ButtonsStart;

	public StatsUI StatsUI;

	public float swipeDist = 2.5f;
	public RectTransform[] PlayerSpritesMainMenu;
	byte currPlayerSprite;
	Vector3 startPosition = Vector3.zero;
	Vector3 endPosition = Vector3.zero;
	bool allowPlayerSwipe = false;

	public GameObject PreGameMenu;
	RectTransform PreGameMenuRect;

	public GameObject InGameMenu;
	public DieMenuController DieMenu;
	public BossBattleUIController BossUI;
	public Inputnickname InputNickname;
	public Leaderboard Leaderboard;

	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI scoreText;
	Slider coinsSlider;
	Slider scoreSlider;

	private void Awake(){
		var childs = transform.GetComponentsInChildren<Transform>(true);
		foreach (var child in childs){
			child.gameObject.SetActive(true);
		}

		currPlayerSprite = (byte)PlayerPrefs.GetInt("MenuController.currPlayerSprite", 0);
	}

	void Start() {
		Back1Start = Back1.localPosition;
		Back2Start = Back2.localPosition;
		ButtonsStart = new RectTransformSaver[Buttons.Length];
		for(byte i = 0; i < Buttons.Length; ++i){
			ButtonsStart[i] = new RectTransformSaver() {
				rotation = Buttons[i].rotation,
				position = Buttons[i].localPosition,
				localScale = Buttons[i].localScale,
			};
		}

		PreGameMenuRect = PreGameMenu.GetComponent<RectTransform>();
		PreGameMenuRect.anchoredPosition = new Vector2(2000, 0);

		coinsSlider = coinsText.GetComponent<Slider>();
		scoreSlider = scoreText.GetComponent<Slider>();
	}

	void Update() {
		if(currMenu == CurrMenu.PreGameMenu && allowPlayerSwipe) {
			if (Input.GetMouseButtonDown(0)) {
				startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				StatsUI.Hide();
			}

			if (startPosition != Vector3.zero) {
				float deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;

				if (deltaX != 0) {
					float swipePersent = Mathf.Abs(deltaX) / swipeDist;
					for (byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
						if(deltaX > 0) {
							PlayerSpritesMainMenu[i].transform.localPosition = new Vector3(
								(i - currPlayerSprite) * 1450 + 1450 * swipePersent,
								450 + (i - currPlayerSprite) * 950 + 950 * swipePersent,
								PlayerSpritesMainMenu[i].transform.localPosition.z
							);
						}
						else{
							PlayerSpritesMainMenu[i].transform.localPosition = new Vector3(
								(i - currPlayerSprite) * 1450 + 1450 * swipePersent * -1,
								450 + (i - currPlayerSprite) * 950 + 950 * swipePersent * -1,
								PlayerSpritesMainMenu[i].transform.localPosition.z
							);
						}
					}
				}

				if (Input.GetMouseButtonUp(0)) {
					endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					float swipePersent = Mathf.Abs(endPosition.x - startPosition.x) / swipeDist;
					int swipeTimes = Mathf.RoundToInt(swipePersent);

					if (startPosition != endPosition && swipeTimes != 0) {
						if (startPosition.x < endPosition.x) {
							while (currPlayerSprite != 0 && swipeTimes-- != 0)
								--currPlayerSprite;
						}
						else {
							while (currPlayerSprite != PlayerSpritesMainMenu.Length - 1 && swipeTimes-- != 0)
								++currPlayerSprite;
						}

					}

					ChangePlayerSprite();
					startPosition = endPosition = Vector3.zero;
				}
			}
		}

		void ChangePlayerSprite() {
			PlayerPrefs.SetInt("MenuController.currPlayerSprite", currPlayerSprite);

			for (byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
				LeanTween.move(PlayerSpritesMainMenu[i], new Vector2((i - currPlayerSprite) * 1450, 450 + (i - currPlayerSprite) * 950), Consts.menuAnimationsTime / 6)
					.setEase(LeanTweenType.linear);
			}

			LeanTween.delayedCall(Consts.menuAnimationsTime / 6, ()=> StatsUI.Show());

			//TODO: call only on start game
			//TODO: call in corruutine
			if(GameManager.Instance.Player.player != null) {
				var childs = GameManager.Instance.Player.player.transform.GetComponentsInChildren<Transform>();
				foreach (var child in childs)
					Destroy(child.gameObject);
			}

			GameManager.Instance.Player.player = Instantiate(PlayerSpritesMainMenu[currPlayerSprite].GetComponent<UISkinData>().SkinDataPrefab, GameManager.Instance.Player.transform).GetComponent<SkinData>();
			StatsUI.SetShipData(GameManager.Instance.Player.player);

			//TODO: force ReInit
			GameManager.Instance.Player.ReInit();
		}
	}

	public void ToInGameMenu(){
		ShowInGameMenu();
		currMenu = CurrMenu.InGameMenu;
	}

	public void ToPreGameMenu() {
		if (currMenu == CurrMenu.MainMenu) {
			HideMainMenu();
			LeanTween.delayedCall(Consts.menuAnimationsTime / 3, () => ShowPreGameMenu());
		}
		else if (currMenu == CurrMenu.DieMenu || currMenu == CurrMenu.InGameMenu) {
			LeanTween.move(Back1, new Vector2(-732, 2225), Consts.menuAnimationsTime)//30
				.setEase(LeanTweenType.linear);
			LeanTween.value(Back1.gameObject, 34.0f, 30.0f, Consts.menuAnimationsTime)
				.setEase(LeanTweenType.linear)
				.setOnUpdate((float z) => {
					Back1.rotation = Quaternion.Euler(0, 0, z);
				});

			LeanTween.move(Back2, new Vector2(641, -1800), Consts.menuAnimationsTime)//38
				.setEase(LeanTweenType.linear);
			LeanTween.value(Back2.gameObject, 34.0f, 38.0f, Consts.menuAnimationsTime)
				.setEase(LeanTweenType.linear)
				.setOnUpdate((float z) => {
					Back2.rotation = Quaternion.Euler(0, 0, z);
				});

			LeanTween.delayedCall(Consts.menuAnimationsTime / 3, () => ShowPreGameMenu());
		}
		else {
			ShowPreGameMenu();
		}
		scoreSlider.SlideOut();
		coinsSlider.SlideOut();
		BossUI.Hide(false);
		currMenu = CurrMenu.PreGameMenu;
	}

	public void ToMainMenu() {
		if (currMenu == CurrMenu.PreGameMenu) {
			HidePreGameMenu();
			LeanTween.delayedCall(Consts.menuAnimationsTime / 3, () => ShowMainMenu());
		}
		else if (currMenu == CurrMenu.InGameMenu) {
			GameManager.Instance.IsGameStart = false;
			ShowMainMenu();
		}
		else if (currMenu == CurrMenu.DieMenu) {
			DieMenu.Hide(Consts.menuAnimationsTime);
			ShowMainMenu();
		}
		else if (currMenu == CurrMenu.Leaderboard) {
			HideLeaderboard();
			ShowMainMenu(false);
		}

		coinsSlider.SlideIn();
		scoreSlider.SlideIn();
		currMenu = CurrMenu.MainMenu;
	}

	public void ToDieMenu() {
		DieMenu.Show(Consts.menuAnimationsTime);
		currMenu = CurrMenu.DieMenu;
	}

	public void ToLeaderboard() {
		if (currMenu == CurrMenu.MainMenu) {
			ShowLeaderboard();
			currMenu = CurrMenu.Leaderboard;
		}
	}

	void ShowMainMenu(bool animateButtons = true){
		LeanTween.move(Back1, Back1Start, Consts.menuAnimationsTime)//30
					.setEase(LeanTweenType.easeInBack);
		LeanTween.value(Back1.gameObject, 30.0f, 34.0f, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back1.rotation = Quaternion.Euler(0, 0, z);
			});

		LeanTween.move(Back2, Back2Start, Consts.menuAnimationsTime)//38
			.setEase(LeanTweenType.easeInOutBack);
		LeanTween.value(Back2.gameObject, 38.0f, 34.0f, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back2.rotation = Quaternion.Euler(0, 0, z);
			});

		for (byte i = 0; i < Buttons.Length && animateButtons; ++i) {
			LeanTween.scale(Buttons[i], ButtonsStart[i].localScale, Consts.menuAnimationsTime)
				.setEase(LeanTweenType.easeInOutQuart);
			LeanTween.rotateZ(Buttons[i].gameObject, ButtonsStart[i].rotation.eulerAngles.z + 360 * Random.Range(1, 10), Consts.menuAnimationsTime)
				.setEase(LeanTweenType.easeOutCirc);
			LeanTween.move(Buttons[i], ButtonsStart[i].position, Consts.menuAnimationsTime / 2)
				.setEase(LeanTweenType.easeInQuint);
			Buttons[i].GetComponent<Button>().interactable = true;
		}
	}

	void HideMainMenu(){
		LeanTween.move(Back1, new Vector2(-732, 2225), Consts.menuAnimationsTime)//30
		.setEase(LeanTweenType.easeOutBack);
		LeanTween.value(Back1.gameObject, 34.0f, 30.0f, Consts.menuAnimationsTime)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((float z) => {
			Back1.rotation = Quaternion.Euler(0, 0, z);
		});

		LeanTween.move(Back2, new Vector2(641, -1800), Consts.menuAnimationsTime)//38
		.setEase(LeanTweenType.easeInOutBack);
		LeanTween.value(Back2.gameObject, 34.0f, 38.0f, Consts.menuAnimationsTime)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((float z) => {
			Back2.rotation = Quaternion.Euler(0, 0, z);
		});

		foreach (var button in Buttons) {
			button.GetComponent<Button>().interactable = false;
			LeanTween.scale(button, new Vector3(0, 0, 0), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInOutQuart);
			LeanTween.rotateZ(button.gameObject, button.rotation.eulerAngles.z - 20 * Random.Range(50, 101), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInCirc);
		}

		LeanTweenType t = LeanTweenType.easeOutQuint;
		LeanTween.move(Buttons[0], new Vector2(-54, 734), Consts.menuAnimationsTime / 2)
		.setEase(t);
		LeanTween.move(Buttons[1], new Vector2(229, 929), Consts.menuAnimationsTime / 2)
		.setEase(t);
		LeanTween.move(Buttons[2], new Vector2(-352, 530), Consts.menuAnimationsTime / 2)
		.setEase(t);
		LeanTween.move(Buttons[3], new Vector2(-645, 336), Consts.menuAnimationsTime / 2)
		.setEase(t);

		StatsUI.Hide();
	}

	void ShowPreGameMenu(){
		DieMenu.Hide(Consts.menuAnimationsTime, false);

		startPosition = endPosition = Vector3.zero;
		GameManager.Instance.IsGameStart = false;

		LeanTween.move(PreGameMenuRect, new Vector2(0, 0), Consts.menuAnimationsTime / 3 * 2)
			.setEase(LeanTweenType.easeOutBack)
			.setOnComplete(()=> {
				//TODO: use same as swipe
				StatsUI.SetShipData(PlayerSpritesMainMenu[currPlayerSprite].GetComponent<UISkinData>().SkinDataPrefab.GetComponent<SkinData>());
				StatsUI.Show();
			});

		for(byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
			LeanTween.move(PlayerSpritesMainMenu[i], new Vector2((i - currPlayerSprite) * 1450, 450 + (i - currPlayerSprite) * 950), Consts.menuAnimationsTime / 6)
				.setDelay(Consts.menuAnimationsTime / 2)
				.setEase(LeanTweenType.easeInOutQuad)
				.setOnComplete(()=> allowPlayerSwipe = true);
		}
	}

	void HidePreGameMenu() {
		allowPlayerSwipe = false;

		LeanTween.move(PreGameMenuRect, new Vector2(2000, 0), Consts.menuAnimationsTime / 3 * 2)
			.setEase(LeanTweenType.easeInBack);

		for (byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
			LeanTween.move(PlayerSpritesMainMenu[i], new Vector2((i + 1) * 1450, 450 + (i + 1) * 950), Consts.menuAnimationsTime / 6)
				.setEase(LeanTweenType.easeInOutQuad);
		}
	}

	void ShowInGameMenu(){
		allowPlayerSwipe = false;

		LeanTween.move(Back1, new Vector2(-732, 3916), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeOutExpo);

		LeanTween.move(Back2, new Vector2(641, -4086), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		LeanTween.move(PreGameMenuRect, new Vector2(0, -2284), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);

		LeanTween.move(PlayerSpritesMainMenu[currPlayerSprite], new Vector2(0, 0), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		LeanTween.scale(PlayerSpritesMainMenu[currPlayerSprite], new Vector2(0.6f, 0.6f), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		CanvasGroup cgPlayer = PlayerSpritesMainMenu[currPlayerSprite].GetComponent<CanvasGroup>();
		LeanTween.value(PlayerSpritesMainMenu[currPlayerSprite].gameObject, 1, 0, 1)
		.setDelay(Consts.menuAnimationsTime / 2)
		.setOnUpdate((a) => {
			cgPlayer.alpha = a;
		})
		.setOnComplete(() => {
			GameManager.Instance.IsGameStart = true;
		});

		for (byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
			if(i != currPlayerSprite)
				PlayerSpritesMainMenu[i].GetComponent<CanvasGroup>().alpha = 0.0f;
		}

		LeanTween.delayedCall(Consts.menuAnimationsTime * 1.5f, () => {
			PlayerSpritesMainMenu[currPlayerSprite].transform.localScale = Vector3.one;

			for (byte i = 0; i < PlayerSpritesMainMenu.Length; ++i) {
				PlayerSpritesMainMenu[i].transform.localPosition = new Vector3((i + 1) * 1450, 450 + (i + 1) * 950, 0);
				PlayerSpritesMainMenu[i].GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		});

		HideFader();
	}

	void ShowLeaderboard() {
		Leaderboard.Show();

		LeanTween.move(Back1, new Vector2(-5833, -2116), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInOutQuad);

		LeanTween.move(Back2, new Vector2(6042, 2713), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInOutQuad);

		LeanTweenType t = LeanTweenType.easeInOutBack;
		LeanTween.move(Buttons[0], ButtonsStart[0].position + new Vector3(-1500, -1500), Consts.menuAnimationsTime)
		.setEase(t);
		LeanTween.move(Buttons[1], ButtonsStart[1].position + new Vector3(1500, 1500), Consts.menuAnimationsTime)
		.setEase(t);
		LeanTween.move(Buttons[2], ButtonsStart[2].position + new Vector3(1500, 1500), Consts.menuAnimationsTime)
		.setEase(t);
		LeanTween.move(Buttons[3], ButtonsStart[3].position + new Vector3(-1500, -1500), Consts.menuAnimationsTime)
		.setEase(t);
	}

	void HideLeaderboard() {
		LeanTween.move(Back1, Back1Start, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInBack);
		LeanTween.value(Back1.gameObject, 30.0f, 34.0f, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back1.rotation = Quaternion.Euler(0, 0, z);
			});

		LeanTween.move(Back2, Back2Start, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeInOutBack);
		LeanTween.value(Back2.gameObject, 38.0f, 34.0f, Consts.menuAnimationsTime)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back2.rotation = Quaternion.Euler(0, 0, z);
			})
			.setOnComplete(()=> {
				Leaderboard.Hide();
			});

		for(byte i = 0; i < Buttons.Length; ++i) {
			LeanTween.move(Buttons[i], ButtonsStart[i].position, Consts.menuAnimationsTime)
				.setEase(LeanTweenType.easeInOutBack);
		}
	}

	public void ShowFader() {
		LeanTween.cancel(fader.gameObject, false);
		LeanTween.value(fader.gameObject, fader.color.a, 1.0f, Consts.menuAnimationsTime / 3)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((a) => {
			Color color = fader.color;
			color.a = a;
			fader.color = color;
		});
	}

	public void HideFader() {
		LeanTween.cancel(fader.gameObject, false);
		LeanTween.value(fader.gameObject, fader.color.a, 0.0f, Consts.menuAnimationsTime / 3)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((a) => {
			Color color = fader.color;
			color.a = a;
			fader.color = color;
		});
	}
}

struct RectTransformSaver{
	public Quaternion rotation { get; set; }
	public Vector3 position { get; set; }
	public Vector3 localScale { get; set; }
}
