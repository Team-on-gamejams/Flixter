using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGroupController : MonoBehaviour {
	List<EnemyController> controllers;

	public float GroupSpeed;

	void Start() {
		controllers = new List<EnemyController>(GetComponentsInChildren<EnemyController>());

		if(GroupSpeed != 0)
			foreach (var c in controllers) {
				c.speed = GroupSpeed;
				c.StartMove();
			}
	}
}
