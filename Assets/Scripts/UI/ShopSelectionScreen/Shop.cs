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

	private int currentMoney;
	private bool moneyUsed;

	public void Start() {
		backButton.onClick.RemoveAllListeners();
		backButton.onClick.AddListener(delegate () {
			if (moneyUsed) {
				InventoryHelper.SavePlayerData();
			}
			GameEventSystem.RaiseGameEvent(GAME_EVENT.REFRESH);
			gameObject.SetActive(false);
		});
	}

	public void ShowShop(ref List<SkillData> skills) {
		currentMoney = InventoryHelper.PLAYER_DATA.TotalMoney;
		totalMoney.text = "$" + currentMoney.ToString();
		moneyUsed = false;
		if (skills != null) {
			gameObject.SetActive(true);
			List<SkillData> utilities = skills.FindAll(x => x.SkillType == SkillType.UTILITY);
			RenderUtilitySection(utilities);
		} else {
			Debug.LogError("Skills is null, cannot render shop");
		}
	}

	private void RenderUtilitySection(List<SkillData> data) {
		if (data != null) {
			int new_panels = data.Count - utilitesContent.childCount;
			for (int i = 0; i < new_panels; i++) {
				Instantiate(utilitiesPrefab, utilitesContent).SetActive(false);
			}
			int total = utilitesContent.childCount;
			int index_counter = 0;
			for (int i = 0; i < total; i++) {
				Transform child = utilitesContent.GetChild(i);
				UtilityBlock block = child.GetComponent<UtilityBlock>();
				if (block != null) {
					SkillData skill = data[index_counter];
					block.RenderBlock(skill,delegate() {
						OnBuyCallback(skill.Cost, skill.Id);
						RenderUtilitySection(data);
					}, currentMoney);
					index_counter++;
				}
			}
		} else {
			Debug.LogError("Skill Data list for utilities is null");
		}
	}

	private void OnBuyCallback(int cost, int skillId) {
		currentMoney -= cost;
		totalMoney.text = "$" + currentMoney.ToString();
		InventoryHelper.UpdateMoney(-cost);
		InventoryHelper.UpdateSkills(skillId, 1);
		moneyUsed = true;
	}
}
