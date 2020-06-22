using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardUIManager : MonoBehaviour {
    public Button PlayButton;
	public GameObject MainScreenOptions;
	public LevelSelection LevelSelectionController;
    private void Start() {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(delegate() {
			if (LevelSelectionController != null) {
				LevelSelectionController.ShowLevelSelection(ref LevelHelper.LEVELS);
			} else {
				Debug.LogError("Level Selection controller is null. Missing a reference?");
			}
        });
		LevelSelectionController.gameObject.SetActive(false);
		MainScreenOptions.SetActive(true);
    }
}
