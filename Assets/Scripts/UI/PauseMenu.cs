using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public Button pauseButton;

	public GameObject pauseMenu;
	public Button menuCloseButton, quitButton;

	private void Start() {
		pauseButton.onClick.RemoveAllListeners();
		pauseButton.onClick.AddListener(delegate() {
			ShowPauseMenu();
		});
		menuCloseButton.onClick.RemoveAllListeners();
		menuCloseButton.onClick.AddListener(delegate() {
			HidePauseMenu();
		});
		quitButton.onClick.RemoveAllListeners();
		quitButton.onClick.AddListener(delegate() {
			Loader.Load(SceneName.Dashboard);
		});

		GameEventSystem.OnGameEventRaised += HandleGameEvents;
	}

	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}

	private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.LEVEL_END) {
			pauseButton.gameObject.SetActive(false);
			pauseMenu.SetActive(false);
		}
		if (type == GAME_EVENT.LEVEL_START) {
			pauseButton.gameObject.SetActive(true);
			pauseMenu.SetActive(false);
		}
	}

	private void ShowPauseMenu() {
		pauseMenu.SetActive(true);
		pauseButton.gameObject.SetActive(false);
	}

	private void HidePauseMenu() {
		pauseMenu.SetActive(false);
		pauseButton.gameObject.SetActive(true);
	}
}
