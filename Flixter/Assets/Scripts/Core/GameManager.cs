using UnityEngine;
using UnityEngine.Advertisements;
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

	public bool IsGameStart {
		get => _isGameStart;
		set {
			_isGameStart = value;
			EventData data = new EventData("CallOnGameStartChangedEvent");
			GameManager.Instance.EventManager.CallOnGameStartChangedEvent(data);
		}
	}
	bool _isGameStart = false;

	public EventManager EventManager;
	public PlayerControl Player;
	public BosterDock bosterDock;
	public InGameMenuController InGameMenu;
	public SpawnController SpawnController;
	public BosterSpawner BosterSpawner;

#if UNITY_IOS
    const string gameId = "3261473";
#elif UNITY_ANDROID
	const string gameId = "3261472";
#else
	const string gameId = "WINDOWS_BUILD";
#endif

	public void Awake() {
		EventManager = new EventManager();
		Input.multiTouchEnabled = false;
		LeanTween.init(800);

		Advertisement.Initialize(gameId, false);

		IsTimeStop = true;
	}
}