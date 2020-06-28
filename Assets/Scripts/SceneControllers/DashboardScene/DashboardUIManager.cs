using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardUIManager : MonoBehaviour {
    public Button PlayButton, ShopButton;
	public GameObject MainScreenOptions;
	public LevelSelection LevelSelectionController;
	public Shop ShopController;

	[Header("Right part")]
	public Text TotalMoneyLabel;


	private void Awake() {
		GameEventSystem.OnGameEventRaised += HandleGameEvents;
	}

	private void Start() {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(delegate() {
			if (LevelSelectionController != null) {
				LevelSelectionController.ShowLevelSelection(ref LevelHelper.LEVELS);
			} else {
				Debug.LogError("Level Selection controller is null. Missing a reference?");
			}
        });
		ShopButton.onClick.RemoveAllListeners();
		ShopButton.onClick.AddListener(delegate() {
			if (ShopController != null) {
				ShopController.ShowShop(ref InventoryHelper.SKILLS);
			} else {
				Debug.LogError("Shop Controller is null. Missing a reference?");
			}
		});
		LevelSelectionController.gameObject.SetActive(false);
		ShopController.gameObject.SetActive(false);
		MainScreenOptions.SetActive(true);

		RefreshDashboard();
	}

	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}

	private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.REFRESH) {
			RefreshDashboard();
		}
	}

	private void RefreshDashboard() {
		TotalMoneyLabel.text = InventoryHelper.PLAYER_DATA != null ? "$" + InventoryHelper.PLAYER_DATA.TotalMoney : "$0";
	}
}
