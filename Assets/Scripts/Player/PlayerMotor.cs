using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private readonly float JumpForceDefault = 4f;
    private bool isGrounded;
    private Rigidbody2D rb;

    private void Start() {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_START);

    }

    private void Update() {
        if (!PlayerHelper.PAUSED) { 
            HandleMovement();
        }
    }

    private void FixedUpdate() {
        if (isGrounded && !PlayerHelper.PAUSED) {
            DoJump(JumpForceDefault);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "ground") {
            isGrounded = true;
        }
        if (collision.gameObject.GetComponent<Obstacle>() != null) {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<Obstacle>().OnPlayerHit();
        }
    }

    private void DoJump(float jumpForce) {
        rb.velocity = new Vector2(Mathf.Sqrt(2f * Physics2D.gravity.magnitude * jumpForce), rb.velocity.y);
        isGrounded = false;
    }

    private void HandleMovement() {
        transform.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0f) * 7f * Time.unscaledDeltaTime;
    }

}
