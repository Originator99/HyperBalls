using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour {
    public GameObject Room;
	public LevelSO levelData;
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
		} else {
			playerGO.SetActive(false);
		}
        levelUpdate = GetComponent<LevelUpdate>();
        if (levelUpdate == null) {
            Debug.LogError("Level update is null. Is it attached in the same GO as Level Controller?");
        }
		if (levelData == null) {
			Debug.LogError("Level Data is null");
		} else {
			var temp = LevelHelper.GetLevelData(levelData.levelData.id);
			if (temp != null) {
				levelData.levelData = temp;
			}
		}

		Invoke("StartLevel", 1f);
	}

	private void StartLevel() {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_START);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.PageUp)) {
			GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_UNPAUSED); //because game is paused when player is died
			GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_START);
		}

		if (Input.GetKeyDown(KeyCode.PageDown)) {
			GameEventSystem.RaiseGameEvent(GAME_EVENT.USE_SKILL, 1);
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
			partsOfLevel[currentPart].IsCurrentlyActive = true;
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

	public void DeactiveCurrentPart() {
		partsOfLevel[currentPart].IsCurrentlyActive = false;
	}

	private IEnumerator SwitchPart() {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_PAUSED);
		playerGO.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		Room.transform.DOMoveX(partsOfLevel[currentPart].RoomPosition.position.x, 1.5f).OnComplete(delegate () {
            playerGO.transform.position = partsOfLevel[currentPart].PlayerStartPosition.position;
            playerGO.SetActive(true);
			GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_UNPAUSED);
		});
    }

	/// <summary>
	/// Runs when life of the player is decrase.
	/// </summary>
	public void ResetPlayerPosition() {
		StartCoroutine(ResetPlayer());
	}

	private IEnumerator ResetPlayer() {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_PAUSED);
		playerGO.SetActive(false);
		playerGO.transform.position = partsOfLevel[currentPart].PlayerStartPosition.position;
		yield return new WaitForSeconds(0.5f);
		playerGO.SetActive(true);
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_UNPAUSED);

	}

	private  void OnLevelEnd(bool has_won) {
		GameEventSystem.RaiseGameEvent(GAME_EVENT.GAME_PAUSED);
		int money_earned = 0;
		if (has_won) {
			Debug.Log("WON !");
			if (levelData != null) {
				money_earned = ScoreManager.GetTotalMoney(levelData.levelData.completed);
				LevelHelper.UpdateLevel(levelData.levelData.id, true, money_earned);
				LevelHelper.SaveLevelData();
				InventoryHelper.UpdateMoney(money_earned);
				InventoryHelper.SavePlayerData();
			} else {
				Debug.LogError("Cannot update level, levelData is null");
			}
		} else {
			playerGO.GetComponent<PlayerController>().OnPlayerDead();
			Debug.Log("LOST");
		}

		if (LevelUIManager.instance != null) {
			StartCoroutine(LevelUIManager.instance.ShowGameOverScreen(has_won, money_earned));
		}
	}
}
