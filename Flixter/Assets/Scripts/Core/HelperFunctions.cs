using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class HelperFunctions : MonoBehaviour {
	static Vector3 bottomLine = Camera.main.ViewportToWorldPoint(new Vector3(0, -0.1f));

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static string GetCurrentMethod() {
		StackTrace st = new StackTrace();
		StackFrame sf = st.GetFrame(1);

		return sf.GetMethod().Name;
	}

	public static bool GetEventWithChance(int percent) {
		int number = Random.Range(1, 101);
		return number <= percent;
	}

	public static void Shuffle<T>(IList<T> list) {
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static IEnumerator MoveRoutine(GameObject go, float speed) {
		while (true) {
			if (!GameManager.Instance.IsTimeStop)
				go.transform.Translate(Vector3.down * speed * GameManager.Instance.SpeedMult * Time.deltaTime);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

	public static IEnumerator CheckOutBorders(GameObject go) {
		while (true) {
			if (go.transform.position.y < bottomLine.y)
				Destroy(go);
			yield return new WaitForSeconds(3);
		}
	}
}
