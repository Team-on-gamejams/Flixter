using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossBattleUIController : MonoBehaviour {
	[SerializeField] TextMeshProUGUI bossName;
	[SerializeField] UnityEngine.UI.Slider sliderLeft;
	[SerializeField] UnityEngine.UI.Slider sliderRight;
	[SerializeField] Slider menuSlider;
	List<Slider> sliders;

	bool isShowed;

	void Awake() {
		sliders = new List<Slider>(3) {
			bossName.GetComponent<Slider>(),
			sliderLeft.transform.parent.GetComponent<Slider>(),
			sliderRight.transform.parent.GetComponent<Slider>()
		};

		EventManager.OnBossSpawned += OnBossSpawned;
		EventManager.OnBossGetDamage += OnBossGetDamage;
		EventManager.OnBossKilled += OnBossKilled;
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	void Start() {
		isShowed = true;
		Hide(true);
	}

	private void OnDestroy() {
		EventManager.OnBossSpawned -= OnBossSpawned;
		EventManager.OnBossGetDamage -= OnBossGetDamage;
		EventManager.OnBossKilled -= OnBossKilled;
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	public void Show(bool isForce) {
		if (isShowed)
			return;

		isShowed = true;
		foreach (var i in sliders) {
			if (isForce)
				i.SlideInForce();
			else
				i.SlideIn();
		}

		sliderLeft.value = sliderRight.value = 1.0f;
	}

	public void Hide(bool isForce) {
		if (!isShowed)
			return;

		isShowed = false;
		foreach (var i in sliders) {
			if (isForce)
				i.SlideOutForce();
			else
				i.SlideOut();
		}
		menuSlider.SlideOut();
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

	void OnTimeStopChangedEvent(EventData data) {
		if (!isShowed)
			return;

		if (GameManager.Instance.IsTimeStop)
			menuSlider.SlideIn();
		else
			menuSlider.SlideOut();
	}
}
