using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsDropper : MonoBehaviour {
	public GameObject coinPrefab;
	public int coinsCount;
	public float dropDistance;
	public float flightSpeed = 4;

	public void Drop() {
		for (ushort i = 0; i < coinsCount; ++i) {
			GameObject go = Instantiate(coinPrefab, this.transform.position, Quaternion.identity, this.transform.parent);
			LinerarMover lm = go.AddComponent<LinerarMover>();
			lm.point = Random.insideUnitCircle * dropDistance;
			lm.flightSpeed = flightSpeed;
		}
	}
}
