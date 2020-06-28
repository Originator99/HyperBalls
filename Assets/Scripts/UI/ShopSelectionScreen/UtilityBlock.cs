using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityBlock : MonoBehaviour {
	public Text Name, Desc, Cost, CurrentCount;
	public Image Icon;
	public Button BuyButton;
	public Outline CostOutline;

	public void RenderBlock(SkillData data, System.Action onBuy, int currentMoney) {
		gameObject.SetActive(true);
		Name.text = data.Name.ToUpper();
		Desc.text = data.Desc;
		Cost.text = "$"+data.Cost;
		Icon.sprite = data.Icon;
		CurrentCount.text = "x" + InventoryHelper.GetCurrentSkillCount(data.Id).ToString();
		if (currentMoney >= data.Cost) {
			BuyButton.interactable = true;
			CostOutline.enabled = true;
		} else {
			BuyButton.interactable = false;
			CostOutline.enabled = false;
		}
		BuyButton.onClick.RemoveAllListeners();
		BuyButton.onClick.AddListener(delegate() {
			onBuy?.Invoke();
		});
	}
}
