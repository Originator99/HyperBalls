using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Slow Motion Variables")]
    public float slowdownFactor = 0.25f;
    public float slowdownLength = 2f;
    private bool stopSlowDown;

    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
			LevelHelper.Init();
		}
	}

    #endregion

    private void Start() {
        GameEventSystem.OnGameEventRaised += HandleGameEvents;
    }

	private void OnApplicationQuit() {
		LevelHelper.SaveLevelData();
	}

	private void Update() {
        HandleSlowMotion();
    }

    private void OnDestroy() {
        GameEventSystem.OnGameEventRaised -= HandleGameEvents;
    }

    private void HandleGameEvents(GAME_EVENT type, System.Object data = null) { 
        
    }

    #region Slow Motion

    public void DoSlowMotion() {
        stopSlowDown = false;
        if (Time.timeScale < 1) {
            Time.timeScale = 1;
        }
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
    #endregion
}
