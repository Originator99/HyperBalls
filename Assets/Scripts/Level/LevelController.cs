using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour {
    public GameObject Room;
    public LevelPart[] partsOfLevel;
    public int currentPart;

    private GameObject playerGO;
    private LevelUpdate levelUpdate;

    public void Start() {
        currentPart = 0;
        GameEventSystem.OnGameEventRaised += HandleGameEvents;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO == null) {
            Debug.LogError("Player GO is null in Level Controller");
        }
        levelUpdate = GetComponent<LevelUpdate>();
        if (levelUpdate == null) {
            Debug.LogError("Level update is null. Is it attached in the same GO as Level Controller?");
        }
    }

    private void OnDestroy() {
        GameEventSystem.OnGameEventRaised -= HandleGameEvents;
    }

    private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
        if (type == GAME_EVENT.LEVEL_START) {
            RestartLevel();
        }
        if (type == GAME_EVENT.LEVEL_END && data!=null && data.GetType() == typeof(bool)) {
			OnLevelEnd((bool)data);
        }
    }

    private void RestartLevel() {
        playerGO.gameObject.SetActive(false);
        int totalParts = partsOfLevel.Length;
        for (int i = 0; i < totalParts; i++) {
            partsOfLevel[i].gameObject.SetActive(false);
        }
        levelUpdate.ResetLevel();
        TrySwitchPart(0);
    }

    public void ResetPart() {
        StartCoroutine(ResetPostions());
    }

    private IEnumerator ResetPostions() {
        playerGO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        partsOfLevel[currentPart].ResetPart();
        playerGO.transform.position = partsOfLevel[currentPart].PlayerStartPosition.position;
        Room.transform.position = partsOfLevel[currentPart].RoomPosition.position;
        yield return new WaitForSeconds(0.5f);
        playerGO.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        playerGO.gameObject.SetActive(true);
    }

    public bool TrySwitchPart(int index) {
        if (partsOfLevel.Length > index) {
            currentPart = index;
            partsOfLevel[currentPart].gameObject.SetActive(true);
            partsOfLevel[currentPart].ResetPart();
            levelUpdate.GOOD_BLOCK_COUNTER = partsOfLevel[currentPart].TotalGood;
            playerGO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(SwitchPart());
            return true;
        } else {
            return false;
        }
    }

    public int GetNextPartIndex() {
        return currentPart + 1;
    }

    private IEnumerator SwitchPart() {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_PAUSED);
        yield return new WaitForSeconds(0.5f);
        playerGO.SetActive(false);
        Room.transform.DOMoveX(partsOfLevel[currentPart].RoomPosition.position.x, 1.5f).OnComplete(delegate () {
            playerGO.transform.position = partsOfLevel[currentPart].PlayerStartPosition.position;
            playerGO.SetActive(true);
			GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_UNPAUSED);
		});
    }

	public void OnLevelEnd(bool has_won) {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_PAUSED);
		if (has_won) {
			Debug.Log("WON !");
		} else {
			playerGO.GetComponent<PlayerController>().OnPlayerDead();
			Debug.Log("LOST");
		}
	}
}
