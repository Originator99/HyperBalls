using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/New Level Data", order = 1)]
public class LevelSO : ScriptableObject {
	public LevelData levelData;
}


/// <summary>
/// Make sure to add every new vairable which is added here to LevelHelper>UpdateLevelVariables method's For loop
/// </summary>
[System.Serializable]
public class LevelData {
	public int id;
	public SceneName sceneName;
	public int difficulty;
	public int maxLives;
}
