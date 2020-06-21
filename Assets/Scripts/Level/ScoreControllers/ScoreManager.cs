using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {
    public static int currentScore;
    public static int lives;
    public static float comboMultiplier;

    public static void InitScoreManager() {
        currentScore = 0;
        comboMultiplier = 1;
        lives = 2;
    }

    public static void AddScore(LevelObjectType type, LevelObjectDifficulty difficulty, int obstacleBlockID) {
        if (type == LevelObjectType.GOOD_BLOCK) {
            currentScore += Mathf.RoundToInt(GetScoreBasedOnID(obstacleBlockID) * comboMultiplier);
        }
        Debug.Log("Current Score : " + currentScore);
    }

    public static void IncreaseCombo() {
        comboMultiplier++;
    }
    public static void DecreaseCombo() {
        comboMultiplier--;
        if (comboMultiplier < 1) {
            comboMultiplier = 1;
        }
    }

    public static int GetScoreBasedOnID(int obstacleBlockID) {
        switch (obstacleBlockID) {
            case 1:
                return 100;
            default:
                return 50;
        }
    }

	public static int GetTotalMoney() {
		return currentScore;
	}
}
