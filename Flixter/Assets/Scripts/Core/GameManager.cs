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

	public float SpeedMult {
		set {
			speedMult = value;
			EventManager.CallSpeedMultChangedEvent();
		}
		get {
			return speedMult;
		}
	}
	private float speedMult = 1.0f;

	public bool ClickOnButton = false;

	public System.Random rand;
	public EventManager EventManager;
	public PlayerControl Player;
	public BosterDock bosterDock;
	public InGameMenuController InGameMenu;

	public void Start() {
		rand = new System.Random();
		EventManager = new EventManager();
		Input.multiTouchEnabled = false;
		LeanTween.init(800);

		IsTimeStop = true;
	}
}