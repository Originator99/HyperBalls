using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class InventoryHelper {
	public static List<SkillData> SKILLS;
	public static PlayerData PLAYER_DATA;

	private static readonly string playerDataPath = Application.persistentDataPath + "/playerdata.hyperballs";

	public static void Init() {
		FetchSkillDataLocally();
		PLAYER_DATA = FetchPlayerDataLocally();
	}

	public static void SavePlayerData() {
		try {
			if (PLAYER_DATA != null) {
				FileStream stream = new FileStream(playerDataPath, FileMode.Create);
				PlayerData data = PLAYER_DATA;
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, data);
				stream.Close();
				Debug.Log("Saved player data File!");
			} else {
				Debug.LogError("Player data in Inventory Helper is null or empty");
			}
		} catch (Exception ex) {
			Debug.LogError("Error Saving Player Data File..  :" + ex.Message);
		}
	}

	private static PlayerData FetchPlayerDataLocally() {
		if (File.Exists(playerDataPath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(playerDataPath, FileMode.Open);
			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			return data;
		} else {
			Debug.Log("New user, creating default data");
			PlayerData data = new PlayerData();
			data.GenerateFirstTimePlayerData();
			return data;
		}
	}

	private static void FetchSkillDataLocally() {
		var resource = Resources.Load<SkillsSO>("SkillsResources/SkillsData");
		if (resource != null && resource.skills != null) {
			SKILLS = new List<SkillData>();
			SKILLS = resource.skills;
		} else {
			Debug.LogError("Could not find Skills collection asset in resources folder");
		}
	}

	public static void UpdateMoney(int amount) {
		if (PLAYER_DATA != null) {
			PLAYER_DATA.TotalMoney += amount;
			if (PLAYER_DATA.TotalMoney < 0) {
				PLAYER_DATA.TotalMoney = 0;
			}
		}
	}

	public static void UpdateSkills(int skillID, int amount) {
		if (PLAYER_DATA != null && PLAYER_DATA.PlayerSkillsData != null) {
			if (PLAYER_DATA.PlayerSkillsData.FindIndex(x => x.SkillID == skillID) == -1) {
				PlayerSkillsData data = new PlayerSkillsData(skillID, 0);
				PLAYER_DATA.PlayerSkillsData.Add(data);
			}
			int total = PLAYER_DATA.PlayerSkillsData.Count;
			for (int i = 0; i < total; i++) {
				if (PLAYER_DATA.PlayerSkillsData[i].SkillID == skillID) {
					PLAYER_DATA.PlayerSkillsData[i].TotalAmount += amount;
					if (PLAYER_DATA.PlayerSkillsData[i].TotalAmount < 0) {
						PLAYER_DATA.PlayerSkillsData[i].TotalAmount = 0;
					}
					i = total;
				}
			}
		}
	}

	public static int GetCurrentSkillCount(int skillID){
		if (PLAYER_DATA != null && PLAYER_DATA.PlayerSkillsData != null) {
			int index = PLAYER_DATA.PlayerSkillsData.FindIndex(x => x.SkillID == skillID);
			if (index != -1) {
				return PLAYER_DATA.PlayerSkillsData[index].TotalAmount;
			}
		}
		return 0;
	}
}
