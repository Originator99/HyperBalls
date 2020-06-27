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
				DoBadMissileSkill(player.position);
				break;
			case 2:
				break;
		}
	}

	private void DoBadMissileSkill(Vector3 startPosition) {
		int total_parts = levelController.partsOfLevel.Length;
		for (int i = 0; i < total_parts; i++) {
			Obstacle closest_bad_obj = levelController.partsOfLevel[i].FindClosestObstacle(startPosition, LevelObjectType.BAD_BLOCK);
			if (closest_bad_obj != null) {
				GameObject go = Instantiate(BadMissilePrefab, startPosition, Quaternion.identity);
				ISkill controller = go.GetComponent<ISkill>();
				if (controller != null) {
					controller.UseSkill(closest_bad_obj);
				} else {
					Debug.LogError("ISkill interface not implemented for : " + go.name);
				}
			}
		}
	}
}
