using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	public Image fader;

	public float minAlpha;
	public float maxAlpha;

	public float fadeTime;
	public float fadeDelay;

	public void FadeIn(){
		LeanTween.value(fader.gameObject, fader.color.a, maxAlpha, fadeTime)
			.setDelay(fadeDelay)
			.setOnUpdate((float a)=> {
				fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, a);
			});
	}

	public void FadeOut() {
		LeanTween.value(fader.gameObject, fader.color.a, minAlpha, fadeTime)
			.setDelay(fadeDelay)
			.setOnUpdate((float a) => {
				fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, a);
			});
	}
}
