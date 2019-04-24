using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterDock : MonoBehaviour {
	public BosterHolder[] bosterHolders;

	void Start() {
		GameManager.Instance.bosterDock = this;
	}

	public void AddBoster(BosterBase boster) {
		if (bosterHolders[0].bosterType == BosterType.None){
			bosterHolders[0].FlyToHolder(boster);
		}
		else if (bosterHolders[1].bosterType == BosterType.None || bosterHolders[2].bosterType == BosterType.None) {
			bool b1 = bosterHolders[1].bosterType == BosterType.None,
				 b2 = bosterHolders[2].bosterType == BosterType.None;
				if(b1 && b2)
					bosterHolders[Random.Range(1, 3)].FlyToHolder(boster);
				else if (b1)
					bosterHolders[1].FlyToHolder(boster);
				else
					bosterHolders[2].FlyToHolder(boster);
		}
		else if (bosterHolders[3].bosterType == BosterType.None || bosterHolders[4].bosterType == BosterType.None) {
			bool b1 = bosterHolders[3].bosterType == BosterType.None,
				 b2 = bosterHolders[4].bosterType == BosterType.None;
			if (b1 && b2)
				bosterHolders[Random.Range(3, 5)].FlyToHolder(boster);
			else if (b1)
				bosterHolders[3].FlyToHolder(boster);
			else
				bosterHolders[4].FlyToHolder(boster);
		}
	}
}
