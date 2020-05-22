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
    public GameObject goodDestroy;
    public GameObject badDestroy;

    #region Singleton
    public static EffectsController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    #endregion

    private void Start() {
    }

    public void CameraShake(float power = 0.5f) {
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.1f);
    }
    public void GoodBlockDestroyEffect(Vector2 pos) {
        GameObject obj = Instantiate(goodDestroy, transform) as GameObject;
        obj.transform.position = pos;
    }
    public void BadBlockDestroyEffect(Vector2 pos) {
        GameObject obj = Instantiate(badDestroy, transform) as GameObject;
        obj.transform.position = pos;
    }
}
