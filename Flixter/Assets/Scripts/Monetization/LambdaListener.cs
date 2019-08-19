using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class LambdaListener : IUnityAdsListener {
	public UnityAction OnFinish;
	public UnityAction OnSkip;
	public UnityAction OnAddReady;
	public UnityAction OnDidStart;

	public LambdaListener() {
		Advertisement.AddListener(this);
	}

	~LambdaListener() {
		Advertisement.RemoveListener(this);
	}


	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
		if (showResult == ShowResult.Finished) {
			if(placementId == Consts.myPlacementId) {
				OnFinish?.Invoke();
				Debug.LogWarning("The ad is Finished");
			}
		}
		else if (showResult == ShowResult.Skipped) {
			OnSkip?.Invoke();
			Debug.LogWarning("The ad is Skipped");
		}
		else if (showResult == ShowResult.Failed) {
			Debug.LogWarning("The ad did not finish due to an error.");
		}
	}

	public void OnUnityAdsReady(string placementId) {
		OnAddReady?.Invoke();
	}

	public void OnUnityAdsDidError(string message) {
		Debug.LogWarning(message);
	}

	public void OnUnityAdsDidStart(string placementId) {
		OnDidStart?.Invoke();
	}
}
