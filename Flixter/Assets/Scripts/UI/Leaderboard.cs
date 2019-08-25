using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour {
	readonly string[] names = {"Nick1", "Nick2", "Nick3", "Nick4", "Nick5", "Nick6", "Nick7", "Nick8", "Nick9", "Nick10", };

	public CanvasGroup canvasGroup;

	public TextMeshProUGUI ServerId;
	public TextMeshProUGUI[] PlayersText;
	public TextMeshProUGUI[] ScoresText;
	public TextMeshProUGUI NoConnection;

	public int UpdateKDSec = 30;
	DateTime lastUpdate;

	List<int> scores;
	List<string> players;

	private void Awake() {
		scores = new List<int>();
		players = new List<string>();
		lastUpdate = DateTime.Now.AddSeconds(-UpdateKDSec - 1);
	}

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

		LoadArray();
		AddPlayerToArray();
		if((DateTime.Now - lastUpdate).TotalSeconds >= UpdateKDSec) {
			lastUpdate = DateTime.Now;
			FillArrayWithRandom();
		}
		SaveArray();

		for (byte i = 0; i < 10; ++i) {
			PlayersText[i].text = players[i];
			ScoresText[i].text = scores[i].ToString();
			PlayersText[i].alpha = ScoresText[i].alpha = 1.0f;
		}
	}

	void ShowNoInternet() {
		NoConnection.alpha = 1.0f;

		foreach (var player in PlayersText)
			player.alpha = 0.0f;
		foreach (var score in ScoresText)
			score.alpha = 0.0f;
	}

	void AddPlayerToArray() {
		int maxPlayerScore = PlayerPrefs.GetInt("maxScore", 100);
		if (players.Contains(GameManager.Instance.Player.Nickname)) {
			int i = players.IndexOf(GameManager.Instance.Player.Nickname);
			if (scores[i] != maxPlayerScore) {
				players.RemoveAt(i);
				scores.RemoveAt(i);
				players.Add(GameManager.Instance.Player.Nickname);
				scores.Add(maxPlayerScore);
			}
		}
		else {
			players.Add(GameManager.Instance.Player.Nickname);
			scores.Add(maxPlayerScore);
		}
	}

	void FillArrayWithRandom() {
		int maxPlayerScore = PlayerPrefs.GetInt("maxScore", 100);
		for (byte i = 0; i < 10; ++i) {
			players.Add(names[UnityEngine.Random.Range(0, names.Length - 1)]);
			scores.Add((int)((maxPlayerScore == 0 ? 100 : maxPlayerScore) * UnityEngine.Random.Range(0.5f, 1.2f)));
		}

		var playersArr = players.ToArray();
		var scoresArr = scores.ToArray();

		Array.Sort(scoresArr, playersArr);
		players.Clear();
		scores.Clear();
		players.AddRange(playersArr);
		scores.AddRange(scoresArr);
		players.Reverse();
		scores.Reverse();
		players.RemoveRange(10, players.Count - 10);
		scores.RemoveRange(10, scores.Count - 10);
	}

	void SaveArray() {
		PlayerPrefsX.SetStringArray("Leaderboard.players", players.ToArray());
		PlayerPrefsX.SetIntArray("Leaderboard.scores", scores.ToArray());
	}

	void LoadArray() {
		players.Clear();
		scores.Clear();
		players.AddRange(PlayerPrefsX.GetStringArray("Leaderboard.players"));
		scores.AddRange(PlayerPrefsX.GetIntArray("Leaderboard.scores"));
	}
}
