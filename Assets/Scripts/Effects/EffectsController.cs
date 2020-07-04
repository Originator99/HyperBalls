using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EZCameraShake;

/// <summary>
/// Every level has its own Effects Controller and variables
/// </summary>
public class EffectsController : MonoBehaviour {
    public Camera mainCam;

	[Header("Skills Prefabs")]
	public GameObject BadMissilePrefab;
	public GameObject GoodMissilePrefab;

	private LevelController levelController;

    #region Singleton
    public static EffectsController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
	#endregion

	private void Start() {
		GameObject obj = GameObject.FindGameObjectWithTag("LevelController");
		if (obj != null) {
			levelController = obj.GetComponent<LevelController>();
		}
		if (levelController == null) {
			Debug.LogError("Level Controller is null in effects controller");
		}
	}

	public void CameraShake(float power = 0.5f) {
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.1f);
    }

	public void UseSkill(int id, Transform player) {
		switch (id) {
			case 1:
				DoMissileSkill(player.position, 1);
				break;
			case 2:
				DoMissileSkill(player.position, 2);
				break;
		}
	}

	private void DoMissileSkill(Vector3 startPosition, int skillID) {
		int total_parts = levelController.partsOfLevel.Length;
		LevelObjectType type = LevelObjectType.NONE;
		GameObject missilePrefab = null;
		if (skillID == 1) {
			missilePrefab = BadMissilePrefab;
			type = LevelObjectType.BAD_BLOCK;
		} else if (skillID == 2) {
			missilePrefab = GoodMissilePrefab;
			type = LevelObjectType.GOOD_BLOCK;
		} else if (type == LevelObjectType.NONE) {
			Debug.LogError("Cant do missile skill : Invalid skill ID");
			return;
		} else if (missilePrefab == null) {
			Debug.LogError("Missile Prefab is null");
			return;
		}
		for (int i = 0; i < total_parts; i++) {
			Obstacle closest_obstacle = levelController.partsOfLevel[i].FindClosestObstacleForMissile(startPosition, type);
			if (closest_obstacle != null) {
				GameObject go = Instantiate(missilePrefab, startPosition, Quaternion.identity);
				ISkill controller = go.GetComponent<ISkill>();
				if (controller != null) {
					controller.UseSkill(closest_obstacle);
					InventoryHelper.UpdateSkills(skillID, -1);
				} else {
					Debug.LogError("ISkill interface not implemented for : " + go.name);
				}
			}
		}
	}
}
