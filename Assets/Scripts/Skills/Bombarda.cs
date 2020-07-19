using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bombarda : MonoBehaviour, ISkill {
	public GameObject trailTrail, headTrail;

	private void Start() {
		trailTrail.SetActive(false);
		headTrail.SetActive(false);
	}

	private IEnumerator DoSequence(Vector3 start_position) {
		transform.position = start_position;
		transform.DOMoveY(start_position.y + 2f, 1f);
		yield return new WaitForSeconds(1f);
		trailTrail.SetActive(true);
		transform.DOMoveX(transform.position.x + 35f, 3.5f);
		headTrail.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
		if (obs != null) {
			if (obs.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
				obs.OnPlayerHit(out LevelObjectType type);
			} else {
				obs.DoDestroyEffect();
			}
		}
	}

	public Obstacle GetTarget() {
		throw new System.NotImplementedException();
	}

	public void UseSkill(object data) {
		gameObject.SetActive(true);
		if (data != null && data.GetType() == typeof(Vector3)) {
			StartCoroutine(DoSequence((Vector3)data));
		} else {
			Debug.LogError("Start position for Bombard is null or has a different type ");
		}
	}
}
