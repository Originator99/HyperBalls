using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSkillsPanel : MonoBehaviour {
	public GameObject skillsOptionPrefab;
	public Transform content;

	private void Start() {
		if (skillsOptionPrefab == null) {
			Debug.LogError("Skills option prefab is null. Add reference maybe?");
		}
	}

	public void RenderSkillsPanel(List<SkillData> data) {
		if (data != null) {
			if (skillsOptionPrefab != null) {
				int new_panels = data.Count - content.childCount;
				for (int i = 0; i < new_panels; i++) {
					Instantiate(skillsOptionPrefab, content).gameObject.SetActive(false);
				}
			}

			int total = content.childCount;
			int index = 0;
			for (int i = 0; i < total; i++) {
				SkillRow row = content.GetChild(i).GetComponent<SkillRow>();
				SkillData skill_data = data[index];
				if (row != null) {
					if (EffectsController.instance.GetCurrentLevel() >= skill_data.UnlocksAtLevel) {
						row.RenderSkillBlock(skill_data);
					}
					index++;
				}
			}
		} else {
			Debug.LogError("Skill data is null");
		}
	}

	public void AfterSkillUsed(int skillId) {
		foreach (Transform child in content) {
			SkillRow row = child.GetComponent<SkillRow>();
			if (row != null && row.currentSkill.Id == skillId) {
				row.AfterSkillUsed();
			}
		}
	}
}
