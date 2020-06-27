using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour {
    public Transform RoomPosition;
    public Transform PlayerStartPosition;
    public int TotalGood;
	[HideInInspector]
	public bool IsCurrentlyActive;

    public void ResetPart() {
        int total_count = transform.childCount;
        for (int i = 0; i < total_count; i++) {
            if (transform.GetChild(i).GetComponent<ObstacleBlock>() != null) {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<ObstacleBlock>().EnableAllObstacles();
            }
        }
    }

	public Obstacle FindClosestObstacle(Vector3 startPosition, LevelObjectType type) {
		Obstacle closest = null;
		if (IsCurrentlyActive) {
			int total_count = transform.childCount;
			for (int i = 0; i < total_count; i++) {
				ObstacleBlock block = transform.GetChild(i).GetComponent<ObstacleBlock>();
				if (block != null) {
					int total_obstacles = block.obstacles.Length;
					for (int j = 0; j < total_obstacles; j++) {
						if (block.obstacles[j].gameObject.activeSelf && block.obstacles[j].TypeOfObstacle == type) {
							if (closest != null && Vector3.Distance(startPosition, block.obstacles[j].transform.position) < Vector3.Distance(startPosition, closest.transform.position)) {
								closest = block.obstacles[j];
							} else {
								closest = block.obstacles[j];
							}
						}
					}
				}
			}
		}
		return closest;
	}
}
