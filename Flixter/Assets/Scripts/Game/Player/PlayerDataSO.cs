using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDataSO : ScriptableObject {
	public string ShipLevel;

	[MenuItem("GameObject/SO/PlayerData")]
	static void CreateSO() {

	}
}
