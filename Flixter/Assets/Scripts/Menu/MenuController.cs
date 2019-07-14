using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	public float MainMenuToPreGameMenu;

	public GameObject MainMenu;
	public RectTransform Back1;
	public RectTransform Back2;
	Vector3 Back1Start;
	Vector3 Back2Start;
	public RectTransform[] Buttons;
	RectTransformSaver[] ButtonsStart;

	public GameObject InGameMenu;

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
	}

	public void ToInGameMenu(){
		MainMenu.SetActive(false);
		InGameMenu.SetActive(true);

		GameManager.Instance.IsGameStart = true;
	}

	public void ToPreGameMenu() {
		LeanTween.move(Back1, new Vector2(-732, 2225), MainMenuToPreGameMenu)//30
		.setEase(LeanTweenType.easeOutBack);
		LeanTween.value(Back1.gameObject, 34.0f, 30.0f, MainMenuToPreGameMenu)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((float z)=> {
			Back1.rotation = Quaternion.Euler(0, 0, z);
		});

		LeanTween.move(Back2, new Vector2(641, -1800), MainMenuToPreGameMenu)//38
		.setEase(LeanTweenType.easeInOutBack);
		LeanTween.value(Back2.gameObject, 34.0f, 38.0f, MainMenuToPreGameMenu)
		.setEase(LeanTweenType.linear)
		.setOnUpdate((float z) => {
			Back2.rotation = Quaternion.Euler(0, 0, z);
		});

		foreach (var button in Buttons) {
			button.GetComponent<Button>().interactable = false;
			LeanTween.scale(button, new Vector3(0, 0, 0), MainMenuToPreGameMenu)
			.setEase(LeanTweenType.easeInOutQuart);
			LeanTween.rotateZ(button.gameObject, button.rotation.eulerAngles.z - 20 * Random.Range(50, 101), MainMenuToPreGameMenu)
			.setEase(LeanTweenType.easeInCirc);
		}

		LeanTweenType t = LeanTweenType.easeOutQuint;
		LeanTween.move(Buttons[0], new Vector2(-54, 734), MainMenuToPreGameMenu / 2)
		.setEase(t);
		LeanTween.move(Buttons[1], new Vector2(229, 929), MainMenuToPreGameMenu / 2)
		.setEase(t);
		LeanTween.move(Buttons[2], new Vector2(-352, 530), MainMenuToPreGameMenu / 2)
		.setEase(t);
		LeanTween.move(Buttons[3], new Vector2(-645, 336), MainMenuToPreGameMenu / 2)
		.setEase(t);
	}

	public void ToMainMenu() {
		LeanTween.move(Back1, Back1Start, MainMenuToPreGameMenu)//30
			.setEase(LeanTweenType.easeInBack);
		LeanTween.value(Back1.gameObject, 30.0f, 34.0f, MainMenuToPreGameMenu)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back1.rotation = Quaternion.Euler(0, 0, z);
			});

		LeanTween.move(Back2, Back2Start, MainMenuToPreGameMenu)//38
			.setEase(LeanTweenType.easeInOutBack);
		LeanTween.value(Back2.gameObject, 38.0f, 34.0f, MainMenuToPreGameMenu)
			.setEase(LeanTweenType.linear)
			.setOnUpdate((float z) => {
				Back2.rotation = Quaternion.Euler(0, 0, z);
			});

		for (byte i = 0; i < Buttons.Length; ++i) {
			LeanTween.scale(Buttons[i], ButtonsStart[i].localScale, MainMenuToPreGameMenu)
				.setEase(LeanTweenType.easeInOutQuart);
			LeanTween.rotateZ(Buttons[i].gameObject, ButtonsStart[i].rotation.eulerAngles.z + 360 * Random.Range(1, 10), MainMenuToPreGameMenu)
				.setEase(LeanTweenType.easeOutCirc);
			LeanTween.move(Buttons[i], ButtonsStart[i].position, MainMenuToPreGameMenu / 2)
				.setEase(LeanTweenType.easeInQuint);
			Buttons[i].GetComponent<Button>().interactable = true;
		}
	}
}

struct RectTransformSaver{
	public Quaternion rotation { get; set; }
	public Vector3 position { get; set; }
	public Vector3 localScale { get; set; }
}