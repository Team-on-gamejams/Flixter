using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosterDock : MonoBehaviour {
	public BosterHolder[] bosterHolders;

	void Start() {
		GameManager.Instance.bosterDock = this;

		var img = GetComponentInChildren<Image>();
		GetComponent<Slider>().slideValue.y = img.rectTransform.rect.height * img.rectTransform.lossyScale.y / 2;
	}

	public void AddBoster(BosterBase boster) {
		if (bosterHolders[0].IsEmpty()){
			bosterHolders[0].FlyToHolder(boster);
		}
		else if (bosterHolders[1].IsEmpty() || bosterHolders[2].IsEmpty()) {
			bool b1 = bosterHolders[1].IsEmpty() ,
				 b2 = bosterHolders[2].IsEmpty() ;
				if(b1 && b2)
					bosterHolders[Random.Range(1, 3)].FlyToHolder(boster);
				else if (b1)
					bosterHolders[1].FlyToHolder(boster);
				else
					bosterHolders[2].FlyToHolder(boster);
		}
		else if (bosterHolders[3].IsEmpty() || bosterHolders[4].IsEmpty()) {
			bool b1 = bosterHolders[3].IsEmpty(),
				 b2 = bosterHolders[4].IsEmpty();
			if (b1 && b2)
				bosterHolders[Random.Range(3, 5)].FlyToHolder(boster);
			else if (b1)
				bosterHolders[3].FlyToHolder(boster);
			else
				bosterHolders[4].FlyToHolder(boster);
		}
	}
}
