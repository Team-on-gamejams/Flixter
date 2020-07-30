using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour {
	[SerializeField] CanvasGroup CanvasGroup;
	[SerializeField] TextMeshProUGUI StatsText;
	[SerializeField] TextMeshProUGUI LevelText;

	public void Show() {
		LeanTween.cancel(gameObject, false);
		LeanTween.value(gameObject, CanvasGroup.alpha, 1, 0.2f)
			.setOnUpdate((a) => {
				CanvasGroup.alpha = a;
			});
	}

	public void Hide() {
		LeanTween.cancel(gameObject, false);
		LeanTween.value(gameObject, CanvasGroup.alpha, 0, 0.2f)
			.setOnUpdate((a) => {
				CanvasGroup.alpha = a;
			});
	}

	public void SetShipData(SkinData data) {
		LevelText.text = $"{data.ShipLevel}";
		StatsText.text = $"HP: {data.maxHealth} \nAttack: {data.bulletDmg}";
	}
}
