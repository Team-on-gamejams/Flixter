using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TObject.Shared;

public class EventManager {
	public static event EventController.MethodContainer OnLocalizationLoadedEvent;
	public void CallOnLocalizationLoadedEvent(EventData ob = null) => OnLocalizationLoadedEvent?.Invoke(ob);

}
