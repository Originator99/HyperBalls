using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHolder : MonoBehaviour {
	public Text leveNumber, difficulty;
	public Transform starsPanel;
	public Image difficultyPanelImage;
	public Transform completedPanel;
	public Button button;

	public void RenderLevelBox(LevelData data) {
		difficulty.text = GetDifficulty(data.difficulty);
		difficultyPanelImage.color = GetDifficultyColor(data.difficulty);
		string _id = data.id.ToString();
		if (data.id <= 9) {
			_id = "0" + data.id;
		}
		leveNumber.text = _id;
		int count = 1;
		foreach (Transform star in starsPanel) {
			if (count <= data.difficulty) {
				star.gameObject.SetActive(true);
			} else {
				star.gameObject.SetActive(false);
			}
			count++;
		}
		if (data.completed) {
			completedPanel.gameObject.SetActive(true);
		} else {
			completedPanel.gameObject.SetActive(false);
		}

		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate() {
			Loader.Load(data.sceneName);
		});
	}

	private string GetDifficulty(int level) {
		if (level == 2)
			return "NORMAL";
		if (level == 3)
			return "HARD";
		return "EASY";
	}

	private Color GetDifficultyColor(int level) {
		if (level == 2)
			return Helper.HexToColor("93B4C3");
		if (level == 3)
			return Helper.HexToColor("CD572C");
		return Helper.HexToColor("B9D1BE");
	}
}
