using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	// guarantee this will be always a singleton only - can't use the constructor!
	protected GameManager() { }

	public bool IsTimeStop{
		set{
			isTimeStop = value;
			EventManager.CallOnTimeStopChangedEvent();
		}
		get{
			return isTimeStop;
		}
	}
	private bool isTimeStop;

	public EventManager EventManager;
	public PlayerControl Player;

	public void Start() {
		EventManager = new EventManager();
		LeanTween.init(800);

		IsTimeStop = false;
	}
}