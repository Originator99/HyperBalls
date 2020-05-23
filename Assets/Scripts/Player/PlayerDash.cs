using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour {
    public float DashSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 target;
    private bool isMoving;
    private float defaultGravityScale;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
        target = transform.position;
    }

    private void Update() {
        if (isMoving) {
            if (Vector3.Distance(transform.position, target) > 1f) {
                transform.position = Vector3.MoveTowards(transform.position, target, DashSpeed * Time.unscaledDeltaTime);
            } else {
                isMoving = false;
                rb.gravityScale = defaultGravityScale;
            }
        }
    }

    public void StartDash(Vector3 target) {
        rb.gravityScale = 0;
        this.target = target;
        isMoving = true;
    }
}

public class PlayerHelper {
    public static bool PAUSED = false;
}
