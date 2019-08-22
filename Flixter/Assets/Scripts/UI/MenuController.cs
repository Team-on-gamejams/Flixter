using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour {
	public enum CurrMenu : byte { MainMenu, PreGameMenu, InGameMenu, DieMenu }
	public CurrMenu currMenu = CurrMenu.MainMenu;

	public GameObject MainMenu;
	public RectTransform Back1;
	public RectTransform Back2;
	Vector3 Back1Start;
	Vector3 Back2Start;
	public RectTransform[] Buttons;
	RectTransformSaver[] ButtonsStart;
	public RectTransform PlayerSpriteMainMenu;

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

		coinsSlider.SlideIn();
		scoreSlider.SlideIn();
		currMenu = CurrMenu.MainMenu;
	}

	public void ToDieMenu() {
		DieMenu.Show(Consts.menuAnimationsTime);
		currMenu = CurrMenu.DieMenu;
	}

	void ShowMainMenu(){
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

		for (byte i = 0; i < Buttons.Length; ++i) {
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
	}

	void ShowPreGameMenu(){
		DieMenu.Hide(Consts.menuAnimationsTime, false);

		GameManager.Instance.IsGameStart = false;

		LeanTween.move(PreGameMenuRect, new Vector2(0, 0), Consts.menuAnimationsTime / 3 * 2)
			.setEase(LeanTweenType.easeOutBack);

		LeanTween.move(PlayerSpriteMainMenu, new Vector2(0, 450), Consts.menuAnimationsTime / 6)
			.setDelay(Consts.menuAnimationsTime / 2)
			.setEase(LeanTweenType.easeInOutQuad)
			//.setOnComplete(()=> {
			//	GameManager.Instance.Player.Reload();
			//})
			;
	}

	void HidePreGameMenu() {
		LeanTween.move(PreGameMenuRect, new Vector2(2000, 0), Consts.menuAnimationsTime / 3 * 2)
			.setEase(LeanTweenType.easeInBack);

		LeanTween.move(PlayerSpriteMainMenu, new Vector2(1450, 1400), Consts.menuAnimationsTime / 6)
			.setEase(LeanTweenType.easeInOutQuad);
	}

	void ShowInGameMenu(){
		LeanTween.move(Back1, new Vector2(-732, 3916), Consts.menuAnimationsTime)
			.setEase(LeanTweenType.easeOutExpo);

		LeanTween.move(Back2, new Vector2(641, -4086), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		LeanTween.move(PreGameMenuRect, new Vector2(0, -2284), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);

		LeanTween.move(PlayerSpriteMainMenu, new Vector2(0, 0), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		LeanTween.scale(PlayerSpriteMainMenu, new Vector2(0.6f, 0.6f), Consts.menuAnimationsTime)
		.setEase(LeanTweenType.easeOutExpo);
		CanvasGroup cgPlayer = PlayerSpriteMainMenu.GetComponent<CanvasGroup>();
		LeanTween.value(PlayerSpriteMainMenu.gameObject, 1, 0, 1)
		.setDelay(Consts.menuAnimationsTime / 2)
		.setOnUpdate((a) => {
			cgPlayer.alpha = a;
		})
		.setOnComplete(() => {
			GameManager.Instance.IsGameStart = true;
		});
	}
}

struct RectTransformSaver{
	public Quaternion rotation { get; set; }
	public Vector3 position { get; set; }
	public Vector3 localScale { get; set; }
}