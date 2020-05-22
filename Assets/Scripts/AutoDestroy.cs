using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float delay = 5;

    private void Start() {
        Destroy(gameObject, delay);
    }
}
