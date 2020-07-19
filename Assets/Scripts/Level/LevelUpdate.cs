using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpdate : MonoBehaviour {
    public int GOOD_BLOCK_COUNTER { get; set; }

    private LevelController levelController;

    private void Start() {
        levelController = GetComponent<LevelController>();
        if (levelController == null) {
            Debug.LogError("Level controller is null, is it attached to the same game object as Level update ?");
        }
        GameEventSystem.OnGameEventRaised += HandleGameEvents;
    }
    private void OnDestroy() {
        GameEventSystem.OnGameEventRaised -= HandleGameEvents;
    }

    public void HandleGameEvents(GAME_EVENT type, System.Object data = null) {
        if (type == GAME_EVENT.OBSTACLE_HIT) {
            if (data != null) {
                try {
                    OnObstacleHit(data);
                } catch (Exception ex) {
                    Debug.LogError("Error on obstacle hit : " + ex.Message);
                }
            }
        }
    }

    public void ResetLevel() {
		ScoreManager.InitScoreManager();
		GameEventSystem.RaiseGameEvent(GAME_EVENT.UPDATE_LIFE, levelController.levelData.levelData.maxLives);
	}

    /// <summary>
    /// Called when player hits an obstacle. Adds score, changes level parts and ends the game if no more parts remain
    /// </summary>
    /// <param name="data"></param>
    private void OnObstacleHit(System.Object hitObject) {
        Obstacle data = null;
        if (hitObject != null) {
            GameObject GO = hitObject as GameObject;
            data = GO.GetComponent<Obstacle>();
        }
        if (data != null) {
            if (data.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
                ScoreManager.AddScore(data.TypeOfObstacle, data.obstacleBlock.difficulty, data.obstacleBlock._id);
                GOOD_BLOCK_COUNTER--;
                if (GOOD_BLOCK_COUNTER <= 0) {
					levelController.DeactiveCurrentPart();
                    bool gameEnd = levelController.TrySwitchPart(levelController.GetNextPartIndex());
                    if (!gameEnd) {
                        GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_END, true);
                    }
                }
            } else if(data.TypeOfObstacle == LevelObjectType.BAD_BLOCK) {
				int current = ScoreManager.lives - 1;
				GameEventSystem.RaiseGameEvent(GAME_EVENT.UPDATE_LIFE, -1);
				if (current <= 0) {
                    GameEventSystem.RaiseGameEvent(GAME_EVENT.LEVEL_END, false);
                } else {
					EffectsController.instance?.PlayPlayerHit();
					levelController.ResetPlayerPosition();
                }
            }
        }
    }
}
