using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHolder : MonoBehaviour {
	public LevelCollectionSO levelCollectionSO;

	public void Awake() {
		LevelHelper.Init(levelCollectionSO);
	}
}
