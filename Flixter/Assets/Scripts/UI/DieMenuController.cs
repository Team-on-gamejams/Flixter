using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class DieMenuController : MonoBehaviour {
	[SerializeField] CanvasGroup canvasGroup;
	[SerializeField] Button reviveButton;
	[SerializeField] TextMeshProUGUI reviveForCoinsPriceText;

	LambdaListener reviveListener;

	private void Awake() {
		reviveListener = new LambdaListener();
		reviveListener.OnFinish += () => {
			GameManager.Instance.Player.Revive();
		};
	}

	public void SetDefaults() {
		reviveButton.gameObject.SetActive(true);
	}

	public void UseReviveForCoins() {

	}

	public void UseRevive() {
		reviveButton.gameObject.SetActive(false);
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

	public void ShowAd() {
		if (Advertisement.IsReady(Consts.myPlacementId)) {
			Advertisement.Show(Consts.myPlacementId);
		}
		else {
			Debug.Log("Cant show AD");
		}
	}
}
