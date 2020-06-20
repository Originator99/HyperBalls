using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour {
    private float moveSpeed = 5f;

    private int direction = 1;

	private void Start() {
		moveSpeed = Random.Range(4f, 9f);
	}

	private void Update() {
        if (transform.position.y > 5)
            direction = -1;
        else if (transform.position.y < -5)
            direction = 1;
        transform.position += new Vector3(0f, direction * moveSpeed * Time.deltaTime, 0f);
    }
}
