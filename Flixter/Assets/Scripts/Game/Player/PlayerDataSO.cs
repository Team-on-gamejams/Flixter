using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDataSO : ScriptableObject {
	public string ShipLevel;

#if UNITY_EDITOR
	[MenuItem("GameObject/SO/PlayerData")]
	static void CreateSO() {

	}
#endif
}
