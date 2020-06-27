using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds the nearest red / bad item and blows it
/// </summary>
public class BadMissile : MonoBehaviour, ISkill {

	private float speed = 10f;

	private bool acquired_target;
	private Obstacle target;

	private void Update() {
		if (acquired_target) {
			Vector3 diff = target.transform.position - transform.position;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("Trigger enter called " + collision.gameObject.name);
		Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
		if (obs != null && obs.UniqueID == target.UniqueID) {
			obs.DoDestroyEffect();
			acquired_target = false;
			SkillsHandler.OnBadMissileCollide(target.UniqueID);
			gameObject.SetActive(false);
			Destroy(gameObject, 3f);
			Debug.Log("Target position reached");
		}
	}

	public void UseSkill(System.Object data) {
		gameObject.SetActive(true);
		if (data != null && data.GetType() == typeof(Obstacle)) {
			target = (Obstacle)data;
			SkillsHandler.OnBadMissileLaunched(target.UniqueID);
			acquired_target = true;
			Debug.Log("Target Accquired : " + target);
		} else {
			Debug.Log("Could not find target");
		}
	}
}
