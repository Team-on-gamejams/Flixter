using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour {
	public CanvasGroup canvasGroup;

	public TextMeshProUGUI ServerId;
	public TextMeshProUGUI[] Players;
	public TextMeshProUGUI[] Scores;
	public TextMeshProUGUI NoConnection;

	public void Show() {
		ServerId.text = $"Server id: #{GameManager.Instance.Player.ServerId:D6}";
		UpdateLeaderboard();

		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
	}

	public void Hide() {
		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
	}

	void UpdateLeaderboard() {
		if (IsConnectedToInternet())
			ShowFakePlayers();
		else
			ShowNoInternet();
	}

	bool IsConnectedToInternet() {
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return false;

		string HtmlText = GetHtmlFromUri("http://google.com");
		if (HtmlText == "") {
			//no connection
			return false;
		}
		else if (!HtmlText.Contains("schema.org/WebPage")) {
			//Redirecting since the beginning of googles html contains that 
			//phrase and it was not found
			return false;
		}
		else {
			//success
			return true;
		}

		//ty, naglers
		//https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
		string GetHtmlFromUri(string resource) {
			string html = string.Empty;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
			try {
				using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
					bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
					if (isSuccess) {
						using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {
							//We are limiting the array to 80 so we don't have
							//to parse the entire html document feel free to 
							//adjust (probably stay under 300)
							char[] cs = new char[80];
							reader.Read(cs, 0, cs.Length);
							foreach (char ch in cs) {
								html += ch;
							}
						}
					}
				}
			}
			catch {
				return "";
			}
			return html;
		}
	}

	void ShowFakePlayers() {
		NoConnection.alpha = 0.0f;

		foreach (var player in Players)
			player.alpha = 1.0f;
		foreach (var score in Scores)
			score.alpha = 1.0f;
	}

	void ShowNoInternet() {
		NoConnection.alpha = 1.0f;

		foreach (var player in Players)
			player.alpha = 0.0f;
		foreach (var score in Scores)
			score.alpha = 0.0f;
	}
}
