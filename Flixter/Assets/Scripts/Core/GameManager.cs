using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	// guarantee this will be always a singleton only - can't use the constructor!
	protected GameManager() { Init(); }

	public EventManager EventManager;

	public void Init() {
		Application.targetFrameRate = 60;

		LeanTween.init(800);
	}
}