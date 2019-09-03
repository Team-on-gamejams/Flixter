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
			if (!GameManager.Instance.IsTimeStop){
					go.transform.Translate(Vector3.down * speed * Time.deltaTime * GameManager.Instance.SpeedMult);
			}
			yield return null;
		}
	}

	public static IEnumerator MoveRoutine(GameObject go, float speed, float addSpeedMult) {
		while (true) {
			if (!GameManager.Instance.IsTimeStop) {
				go.transform.Translate(Vector3.down * speed * Time.deltaTime * (GameManager.Instance.SpeedMult == 1 ? 1 : GameManager.Instance.SpeedMult * addSpeedMult));
			}
			yield return null;
		}
	}

	public static IEnumerator CheckOutBorders(GameObject go) {
		while (true) {
			if (go.transform.position.y < bottomLine.y)
				Destroy(go);
			yield return new WaitForSeconds(3);
		}
	}

    public static IEnumerator BlinkOfDamage(SpriteRenderer _spRen)
    {
        Color tmp = _spRen.color;
        tmp.a = 0.5f;
        _spRen.color = tmp;
        yield return new WaitForSeconds(0.01f);
        tmp.a = 1;
		_spRen.color = tmp;
    }

    public static Vector3 GetRandSpawnPoint() {
		return Camera.main.ViewportToWorldPoint(
			new Vector3(
				Random.Range(0.0f, 1.0f),
				1.2f,
				-1 * (Camera.main.transform.position.z)
		));
	}

	public static Vector3 GetRandSpawnPointForStars() {
		return Camera.main.ViewportToWorldPoint(
			new Vector3(
				Random.Range(0.0f, 1.0f),
				1.2f,
				-1 * (Camera.main.transform.position.z)
		));
	}

	public static Vector3 GetRandSpawnPointForInit() {
		return Camera.main.ViewportToWorldPoint(
			new Vector3(
				Random.Range(0.0f, 1.0f),
				Random.Range(0.0f, 1.2f),
				-1 * (Camera.main.transform.position.z)
		));
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("PlayerPrefs/Clear All")]
	static void ClearPlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}
#endif
}
