using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelection : MonoBehaviour {
	public GameObject levelPrefab;
	public Transform levelContent;

	public Button backButton;
	public ScrollRect scollRect;

	private void Start() {
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(delegate() {
			gameObject.SetActive(false);
		});
	}

	public void ShowLevelSelection(List<LocalLevelData> data) {
		if (data != null) {
			int new_panels = data.Count - levelContent.childCount;
			for (int i = 0; i < new_panels; i++) {
				Instantiate(levelPrefab, levelContent).gameObject.SetActive(false);
			}
			int total = levelContent.childCount;
			int index_counter = 0;
			for (int i = 0; i < total; i++) {
				Transform child = levelContent.GetChild(i);
				if (data.Count > index_counter) {
					LevelHolder holder = child.GetComponent<LevelHolder>();
					if (holder != null) {
						holder.RenderLevelBox(data[index_counter]);
						index_counter++;
					}
				}
			}
			gameObject.SetActive(true);
		} else {
			Debug.LogError("Level data list is null");
			gameObject.SetActive(false);
		}
	}
}
