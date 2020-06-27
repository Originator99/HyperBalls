using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
	public GameObject utilitiesPrefab;
	public Transform utilitesContent;
	public GameObject utilitiesGO;

	public Text totalMoney;
	public Button backButton;

	public void Start() {
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(delegate () {
			InventoryHelper.SavePlayerData();
			gameObject.SetActive(false);
		});
	}

	public void ShowShop(ref List<SkillData> skills) {
		if (skills != null) {
			gameObject.SetActive(true);
			List<SkillData> utilities = skills.FindAll(x => x.SkillType == SkillType.UTILITY);
			RenderUtilitySection(utilities);
		} else {
			Debug.LogError("Skills is null, cannot render shop");
		}
	}

	private void RenderUtilitySection(List<SkillData> data) {

	}
}
