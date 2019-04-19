using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterBase : MonoBehaviour {
	public float speed = 2;
	public SpriteRenderer spRen;

	public Coroutine moveCoroutine;
	public Coroutine checkOutBordersCoroutine;


	void Start() {
		spRen = GetComponent<SpriteRenderer>();
		moveCoroutine = StartCoroutine(HelperFunctions.MoveRoutine(gameObject, speed));
		checkOutBordersCoroutine = StartCoroutine(HelperFunctions.CheckOutBorders(gameObject));
	}

	public virtual void Use(){ }

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			StopCoroutine(moveCoroutine);
			StopCoroutine(checkOutBordersCoroutine);
			GameManager.Instance.bosterDock.AddBoster(this);
		}
	}
}
