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
	GOOD_DESTROYED,
	BAD_DESTROYED
}
