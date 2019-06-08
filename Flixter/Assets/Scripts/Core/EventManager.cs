using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TObject.Shared;

public class EventManager {
	public static event EventController.MethodContainer OnLocalizationLoadedEvent;
	public void CallOnLocalizationLoadedEvent(EventData ob = null) => OnLocalizationLoadedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnTimeStopChangedEvent;
	public void CallOnTimeStopChangedEvent(EventData ob = null) => OnTimeStopChangedEvent?.Invoke(ob);

	public static event EventController.MethodContainer OnSpeedMultChangedEvent;
	public void CallSpeedMultChangedEvent(EventData ob = null) => OnSpeedMultChangedEvent?.Invoke(ob);
}
