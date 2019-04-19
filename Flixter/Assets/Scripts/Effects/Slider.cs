using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
	public Vector3 slideValue;
	public LeanTweenType tweenType;

	public float slideTime;
	public float slideDelay;

	Vector3 slideInPos;
	Vector3 slideOutPos;

	void Start() {
		slideInPos = transform.position;
		slideOutPos = transform.position - slideValue;
	}

	public void SlideIn() {
		LeanTween.move(gameObject, slideInPos, slideTime)
		.setDelay(slideDelay)
		.setEase(tweenType);
	}

	public void SlideOut() {
		LeanTween.move(gameObject, slideOutPos, slideTime)
		.setDelay(slideDelay)
		.setEase(tweenType);
	}
}
