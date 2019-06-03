using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolShieldEffect : MonoBehaviour {
	public float time1 = 0.2f;
	public float time2 = 1f;

	public float size1 = 1.05f;
	public float size2 = 0.95f;
	public float alpha1 = 0.5f;
	public float alpha2 = 1f;

	Vector3 scale1, scale2;
	SpriteRenderer sp;

	internal bool IsActive;
	internal bool IsReadyToActive;

	void Start() {
		IsActive = false;
		scale1 = new Vector3(size1, size1);
		scale2 = new Vector3(size2, size2);
		sp = GetComponent<SpriteRenderer>();
		sp.color = new Color(1, 1, 1, 0);
	}

	void Awake() {
		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	public void ActivateShield(){
		IsReadyToActive = true;
	}

	//TODO: Cool effect when time is almost over
	public void DeactivateShield() {
		IsActive = false;
		LeanTween.cancel(gameObject, false);
		sp.color = new Color(1, 1, 1, 0);
	}

	void OnTimeStopChangedEvent(EventData ed) {
		if(GameManager.Instance.IsTimeStop) {
			LeanTween.cancel(gameObject, false);
		}
		else if(IsReadyToActive){
			IsReadyToActive = false;
			IsActive = true;
			transform.localScale = scale2;
			LeanTween.alpha(gameObject, alpha2, time2).setOnComplete(() => {
				StartTween();
			});
		}
		else if(IsActive){
			StartTween();
		}
	}

	void StartTween(){
		LeanTween.scale(gameObject, scale1, time1);
		LeanTween.alpha(gameObject, alpha1, time1).setOnComplete(()=> {
			LeanTween.scale(gameObject, scale2, time2);
			LeanTween.alpha(gameObject, alpha2, time2).setOnComplete(() => {
				if(!GameManager.Instance.IsTimeStop)
					StartTween();
			});
		});
	}
}
