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
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position = playerDash.transform.position;
            aim.StartDrawing();
            GameManager.instance.DoSlowMotion();
        }
        if (Input.GetKey(KeyCode.Space)) {
            transform.position += Vector3.right * moveSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            playerDash.StartDash(transform.position);
            aim.StopDrawing();
            GameManager.instance.StopSlowMotion();
        }
    }
}
