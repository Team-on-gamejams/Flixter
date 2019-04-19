using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosterHolder : MonoBehaviour {
	public BosterBase boster;
	public Image UIImage;

	void Start() {
		UIImage = GetComponent<Image>();
	}


	void Update() {

	}
}
