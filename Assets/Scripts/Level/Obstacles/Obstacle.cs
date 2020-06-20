using UnityEngine;

public class Obstacle : MonoBehaviour {
    public GameObject DestroyEffect;
    public LevelObjectType TypeOfObstacle;
    public ObstacleBlock obstacleBlock;

    private void Awake() {
        if (gameObject.tag.ToLower() == "good")
            TypeOfObstacle = LevelObjectType.GOOD_BLOCK;
        else if (gameObject.tag.ToLower() == "bad")
            TypeOfObstacle = LevelObjectType.BAD_BLOCK;
        else if (gameObject.tag.ToLower() == "untagged") {
            TypeOfObstacle = LevelObjectType.NONE;
        }
        if (obstacleBlock == null) {
            Debug.LogError("Parent of obstacle is null. Object Name : " + gameObject.name + " Parent : " + transform.parent.name);
        }
    }

    public void OnPlayerHit(out LevelObjectType type) {
		type = TypeOfObstacle;
        if (gameObject.activeSelf) {
            if (obstacleBlock != null && TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
                EffectsController.instance.CameraShake();
                DoDestroyEffect();
                obstacleBlock.DestroyAllBad();
            }
            GameEventSystem.RaiseGameEvent(GAME_EVENT.OBSTACLE_HIT, gameObject);
        }
    }

    public void DoDestroyEffect() {
		if (DestroyEffect != null && gameObject.activeSelf) {
			Instantiate(DestroyEffect, transform.position, Quaternion.identity);
		}
        gameObject.SetActive(false);
    }
}
