using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour {
    public Transform RoomPosition;
    public Transform PlayerStartPosition;
    public int TotalGood;

    public void ResetPart() {
        int total_count = transform.childCount;
        for (int i = 0; i < total_count; i++) {
            if (transform.GetChild(i).GetComponent<ObstacleBlock>() != null) {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<ObstacleBlock>().EnableAllObstacles();
            }
        }
    }
}
