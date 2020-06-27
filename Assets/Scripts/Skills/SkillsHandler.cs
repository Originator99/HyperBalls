using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkillsHandler {

	#region Bad Missile Methods and variables
	private static List<string> badMissileList;

	public static void OnBadMissileLaunched(string id) {
		if (badMissileList == null) {
			badMissileList = new List<string>();
		}
		badMissileList.Add(id);
	}
	public static void OnBadMissileCollide(string id) {
		if (badMissileList != null) {
			badMissileList.Remove(id);
		}
	}
	public static bool CheckIfTargetExistsForBadMissile(string id) {
		if (badMissileList != null && badMissileList.Count > 0) {
			return badMissileList.Contains(id);
		}
		return false;
	}
	#endregion
}
