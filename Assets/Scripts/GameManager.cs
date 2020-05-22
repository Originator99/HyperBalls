using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Header("Slow Motion Variables")]
    public float slowdownFactor = 0.25f;
    public float slowdownLength = 2f;
    private bool stopSlowDown;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {
        HandleSlowMotion();
    }

    public void DoSlowMotion() {
        stopSlowDown = false;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void StopSlowMotion() {
        stopSlowDown = true;
    }
    private void HandleSlowMotion() {
        if (stopSlowDown) {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale >= 1) {

            }
        }
    }
}
