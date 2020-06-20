using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour {
    public int _id;
    public LevelObjectDifficulty difficulty;
    public Obstacle[] obstacles;
	public bool autoDestroyBad = true;

    private void Awake() {
        if (obstacles == null && obstacles.Length <= 0) {
            Debug.LogError("Obstacle block has no obstacles assigned in arary. Name : " + transform.name);
        }
    }

    public void EnableAllObstacles() {
        int total = obstacles.Length;
        for (int i = 0; i < total; i++) {
            obstacles[i].gameObject.SetActive(true);
        }
    }

    public void DestroyAllBad() {
        if (obstacles != null && autoDestroyBad) { 
            int total = obstacles.Length;
            for (int i = 0; i < total; i++) {
                if (obstacles[i].TypeOfObstacle == LevelObjectType.BAD_BLOCK) {
                    obstacles[i].DoDestroyEffect();
                }
            }
        }
    }
}
