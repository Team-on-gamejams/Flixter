using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	public static event EventController.MethodContainer OnTimeStopChangedEvent;
	public void CallOnTimeStopChangedEvent(EventData ob = null) => OnTimeStopChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnSpeedMultChangedEvent;
	public void CallSpeedMultChangedEvent(EventData ob = null) => OnSpeedMultChangedEvent?.Invoke(ob);
}
