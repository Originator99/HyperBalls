using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkillsHandler {
	#region Handler
	private static List<MissileInfo> missileList;

	public static void OnMissileLaunched(string id) {
		if (!string.IsNullOrWhiteSpace(id)) {
			if (missileList == null) {
				missileList = new List<MissileInfo>();
			}
			MissileInfo info = new MissileInfo(id);
			missileList.Add(info);
		} else {
			Debug.LogWarning("Missile ID aka obstacle id is empty or null");
		}
	}

	public static void OnMissileCollide(string id) {
		if (missileList != null && !string.IsNullOrEmpty(id)) {
			int index = missileList.FindIndex(x => x.id == id);
			if (index != -1) {
				missileList.RemoveAt(index);
			}
		}
	}

	public static bool CheckIfTargetExists(string id) {
		if (missileList != null && !string.IsNullOrEmpty(id)) {
			int index = missileList.FindIndex(x => x.id == id);
			return index != -1;
		}
		return false;
	}
	#endregion
}

public class MissileInfo {
	public string id;

	public MissileInfo(string _id) {
		id = _id;
	}
}
