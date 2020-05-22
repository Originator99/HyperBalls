using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private float JumpForceDefault = 4f;
    private bool isGrounded;
    private Rigidbody2D rb;

    private float horizontalExtent, verticleExtent;

    private void Start() {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        verticleExtent = Camera.main.orthographicSize;
        Debug.Log("Hor Extent : " + horizontalExtent + " Verticle Extent " + verticleExtent);
    }

    private void Update() {
        HandleMovement();
    }

    private void FixedUpdate() {
        if (isGrounded) {
            DoJump(JumpForceDefault);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isGrounded = true;
        } else if (collision.gameObject.tag == "Good") {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Bad") {
            Debug.Log("Game over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "ground") {
            isGrounded = true;
        } else if (collision.gameObject.tag == "Good") {
            GameEventSystem.RaiseGameEvent(GAME_EVENT.GOOD_DESTROYED);
            EffectsController.instance.CameraShake();
            EffectsController.instance.GoodBlockDestroyEffect(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Bad") {
            GameEventSystem.RaiseGameEvent(GAME_EVENT.BAD_DESTROYED);
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
