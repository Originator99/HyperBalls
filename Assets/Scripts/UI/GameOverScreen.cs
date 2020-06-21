using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {
	public Button quitButton;
	public Button replayButton;
	public Text levelOutputText;
	public Text moneyText;

	private bool hasWon;
	private void Start() {
		hasWon = false;
		if (quitButton != null) {
			quitButton.onClick.RemoveAllListeners();
			quitButton.onClick.AddListener(delegate () {
				//Save Game Here
				ScoreManager.SaveGame(hasWon);
				Loader.Load(SceneName.Dashboard);
			});
		} else {
			Debug.LogError("Quit Button is null in " +  transform.name);
		}
		if (replayButton != null) {
			replayButton.onClick.RemoveAllListeners();
			replayButton.onClick.AddListener(delegate() {
				GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_UNPAUSED); //because game is paused when player is died
				GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_START);
				gameObject.SetActive(false);
			});
		} else {
			Debug.LogError("Retry Button is null in " + transform.name);
		}

		gameObject.SetActive(false);
	}

	public void ShowGameOver(bool hasWon) {
		this.hasWon = hasWon;

		gameObject.SetActive(true);
		quitButton.gameObject.SetActive(true);
		string header = "";
		if (hasWon) {
			replayButton.gameObject.SetActive(false);
			header = "LEVEL COMPLETED!";
		} else {
			replayButton.gameObject.SetActive(true);
			header = "LOSS";
		}
		StartCoroutine(TypeWriterEffect(header, levelOutputText));
		moneyText.text = "$" + ScoreManager.GetTotalMoney();
	}

	private IEnumerator TypeWriterEffect(string text, Text textComponent) {
		textComponent.text = "";
		foreach (char c in text) {
			textComponent.text += c;
			yield return new WaitForSeconds(0.125f);
		}
	}
}
