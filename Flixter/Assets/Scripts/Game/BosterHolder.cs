using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosterHolder : MonoBehaviour {
	public BosterType bosterType;
	public Image UIImage;

	void Start() {
		UIImage = GetComponent<Image>();
		bosterType = BosterType.None;
	}

	public void FlyToHolder(BosterBase boster){
		bosterType = boster.bosterType;
		UIImage.sprite = boster.spRen.sprite;
		Destroy(boster.gameObject);
	}

	public void Use(){
		if (bosterType == BosterType.None)
			return;

		bosterType = BosterType.None;
		UIImage.sprite = null;
	}
}
