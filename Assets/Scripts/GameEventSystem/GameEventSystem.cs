public class GameEventSystem {
	public delegate void EventRaised(GAME_EVENT type, System.Object data);
	public static event EventRaised OnGameEventRaised;
	public static void RaiseGameEvent(GAME_EVENT type, System.Object data = null) {
		if (OnGameEventRaised != null) {
			OnGameEventRaised(type, data);
		}
	}
}

public enum GAME_EVENT { 
	LEVEL_START,
	LEVEL_END,
	OBSTACLE_HIT,
	GAME_PAUSED,
	GAME_UNPAUSED
}
