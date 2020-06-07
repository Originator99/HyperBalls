using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour {
    public AimLine aim;
    public float moveSpeed = 5f;
    public float maxUnits = 5f;
    public PlayerDash playerDash;

    private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			transform.position = playerDash.transform.position;
			aim.StartDrawing();
			playerDash.DisablePhysics();
			GameManager.instance.DoSlowMotion();
		}
		if (Input.GetMouseButton(0)) {
			if (Vector2.Distance(playerDash.transform.position, transform.position) < maxUnits) {
				transform.position += Vector3.right * moveSpeed * Time.unscaledDeltaTime;
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			playerDash.EnablePhysics();
			playerDash.StartDash(transform.position);
			aim.StopDrawing();
			GameManager.instance.StopSlowMotion();
		}
	}
}
