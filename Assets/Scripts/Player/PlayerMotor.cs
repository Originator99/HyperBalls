using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour {
	public SimpleTouchController movementArea;
	public float MoveSpeed = 7f;

	private readonly float JumpForceDefault = 4f;
	private bool isGrounded;
	private Rigidbody2D rb;

	private void Start() {
		isGrounded = false;
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		HandleMovement();
	}

	private void FixedUpdate() {
		if (isGrounded) {
			DoJump(JumpForceDefault);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "ground") {
			isGrounded = true;
		}
	}

	private void DoJump(float jumpForce) {
		rb.velocity = new Vector2(Mathf.Sqrt(2f * Physics2D.gravity.magnitude * jumpForce), rb.velocity.y);
		isGrounded = false;
	}

	private void HandleMovement() {
#if UNITY_EDITOR
		transform.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0f) * MoveSpeed * Time.unscaledDeltaTime;
#else
		transform.position += new Vector3(0, movementArea.GetTouchPosition.y, 0f) * MoveSpeed * Time.unscaledDeltaTime;
#endif
	}
}
