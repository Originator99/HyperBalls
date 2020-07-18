using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillRow : MonoBehaviour {
	public Image icon;
	public Text totalCount;
	public Button button;
	public SkillData currentSkill;

	private int currentCount;

	public void RenderSkillBlock(SkillData data) {
		currentSkill = data;
		gameObject.SetActive(true);
		currentCount = InventoryHelper.GetCurrentSkillCount(data.Id);
		if (currentCount > 0) {
			icon.color = Helper.HexToColor("FFFFFFFF");
			icon.sprite = data.Icon;
			totalCount.text = currentCount.ToString();
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(delegate() {
				if (currentCount > 0) {
					GameEventSystem.RaiseGameEvent(GAME_EVENT.USE_SKILL, data.Id);
				}
			});
			DoAnimation();
		} else {
			gameObject.SetActive(false);
		}
	}

	public void AfterSkillUsed() {
		currentCount--;
		totalCount.text = currentCount.ToString();
		if (currentCount <= 0 || currentSkill.CanOnlyUseOnce) {
			icon.color = Helper.HexToColor("FFFFFF83");
			button.onClick.RemoveAllListeners();
		}
	}

	private void DoAnimation() {
		transform.localScale = new Vector3(0, 0, 0);
		transform.DOScale(new Vector3(1,1,1), 0.55f);
	}
}
