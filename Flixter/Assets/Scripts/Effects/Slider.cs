using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
	public Vector3 slideValue;
	public LeanTweenType tweenType;

	public float slideTime;
	public float slideDelay;

	bool calculatePos = true;
	Vector3 slideInPos;
	Vector3 slideOutPos;

	public void SlideIn() {
		if(calculatePos) {
			calculatePos = false;
			slideInPos = transform.position;
			slideOutPos = transform.position - slideValue;
		}
		LeanTween.cancel(gameObject);
		LeanTween.move(gameObject, slideInPos, slideTime)
		.setDelay(slideDelay)
		.setEase(tweenType);
	}

	public void SlideOut() {
		if (calculatePos) {
			calculatePos = false;
			slideInPos = transform.position;
			slideOutPos = transform.position - slideValue;
		}
		LeanTween.cancel(gameObject);
		LeanTween.move(gameObject, slideOutPos, slideTime)
		.setDelay(slideDelay)
		.setEase(tweenType);
	}
}
