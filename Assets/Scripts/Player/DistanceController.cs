using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour {
    public AimLine aim;
    public float moveSpeed = 5f;
    private PlayerDash playerDash;
    private void Start() {
        playerDash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDash>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            transform.position = playerDash.transform.position;
            aim.StartDrawing();
            GameManager.instance.DoSlowMotion();
        }
        if (Input.GetMouseButton(0)) {
            transform.position += Vector3.right * moveSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetMouseButtonUp(0)) {
            playerDash.StartDash(transform.position);
            aim.StopDrawing();
            GameManager.instance.StopSlowMotion();
        }
    }
}
