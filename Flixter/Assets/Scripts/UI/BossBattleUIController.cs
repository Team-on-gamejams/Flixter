using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossBattleUIController : MonoBehaviour {
	[SerializeField] TextMeshProUGUI bossName;
	[SerializeField] UnityEngine.UI.Slider sliderLeft;
	[SerializeField] UnityEngine.UI.Slider sliderRight;
	List<Slider> sliders;

	void Awake() {
		sliders = new List<Slider>(3);
		sliders.Add(bossName.GetComponent<Slider>());
		sliders.Add(sliderLeft.GetComponent<Slider>());
		sliders.Add(sliderRight.GetComponent<Slider>());

		EventManager.OnBossSpawned += OnBossSpawned;
		EventManager.OnBossGetDamage += OnBossGetDamage;
		EventManager.OnBossKilled += OnBossKilled;
	}

	void Start() {
		Hide(true);
	}

	private void OnDestroy() {
		EventManager.OnBossSpawned -= OnBossSpawned;
		EventManager.OnBossGetDamage -= OnBossGetDamage;
		EventManager.OnBossKilled -= OnBossKilled;
	}

	public void Show(bool isForce) {
		foreach (var i in sliders) {
			if (isForce)
				i.SlideInForce();
			else
				i.SlideIn();
		}

		sliderLeft.value = sliderRight.value = 1.0f;
	}

	public void Hide(bool isForce) {
		foreach (var i in sliders) {
			if (isForce)
				i.SlideOutForce();
			else
				i.SlideOut();
		}
	}

	void OnBossSpawned(EventData data) {
		bossName.text = (string)data["name"];
		Show(false);
	}

	void OnBossKilled(EventData data) {
		Hide(false);
	}

	void OnBossGetDamage(EventData data) {
		sliderLeft.value = sliderRight.value = (float)((int)(data["livesCurr"])) / (int)(data["livesMax"]);
	}
}
