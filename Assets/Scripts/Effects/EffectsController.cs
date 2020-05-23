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

    #region Singleton
    public static EffectsController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    #endregion

    public void CameraShake(float power = 0.5f) {
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.1f);
    }
}
