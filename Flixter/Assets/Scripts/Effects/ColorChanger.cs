using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ColorChanger : MonoBehaviour {
	public enum ConstColor{ backColorGame, backColorMenu }
	public ConstColor color;

#if UNITY_EDITOR
	public void Awake() {
		if (Application.isEditor && !Application.isPlaying){
			Camera camera = GetComponent<Camera>();
			if(camera != null){
				switch (color) {
					case ConstColor.backColorGame:
						camera.backgroundColor = Consts.backColorGame;
						break;
					case ConstColor.backColorMenu:
						camera.backgroundColor = Consts.backColorMenu;
						break;
				}
			}
			else{
				Image image = GetComponent<Image>();
				switch (color) {
					case ConstColor.backColorGame:
						image.color = Consts.backColorGame;
						break;
					case ConstColor.backColorMenu:
						image.color = Consts.backColorMenu;
						break;
				}
			}
		}
	}
#endif
}
