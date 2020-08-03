using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelCollection {
	public List<LocalLevelData> localLevelData;
}

[System.Serializable]
public class LocalLevelData {
	public int id;
	public bool completed;
	public int moneyEarned;
	public SceneName sceneName;
	public int difficulty;
	public int maxLives;
}