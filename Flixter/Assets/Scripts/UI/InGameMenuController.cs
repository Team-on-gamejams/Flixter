using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour {
	public Fader mainFader;

	void Start() {
		GameManager.Instance.InGameMenu = this;
	}

	public void Show(){
		GameManager.Instance.IsTimeStop = true;
		mainFader.FadeIn();
	}

	public void Hide(){
		mainFader.FadeOut();
		GameManager.Instance.IsTimeStop = false;
	}
}
