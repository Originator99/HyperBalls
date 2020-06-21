using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class LevelHelper {
	public static List<LevelData> LEVELS;

	private static string saveFilePath = Application.persistentDataPath + "/level.hyperballs";

	public static void Init() {
		Debug.Log("Path  : " + saveFilePath);
		LevelCollection collection = LoadLevelData();
		if (collection != null) {
			LEVELS = new List<LevelData>();
			LEVELS = collection.levels;
			Debug.Log("Level Data Fetched");
			foreach (var s in LEVELS) {
				Debug.Log(s.id + "-" + s.sceneName + "-" + s.completed);
			}
		} else {
			Debug.LogError("Level Collection type is not attached to resource fetched");
		}
	}
	public static void SaveLevelData() {
		try {
			if (LEVELS != null && LEVELS.Count > 0) {
				FileStream stream = new FileStream(saveFilePath, FileMode.Create);
				LevelCollection data = new LevelCollection();
				data.levels = LEVELS;
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, data);
				stream.Close();
				Debug.Log("Saved Level File!");
			} else {
				Debug.LogError("LEVELS Collection in Level Helper is null or empty");
			}
		} catch (Exception ex) {
			Debug.LogError("Error Saving Level File..  :" + ex.Message);
		}
	}

	public static void UpdateLevel(int id, bool isCompleted, int money_won) {
		int index = LEVELS.FindIndex(x => x.id == id);
		if (index >= 0 && LEVELS.Count > index) {
			LEVELS[index].completed = isCompleted;
			LEVELS[index].moneyEarned = money_won;
			Debug.Log("Level Data updated");
		} else {
			Debug.LogError("Cannot find the index of level id : " + id);
		}
	}

	private static LevelCollection LoadLevelData() {
		if (File.Exists(saveFilePath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(saveFilePath, FileMode.Open);
			LevelCollection data = formatter.Deserialize(stream) as LevelCollection;
			stream.Close();
			return data;
		} else {
			Debug.Log("New user, fetching the default Level data..");
			return FetchLevelDataLocally();
		}
	}

	private static LevelCollection FetchLevelDataLocally() {
		var resource = Resources.Load<LevelCollectionSO>("LevelResources/LevelCollection1");
		if (resource != null && resource.levels!=null) {
			LevelCollection collection = new LevelCollection();
			collection.levels = new List<LevelData>();
			foreach (var temp in resource.levels) {
				collection.levels.Add(temp.levelData);
			}
			return collection;
		} else {
			Debug.LogError("Could not find level collection asset in resources folder");
		}
		return null;
	}
}
