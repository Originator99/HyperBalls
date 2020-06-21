using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelUIManager : MonoBehaviour {
	public GameOverScreen gameOverController;

	public static LevelUIManager instance;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("More than on instane of Singleton LevelUIManager");
		}
	}

	private void Start() {
		GameEventSystem.OnGameEventRaised += HandleGameEvents;
		gameOverController.gameObject.SetActive(false);
	}
	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}
	private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.LEVEL_START) {
			OnLevelStart();
		}
	}

	private void OnLevelStart() {
		if (gameOverController != null) {
			gameOverController.gameObject.SetActive(false);
		} else {
			Debug.LogError("Game Over Controller is null in : " + transform.name);
		}
	}

	public IEnumerator ShowGameOverScreen(bool hasWon) {
		yield return new WaitForSeconds(1f);// waiting for shit to get settled before showing the damn screen
		gameOverController.ShowGameOver(hasWon);
	}
}
