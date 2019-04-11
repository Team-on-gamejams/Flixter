using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using TObject.Shared;

public class Localer : MonoBehaviour {
	/*
	http://docs.unity3d.com/ScriptReference/SystemLanguage.html
	http://docs.unity3d.com/Manual/StyledText.html
	*/

	private static Dictionary<string, string> _textBase;
	private static string _defaultLocale = "English";

	public static void Init() {
		_textBase = new Dictionary<string, string>();
		Reload(GetSystemLanguage());
	}

	public static void Reload(string locale = "English") {
		if (_textBase == null) 
			_textBase = new Dictionary<string, string>();

		TextAsset _localeString = Resources.Load<TextAsset>("Data/Locales/" + locale + "/text/text");

		if (_localeString == null) {
			Debug.LogWarning("CAN'T FIND LOCALE '" + locale + "'. LOADING DEFAULT LOCALE '" + _defaultLocale + "'.");
			_localeString = Resources.Load<TextAsset>("Data/Locales/" + _defaultLocale + "/text/text");
		}

		NanoXMLDocument document = new NanoXMLDocument(_localeString.text);
		NanoXMLNode RotNode = document.RootNode;

		foreach (NanoXMLNode node in RotNode.SubNodes) 
			if (node.Name.Equals("String")) 
				_textBase.Add(node.GetAttribute("id").Value, NormalizeDataString(node.Value));

		GameManager.Instance.EventManager.CallOnLocalizationLoadedEvent();
	}

	public static string GetText(string id) {
		return _textBase?.ContainsKey(id) ?? false ? _textBase[id] : $"#{id}#";
	}

	public static string GetSystemLanguage() {
		return Application.systemLanguage.ToString();
	}

	public static string GetDefaultLocale() {
		return _defaultLocale;
	}

	private static string NormalizeDataString(string ampersandTaggetString) {
		ampersandTaggetString = ampersandTaggetString.Replace("&lt;", "<");
		ampersandTaggetString = ampersandTaggetString.Replace("&gt;", ">");
		ampersandTaggetString = ampersandTaggetString.Replace("&#13;", "\n");
		ampersandTaggetString = ampersandTaggetString.Replace("\r", "\n");
		return ampersandTaggetString;
	}
}
