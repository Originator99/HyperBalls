using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle {
    GameObject DestroyEffect { get; set; }
    void DoDestroyEffect();
    void OnPlayerHit();
    LevelObjectType TypeOfObstacle { get; set; }
}
