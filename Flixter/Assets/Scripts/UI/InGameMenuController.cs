using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour {
	public Fader mainFader;
	public Slider bosterSlider;
	public Slider buttonBack;

	void Start() {
		GameManager.Instance.InGameMenu = this;
	}

	public void Show(bool showBooster = true){
		GameManager.Instance.IsTimeStop = true;
		mainFader.FadeIn();
		buttonBack.SlideIn();
		if (showBooster)
			bosterSlider.SlideIn();
	}

	public void Hide(){
		mainFader.FadeOut();
		bosterSlider.SlideOut();
		buttonBack.SlideOut();
		GameManager.Instance.IsTimeStop = false;
	}
}
