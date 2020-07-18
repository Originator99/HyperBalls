using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanel : MonoBehaviour {
	public GameObject heartPrefab;
	public Transform livesPanel;

	private int currentLives;
	private readonly string heartFilledColor = "FFFF00";
	private readonly string heartUnfilledColor = "67675F";

	private void Start() {
		GameEventSystem.OnGameEventRaised += HandleGameEvents;
		DisableLivesPanel();
	}
	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}

	private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.UPDATE_LIFE && data != null && data.GetType() == typeof(int)) {
			UpdateLives((int)data);
		} else if (type == GAME_EVENT.UPDATE_LIFE) {
			Debug.LogWarning("Update life called but type of data is not int inside : " + gameObject.name);
		}

		if (type == GAME_EVENT.LEVEL_START) {
			currentLives = -1;
		}

		if (type == GAME_EVENT.LEVEL_END) {
			DisableLivesPanel();
		}
	}

	private void DisableLivesPanel() {
		if (livesPanel != null) {
			foreach (Transform child in livesPanel) {
				child.gameObject.SetActive(false);
			}
		} else {
			Debug.LogError("Cannot render lives UI. Lives Panel is NULL");
		}
	}

	private void UpdateLives(int amount) {
		if (currentLives == -1) {
			currentLives = amount;
			InitLives();
		} else {
			currentLives += amount;
			if (amount < 0) {
				//life has decreased
				OnLifeDecreased();
			} else {
				//life has increased
				InitLives();
			}
		}
	}



	private void InitLives() {
		int new_panels = currentLives - livesPanel.childCount;
		for (int i = 0; i < new_panels; i++) {
			Instantiate(heartPrefab, livesPanel);
		}

		for (int i = 0; i < currentLives; i++) {
			Transform child = livesPanel.GetChild(i);
			child.gameObject.SetActive(true);
			child.GetComponent<Image>().color = Helper.HexToColor(heartFilledColor);
		}

		for (int i = currentLives; i < livesPanel.childCount; i++) {
			Transform child = livesPanel.GetChild(i);
			child.gameObject.SetActive(false);
		}
	}

	private void OnLifeDecreased() {
		if (currentLives < 0) {
			currentLives = 0;
		}
		if (currentLives < livesPanel.childCount) {
			Transform child = livesPanel.GetChild(currentLives);
			child.GetComponent<Image>().color = Helper.HexToColor(heartUnfilledColor);
		}
	}
}
