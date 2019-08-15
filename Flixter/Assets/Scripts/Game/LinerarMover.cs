using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerarMover : MonoBehaviour {
	public Vector2 point;
	public float flightSpeed;

	private void Awake() {

		EventManager.OnTimeStopChangedEvent += OnTimeStopChangedEvent;
	}

	private void Start() {
		OnTimeStopChangedEvent(null);
	}

	private void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChangedEvent;
	}

	void OnTimeStopChangedEvent(EventData e) {
		if (GameManager.Instance.IsTimeStop) {
			LeanTween.cancel(gameObject, false);
		}
		else {
			LeanTween.move(gameObject, point, (((Vector2)(transform.position)) - point).magnitude / flightSpeed)
			.setOnComplete(()=> {
				Destroy(this);
			});
		}
	}
}
