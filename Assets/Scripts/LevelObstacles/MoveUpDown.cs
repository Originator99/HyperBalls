using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour {
    public float moveSpeed = 5f;

    private int direction = 1;

    private void Update() {
        if (transform.position.y > 5)
            direction = -1;
        else if (transform.position.y < -5)
            direction = 1;
        transform.position += new Vector3(0f, direction * moveSpeed * Time.unscaledDeltaTime, 0f);
    }
}
