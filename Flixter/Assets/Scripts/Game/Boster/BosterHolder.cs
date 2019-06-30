﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosterHolder : MonoBehaviour {
	public static Sprite emptySprite;
	public BosterBase boster;
	Image UIImage;
	Button button;

	void Start() {
		UIImage = GetComponent<Image>();
		button = GetComponent<Button>();
		emptySprite = UIImage.sprite;
	}

	public bool IsEmpty() => boster == null;

	public void FlyToHolder(BosterBase _boster){
		boster = _boster;
		UIImage.sprite = boster.spRen.sprite;
		button.interactable = true;

		boster.Hide();
	}

	public void Use(){
		if (boster.bosterType == BosterType.None || !boster.CanUse())
			return;

		boster.Use();

		boster = null;
		UIImage.sprite = emptySprite;
		button.interactable = false;
	}

	public void ClickOnButton(){
		GameManager.Instance.ClickOnButton = true;
		LeanTween.delayedCall(0.2f, () => { GameManager.Instance.ClickOnButton = false; });
	}
}
