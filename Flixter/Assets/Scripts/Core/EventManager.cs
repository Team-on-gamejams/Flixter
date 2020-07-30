using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	public static event EventController.MethodContainer OnGameStartChangedEvent;
	public void CallOnGameStartChangedEvent(EventData ob = null) => OnGameStartChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnTimeStopChangedEvent;
	public void CallOnTimeStopChangedEvent(EventData ob = null) => OnTimeStopChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnSpeedMultChangedEvent;
	public void CallSpeedMultChangedEvent(EventData ob = null) => OnSpeedMultChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnScoreChangedEvent;
	public void CallOnScoreChangedEvent(EventData ob = null) => OnScoreChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnCoinsChangedEvent;
	public void CallOnCoinsChangedEvent(EventData ob = null) => OnCoinsChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnBossSpawned;
	public void CallOnBossSpawned(EventData ob = null) => OnBossSpawned?.Invoke(ob);

	public static event EventController.MethodContainer OnBossKilled;
	public void CallOnBossKilled(EventData ob = null) => OnBossKilled?.Invoke(ob);

	public static event EventController.MethodContainer OnBossGetDamage;
	public void CallOnBossGetDamage(EventData ob = null) => OnBossGetDamage?.Invoke(ob);
}
