using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	public string Name;
	public int TotalMoney;
	public List<PlayerSkillsData> PlayerSkillsData;

	public void GenerateFirstTimePlayerData() {
		this.Name = "Player";
		this.TotalMoney = 5000;
		this.PlayerSkillsData = new List<PlayerSkillsData>();

		PlayerSkillsData skills = new PlayerSkillsData(1,5);

		this.PlayerSkillsData.Add(skills);
	}
}

[System.Serializable]
public class PlayerSkillsData {
	public int SkillID;
	public int TotalAmount;

	public PlayerSkillsData(int skillID, int amount = 0) {
		this.SkillID = skillID;
	}
}
