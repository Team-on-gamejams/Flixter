using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour {
	public string TextID = "";
	private Text _textComponent;
	private Text textComponent {
		get {
			if (_textComponent == null)
				_textComponent = GetComponent<Text>();
			return _textComponent;
		}
	}
	private TextMeshProUGUI _textMeshComponent;
	private TextMeshProUGUI textMeshComponent {
		get {
			if (_textMeshComponent == null)
				_textMeshComponent = GetComponent<TextMeshProUGUI>();
			return _textMeshComponent;
		}
	}

	void OnEnable() {
		EventManager.OnLocalizationLoadedEvent += OnLocalizationLoaded;
	}

	void OnDisable() {
		EventManager.OnLocalizationLoadedEvent -= OnLocalizationLoaded;
	}

	void Start() {
		RefreshText();
	}

	void OnLocalizationLoaded(EventData eData) {
		RefreshText();
	}

	void RefreshText() {
		if (textComponent != null) 
			textComponent.text = Localer.GetText(TextID);
		else if (textMeshComponent != null) 
			textMeshComponent.text = Localer.GetText(TextID);
		else 
			Debug.LogError("Can't set localized text: Text or TextMeshPro component not found in " + gameObject.name);
	}
}
