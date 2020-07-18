public class GameEventSystem {
	public delegate void EventRaised(GAME_EVENT type, System.Object data);
	public static event EventRaised OnGameEventRaised;
	public static void RaiseGameEvent(GAME_EVENT type, System.Object data = null) {
		OnGameEventRaised?.Invoke(type, data);
	}
}

public enum GAME_EVENT { 
	LEVEL_START,
	LEVEL_END,
	OBSTACLE_HIT,
	UPDATE_LIFE,
	USE_SKILL,
	REFRESH,
	GAME_PAUSED,
	GAME_UNPAUSED
}