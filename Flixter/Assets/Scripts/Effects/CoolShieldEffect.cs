using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolShieldEffect : MonoBehaviour {
	public float time1 = 0.2f;
	public float time2 = 1f;

	public float s1 = 1.05f;
	public float s2 = 0.95f;
	public float a1 = 0.5f;
	public float a2 = 1f;

	void Awake() {
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	void OnTimeStopChangedEvent(EventData ed) {
		if(GameManager.Instance.IsTimeStop){
			LeanTween.cancelAll(gameObject);
		}
		else{
			StartTween();
		}
	}

	void StartTween(){
		LeanTween.scale(gameObject, new Vector3(s1, s1), time1);
		LeanTween.alpha(gameObject, a1, time1).setOnComplete(()=> {
			LeanTween.scale(gameObject, new Vector3(s2, s2), time2);
			LeanTween.alpha(gameObject, a2, time2).setOnComplete(() => {
				if(!GameManager.Instance.IsTimeStop)
					StartTween();
			});
		});
	}
}
