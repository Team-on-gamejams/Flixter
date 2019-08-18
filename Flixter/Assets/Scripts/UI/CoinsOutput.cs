using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsOutput : MonoBehaviour {
	public TextMeshProUGUI text;

	int currCoins;
	int displayCoins;
	Coroutine corrutine;

	void Awake() {
		EventManager.OnCoinsChangedEvent += OnCoinsChangedEvent;

		currCoins = displayCoins = 0;
		corrutine = null;
	}

	void OnDestroy() {
		EventManager.OnCoinsChangedEvent -= OnCoinsChangedEvent;
	}

	IEnumerator UpgradeText() {
		float pause = 1.0f / Mathf.Abs(currCoins - displayCoins);
		while (displayCoins < currCoins) {
			++displayCoins;
			text.text = displayCoins.ToString();
			yield return new WaitForSeconds(pause);
		}
		while (displayCoins > currCoins) {
			--displayCoins;
			text.text = displayCoins.ToString();
			yield return new WaitForSeconds(pause);
		}
	}

	void OnCoinsChangedEvent(EventData data) {
		currCoins = (int)data.Data["coins"];

		if (corrutine != null)
			StopCoroutine(corrutine);
		corrutine = StartCoroutine(UpgradeText());
	}
}
