using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/New Level Data", order = 1)]
public class LevelSO : ScriptableObject {
	public LevelData levelData;
}

[System.Serializable]
public class LevelData {
	public int id;
	public SceneName sceneName;
	public bool completed;
	public int moneyEarned;
	public int difficulty;
}
