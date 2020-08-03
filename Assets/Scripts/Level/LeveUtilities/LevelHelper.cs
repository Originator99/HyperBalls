using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class LevelHelper {
	public static List<LocalLevelData> LEVELS;

	private static readonly string saveFilePath = Application.persistentDataPath + "/level.hyperballs";

	private static bool Initialized;

	public static void Init(LevelCollectionSO collection) {
		if (!Initialized) {
			Debug.Log("Levels local Path  : " + saveFilePath);
			LevelCollection data = LoadLevelData();
			if (data != null && data.localLevelData != null) {
				LEVELS = new List<LocalLevelData>();
				LEVELS = data.localLevelData;
				Debug.Log("Level Data fetched, updating variables..");
				UpdateLevelVariables(collection);

				Initialized = true;
			} else {
				Debug.LogError("Level Collection type is not attached to resource fetched");
			}
		}
	}

	public static void UpdateLevelVariables(LevelCollectionSO collection) {
		if (collection != null && collection.levels != null) {
			int total_levels = collection.levels.Count;
			for (int i = 0; i < total_levels ; i++) {
				LevelData levelData = collection.levels[i].levelData;
				int index = LEVELS.FindIndex(x => x.id == levelData.id);
				LocalLevelData data = null;
				if (index < 0) {
					data = new LocalLevelData();
					LEVELS.Add(data);
				} else {
					data = LEVELS[index];
				}

				data.id = levelData.id;
				data.sceneName = levelData.sceneName;
				data.maxLives = levelData.maxLives;
				data.difficulty = levelData.difficulty;
			}
			Debug.Log("Level Data Variable Updated");
		} else {
			Debug.LogError("Level collection SO is null");
		}
	}

	public static void SaveLevelData() {
		try {
			if (LEVELS != null && LEVELS.Count > 0) {
				FileStream stream = new FileStream(saveFilePath, FileMode.Create);
				LevelCollection data = new LevelCollection();
				data.localLevelData = LEVELS;
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
			Debug.Log("New user, returning default data");
			LevelCollection data = new LevelCollection {
				localLevelData = new List<LocalLevelData>()
			};
			return data;
		}
	}

	public static LocalLevelData GetLocalLevelData(int id) {
		int index = LEVELS.FindIndex(x => x.id == id);
		if (index >= 0) {
			return LEVELS[index];
		}
		Debug.LogError("Local Data not found for level with ID : " + id);
		return null;
	}

	private static void Extension() {
		for (int i = 0; i < 100; i++) {
			LEVELS.Add(LEVELS[0]);
		}
	}
}
