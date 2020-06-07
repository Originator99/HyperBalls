using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will controll all the items related to player. Like Aim , dash etc. This will always be attached to a GO in scene
/// </summary>
public class PlayerController : MonoBehaviour {
	public GameObject AimGO;
	public PlayerMotor PlayerMotor;
	public PlayerDash PlayerDash;
	public GameObject DyingEffect;
	private void Start() {
		GameEventSystem.OnGameEventRaised += HandleGameEvents;
	}
	private void OnDestroy() {
		GameEventSystem.OnGameEventRaised -= HandleGameEvents;
	}

	public void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
		if (type == GAME_EVENT.GAME_PAUSED) { //called from LevelController when the parts are being switched
			OnGamePaused();
		} else if (type == GAME_EVENT.GAME_UNPAUSED) { //Called from LevelController when the parts are DONE being switched
			OnGameUnPaused();
		}
	}

	public void OnPlayerDead() {
		Instantiate(DyingEffect, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}

	private void OnGamePaused() {
		AimGO.SetActive(false);
		PlayerMotor.enabled = false;
		PlayerDash.enabled = false;
	}

	private void OnGameUnPaused() {
		AimGO.SetActive(true);
		PlayerMotor.enabled = true;
		PlayerDash.enabled = true;
	}
}
