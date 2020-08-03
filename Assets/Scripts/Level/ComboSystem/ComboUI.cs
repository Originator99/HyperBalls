using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public static class ComboHelper {
	public static int MaxCombo;
}

public class ComboUI : MonoBehaviour {
	public TextMeshProUGUI comboAmount, comboLabel;
	public TMP_ColorGradient combo2xGradient, combo3xGradient, combo4xGradient, combo5xGradient;

	private readonly float maxComboTime = 3.5f;

	private float currentComboTime;
	private int currentCombo;
	private bool gamePaused;

	private void Start() {
		GameEventSystem.OnGameEventRaised += HandleGameEvents;
		comboAmount.gameObject.SetActive(true);
		comboLabel.gameObject.SetActive(true);
		DisableCombo();
	}
	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}

	private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.OBSTACLE_HIT && data!= null && data.GetType() == typeof(GameObject)) {
			GameObject go = data as GameObject;
			Obstacle obs = go.GetComponent<Obstacle>();
			if (obs != null) {
				CheckAndIncreaseCombo(obs);
			} else {
				Debug.LogWarning("Cannot increase combo, obstacle type not found on hit event");
			}
		}
		if (type == GAME_EVENT.LEVEL_START) {
			ResetCombo();
		}
		if (type == GAME_EVENT.LEVEL_END) {

		}

		if (type == GAME_EVENT.GAME_PAUSED) {
			gamePaused = true;
		}
		if (type == GAME_EVENT.GAME_UNPAUSED) {
			gamePaused = false;
		}
	}
	private void Update() {
		if (currentComboTime > 0 && !gamePaused) {
			currentComboTime -= Time.unscaledDeltaTime;
			if (currentComboTime <= 0) {
				currentCombo = 0;
				DisableCombo();
			}
		}
	}

	private void ResetCombo() {
		ComboHelper.MaxCombo = 1;
		currentCombo = 0;
		currentComboTime = 0;
		DisableCombo();
	}

	private void OnLevelEnd() {
		comboLabel.DOFade(0f, 1f);
		comboAmount.DOFade(0, 1f);
	}

	private void CheckAndIncreaseCombo(Obstacle data) {
		if (data.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
			currentCombo++;
			if (currentCombo > 1) {
				EnableCombo();
				currentComboTime = maxComboTime;

				switch (currentCombo) {
					case 2:
						DoX2Combo();
						break;
					case 3:
						DoX3Combo();
						break;
					case 4:
						DoX4Combo();
						break;
					case 5:
						DoX5Combo();
						break;
					default:
						JustDoCombo(currentCombo);
						break;
				}

				if (currentCombo > ComboHelper.MaxCombo) {
					ComboHelper.MaxCombo = currentCombo;
				}
			}
		}
	}

	private void DisableCombo() {
		comboLabel.DOFade(0, 0.5f);
		comboAmount.DOFade(0, 0.5f);
	}
	private void EnableCombo() {
		comboLabel.DOFade(1, 0.5f);
		comboAmount.DOFade(1, 0.5f);
	}

	private void DoX2Combo() {
		comboLabel.GetComponent<RectTransform>().DOShakePosition(maxComboTime, 5);
		comboLabel.colorGradientPreset = combo2xGradient;
		comboAmount.colorGradientPreset = combo2xGradient;
		comboAmount.text = "x2";
	}
	private void DoX3Combo() {
		comboLabel.GetComponent<RectTransform>().DOShakePosition(maxComboTime, 8);
		comboLabel.colorGradientPreset = combo3xGradient;
		comboAmount.colorGradientPreset = combo3xGradient;
		comboAmount.text = "x3";

	}
	private void DoX4Combo() {
		comboLabel.GetComponent<RectTransform>().DOShakePosition(maxComboTime, 10);
		comboLabel.colorGradientPreset = combo4xGradient;
		comboAmount.colorGradientPreset = combo4xGradient;
		comboAmount.text = "x4";

	}
	private void DoX5Combo() {
		comboLabel.GetComponent<RectTransform>().DOShakePosition(maxComboTime, 15);
		comboLabel.colorGradientPreset = combo5xGradient;
		comboAmount.colorGradientPreset = combo5xGradient;
		comboAmount.text = "x5";
	}

	private void JustDoCombo(int combo) {
		if (combo > 5) {
			comboLabel.GetComponent<RectTransform>().DOShakePosition(maxComboTime, 15);
		}
		comboAmount.text = "x" + combo;
	}
}
