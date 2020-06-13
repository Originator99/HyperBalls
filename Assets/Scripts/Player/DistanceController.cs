using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DistanceController : MonoBehaviour {
    public AimLine aim;
    public float moveSpeed = 5f;
    public float maxUnits = 5f;
    public PlayerDash playerDash;

	public EventTrigger clickArea;

	private bool move;

	private void Start() {
		move = false;
		if (clickArea != null && clickArea.triggers != null) {
			int total_triggers = clickArea.triggers.Count;
			for (int i = 0; i < total_triggers; i++) {
				if (clickArea.triggers[i].eventID == EventTriggerType.PointerDown) {
					clickArea.triggers[i].callback.RemoveAllListeners();
					clickArea.triggers[i].callback.AddListener((e) => {
						PointerDown(e);
					});
				} else if (clickArea.triggers[i].eventID == EventTriggerType.PointerUp) {
					clickArea.triggers[i].callback.RemoveAllListeners();
					clickArea.triggers[i].callback.AddListener((e) => {
						PointerUp(e);
					});
				}
			}
		} else {
			Debug.LogError("ERROR : Event triggers are null for dash mechanism");
		}
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			//start distace logic
		}
		if (move) {
			//distance logic
			DistanceLogic();
		}
		if (Input.GetMouseButtonUp(0)) {
			//stop distance logic
		}
	}

	private void StartDistanceLogic() {
		transform.position = playerDash.transform.position;
		aim.StartDrawing();
		playerDash.DisablePhysics();
		if (GameManager.instance != null) {
			GameManager.instance.DoSlowMotion();
		}
		move = true;
	}

	private void DistanceLogic() {
		if (Vector2.Distance(playerDash.transform.position, transform.position) < maxUnits) {
			transform.position += Vector3.right * moveSpeed * Time.unscaledDeltaTime;
		}
	}

	private void StopDistanceLogic() {
		playerDash.EnablePhysics();
		playerDash.StartDash(transform.position);
		aim.StopDrawing();
		if (GameManager.instance != null) {
			GameManager.instance.StopSlowMotion();
		}
		move = false;
	}

	#region Event Trigger methods
	private void PointerDown(BaseEventData e) {
		StartDistanceLogic();
	}
	private void PointerUp(BaseEventData e) {
		StopDistanceLogic();
	}
	#endregion
}
