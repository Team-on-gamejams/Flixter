using System;
using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif

[ExecuteInEditMode]
public class Timebomb : MonoBehaviour {
	public int Year = 2019;
	public int Month = 7;
	public int Day = 9;
	public int TimebombLength = 7;
	
	public bool BombActivated = false;

#if UNITY_EDITOR
	public void Awake() {
		if (Application.isEditor && !Application.isPlaying) 
			SetCurrentDate();
	}
#else
	void Awake() {
		if (!BombActivated)
			return;

		DateTime deathDate = new DateTime(ayear, amonth, aday);
		DateTime nowDate = System.DateTime.Now;

		TimeSpan elapsed = nowDate.Subtract(deathDate);

		if (elapsed.TotalDays > timebombLength) 
			Application.Quit();
	}
#endif

	public void SetCurrentDate() {
		DateTime nowDate = System.DateTime.Now;
		Year = nowDate.Year;
		Month = nowDate.Month;
		Day = nowDate.Day;
	}
}

#if UNITY_EDITOR
	[CustomEditor(typeof(Timebomb))]
	public class TimebombGUI : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			Timebomb tb = (Timebomb)target;
			if (GUILayout.Button("SetCurrentDate"))
				tb.SetCurrentDate();
		}
	}
#endif
