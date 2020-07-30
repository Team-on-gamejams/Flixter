using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {
	public float timeForChange = 0.2f;
	public float speed = 1.0f;
	public float length = 1.0f;

	UnityEngine.UI.Slider slider;

	void Start() {
		slider = GetComponent<UnityEngine.UI.Slider>();

		slider.value = GameManager.Instance.SpeedMult == 1 ? 0 : length;

		StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed, 4));
		StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	void Awake() {
		EventManager.OnSpeedMultChangedEvent += OnSpeedMultChangedEvent;
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	void OnDestroy() {
		EventManager.OnSpeedMultChangedEvent -= OnSpeedMultChangedEvent;
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	void OnSpeedMultChangedEvent(EventData ed) {
		if ((GameManager.Instance.IsTimeStop && slider.value == 0) || slider == null)
			return;

		LeanTween.cancel(gameObject);
		LeanTween.value(gameObject, slider.value, GameManager.Instance.SpeedMult == 1 ? 0 : length, timeForChange)
		.setOnUpdate((float a) => {
			slider.value = a;
		});
	}

	void OnTimeStopChangedEvent(EventData ed) {
		if (GameManager.Instance.IsTimeStop) {
			LeanTween.cancel(gameObject);
		}
		else {
			LeanTween.cancel(gameObject);
			LeanTween.value(gameObject, slider.value, GameManager.Instance.SpeedMult == 1 ? 0 : length, timeForChange)
			.setOnUpdate((float a) => {
				slider.value = a;
			});
		}

	}
}
