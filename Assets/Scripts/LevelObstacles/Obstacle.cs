using UnityEngine;

public class Obstacle : MonoBehaviour {
    public int defaultChildCount;
    private bool isActive;

    private void Awake() {
        defaultChildCount = transform.childCount;
        isActive = gameObject.activeSelf;
    }

    private void OnEnable() {
        isActive = true;
    }

    private void OnDisable() {
        isActive = false;
    }

    private void Update() {
        if (isActive && defaultChildCount > 0) {
            if (transform.childCount != defaultChildCount) {
                isActive = false;
                Invoke("DelayedDisable", 2f);
            }
        }
    }

    private void DelayedDisable() {
        gameObject.SetActive(false);
    }
}
