using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BosterType {
	None,
	Random,
	Speed,
	Magnet,
	Shield,
	Shoot,
}

public class BosterBase : MonoBehaviour {
	public float speed = 2;
	public BosterType bosterType;
	public float time = 10;
	float currTime = 0;
	bool isActive = false;

	internal SpriteRenderer spRen;
	Collider2D collider2d;
	Coroutine moveCoroutine;
	Coroutine checkOutBordersCoroutine;

	void Start() {
		spRen = GetComponent<SpriteRenderer>();
		collider2d = GetComponent<Collider2D>();
		moveCoroutine = StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
		checkOutBordersCoroutine = StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	void Update() {
		if (!isActive || GameManager.Instance.IsTimeStop)
			return;
		currTime += Time.deltaTime;
		if (currTime >= time) {
			isActive = false;
			TimeEnd();
		}
	}

	public bool CanUse(){
		foreach (var boster in PlayerControl.activeBoster) 
			if (boster.bosterType == bosterType) 
				return false;
		return true;
	}

	//Call on click on boster
	public virtual void Use() {
		isActive = true;
		PlayerControl.activeBoster.Add(this);
	}

	//Call when time == 0
	protected virtual void TimeEnd() {
		PlayerControl.activeBoster.Remove(this);
		Destroy(gameObject);
	}

	public void Hide() {
		StopCoroutine(moveCoroutine);
		StopCoroutine(checkOutBordersCoroutine);
		collider2d.enabled = false;
		spRen.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			Hide();
			GameManager.Instance.bosterDock.AddBoster(this);
		}
	}
}
