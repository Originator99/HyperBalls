using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour {
    public GameObject Room;
    public LevelPart[] partsOfLevel;
    public int currentPart;

    public void Start() {
        currentPart = 0;
        GameEventSystem.OnGameEventRaised += HandleGameEvents;
    }

    private void OnDestroy() {
        GameEventSystem.OnGameEventRaised -= HandleGameEvents;
    }

    private void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
        if (type == GAME_EVENT.LEVEL_START) {

        }
        if (type == GAME_EVENT.LEVEL_END) { 
        
        }
        if (type == GAME_EVENT.GOOD_DESTROYED) {
            UpdateLevel(true);
        }
        if (type == GAME_EVENT.BAD_DESTROYED) { 
            UpdateLevel(false);
        }
    }

    private void UpdateLevel(bool isGoodObstacle) {
        if (isGoodObstacle) {
            partsOfLevel[currentPart].TotalGood -= 1;
            if (partsOfLevel[currentPart].TotalGood <= 0) {
                SwitchToNewPart();
            }
        } else {
            Debug.Log("Game over");
        }
    }

    public void SwitchToNewPart() {
        if (partsOfLevel != null && partsOfLevel.Length > currentPart) {
            partsOfLevel[currentPart].gameObject.SetActive(false);
            currentPart++;
            if (partsOfLevel.Length > currentPart) {
                partsOfLevel[currentPart].gameObject.SetActive(true);
                Room.transform.DOMoveX(partsOfLevel[currentPart].RoomPosition.position.x, 1.5f);
            } else {
                Debug.Log("Level Completed");
            }
        }
    }
}
