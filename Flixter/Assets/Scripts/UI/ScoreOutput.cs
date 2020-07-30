using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreOutput : MonoBehaviour {
	public TextMeshProUGUI text;
	int currScore;
	int displayScore;
	Coroutine corrutine;

	void Awake() {
		EventManager.OnScoreChangedEvent += OnScoreChanged;

		currScore = displayScore = 0;
		corrutine = null;
	}

	void OnDestroy() {
		EventManager.OnScoreChangedEvent -= OnScoreChanged;
	}

	IEnumerator UpgradeText(){
        float pause = 1.0f / (currScore - displayScore);
		while(displayScore < currScore){
			++displayScore;
			text.text = displayScore.ToString();
			yield return new WaitForSeconds(pause);
		}
		if(currScore < displayScore)
			text.text = (displayScore = currScore).ToString();
	}

	void OnScoreChanged(EventData data){
		currScore = (int)data.Data["score"];

		if(corrutine != null)
			StopCoroutine(corrutine);
		corrutine = StartCoroutine(UpgradeText());
	}
}
