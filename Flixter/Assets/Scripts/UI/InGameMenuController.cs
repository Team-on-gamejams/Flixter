using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour {
	public Fader mainFader;
	public Slider bosterSlider;

	void Start() {
		GameManager.Instance.InGameMenu = this;
	}

	public void Show(){
		GameManager.Instance.IsTimeStop = true;
		mainFader.FadeIn();
		bosterSlider.SlideIn();
	}

	public void Hide(){
		mainFader.FadeOut();
		bosterSlider.SlideOut();
		GameManager.Instance.IsTimeStop = false;
	}
}
