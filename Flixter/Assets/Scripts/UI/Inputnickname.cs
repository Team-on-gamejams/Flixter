using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inputnickname : MonoBehaviour {
	public TMP_InputField inputField;
	public CanvasGroup canvasGroup;

	void Start() {
		if ((GameManager.Instance.Player.Nickname?.Length ?? 0) != 0) {
			Hide(Consts.menuAnimationsTime, true);
		}
	}

	public void OnOkButtonClick() {
		if(inputField.text.Length != 0) {
			GameManager.Instance.Player.Nickname = inputField.text;
			Hide(Consts.menuAnimationsTime, false);
		}
	}

	public void Show(float time) {
		LeanTween.cancel(gameObject, false);
		LeanTween.value(gameObject, canvasGroup.alpha, 1, time)
		.setOnUpdate((a) => {
			canvasGroup.alpha = a;
		})
		.setOnComplete(() => {
			canvasGroup.blocksRaycasts = canvasGroup.interactable = true;
		});
	}

	public void Hide(float time, bool isForce = false) {
		LeanTween.cancel(gameObject, false);

		if (!isForce) {
			LeanTween.value(gameObject, canvasGroup.alpha, 0, time / 3)
			.setOnUpdate((a) => {
				canvasGroup.alpha = a;
			})
			.setOnComplete(() => {
				canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
			});
		}
		else {
			canvasGroup.alpha = 0.0f;
			canvasGroup.blocksRaycasts = canvasGroup.interactable = false;
		}
	}
}
