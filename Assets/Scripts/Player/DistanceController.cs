using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour {
    public AimLine aim;
    public float moveSpeed = 5f;
    public float maxUnits = 5f;
    public PlayerDash playerDash;
    private float maxDistance;
    private void Start() {
        maxDistance = transform.position.x + maxUnits;
    }

    private void Update() {
        if (!PlayerHelper.PAUSED) {
            if (Input.GetMouseButtonDown(0)) {
                transform.position = playerDash.transform.position;
                aim.StartDrawing();
                GameManager.instance.DoSlowMotion();
                maxDistance = transform.position.x + maxUnits;
            }
            if (Input.GetMouseButton(0)) {
                if (transform.position.x < maxDistance) {
                    transform.position += Vector3.right * moveSpeed * Time.unscaledDeltaTime;
                }
            }
            if (Input.GetMouseButtonUp(0)) {
                playerDash.StartDash(transform.position);
                aim.StopDrawing();
                GameManager.instance.StopSlowMotion();
            }
            if (Input.GetKeyDown(KeyCode.PageUp)) {
                GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_START);
            }
        }
    }
}
