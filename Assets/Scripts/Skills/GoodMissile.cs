﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodMissile : MonoBehaviour, ISkill {
	private float speed = 10f;
	private float timeout = 3f;


	private bool acquired_target;
	private Obstacle target;

	private void Update() {
		if (acquired_target) {
			Vector3 diff = target.transform.position - transform.position;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.unscaledDeltaTime);
		}
		CheckForTimeout();
	}

	private void CheckForTimeout() {
		//means the object has been disabled from elsewhere and the missile keeps spinning
		if (Vector3.Distance(transform.position, target.transform.position) <= 0.1f) {
			timeout -= Time.deltaTime;
			if (timeout <= 0) {
				acquired_target = false;
				gameObject.SetActive(false);
				Destroy(gameObject, 3f);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
		if (obs != null && obs.UniqueID == target.UniqueID) {
			if (obs.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
				obs.OnPlayerHit(out LevelObjectType type);
			} else {
				Debug.LogWarning("Good Missile Script collided with obstacle which is not good.. So just destroying the objection now");
				obs.DoDestroyEffect();
			}
			acquired_target = false;
			SkillsHandler.OnMissileCollide(target.UniqueID);
			gameObject.SetActive(false);
			Destroy(gameObject, 3f);
		}
	}

	public void UseSkill(object data) {
		gameObject.SetActive(true);
		if (data != null && data.GetType() == typeof(Obstacle)) {
			target = (Obstacle)data;
			SkillsHandler.OnMissileLaunched(target.UniqueID);
			acquired_target = true;
			Debug.Log("Target Accquired : " + target);
		} else {
			Debug.Log("Could not find target");
		}
	}

	public Obstacle GetTarget() {
		return target;
	}
}
