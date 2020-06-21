using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCollection", menuName = "Level/Level Collection", order = 2)]
public class LevelCollectionSO : ScriptableObject {
	public List<LevelSO> levels;
}

