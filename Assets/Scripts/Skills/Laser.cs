using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, ISkill {

	public LineRenderer laserLine;
	public GameObject tipEffect;

	private float laserSpeed = 10f;
	private float maxTime = 2f;
	private bool laserRunning;
	private Vector2 tempVector;

	private void Start() {
		laserRunning = false;
		laserLine.SetPosition(0, Vector2.zero);
		laserLine.SetPosition(1, Vector2.zero);
		tipEffect.SetActive(false);
	}

	private void InitLaser(Vector2 starPosition) {
		laserLine.SetPosition(0, starPosition);
		tempVector = starPosition;
		tipEffect.transform.position = starPosition;
		laserRunning = true;
		tipEffect.SetActive(true);
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			InitLaser(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		if (laserRunning && maxTime > 0) {
			maxTime -= Time.deltaTime;
			tempVector += Vector2.right * laserSpeed * Time.deltaTime;
			tipEffect.transform.position = tempVector;
			laserLine.SetPosition(1, tempVector);

			RaycastHit2D hit = Physics2D.Raycast(laserLine.GetPosition(0), Vector2.right,Vector2.Distance(laserLine.GetPosition(0),laserLine.GetPosition(1)));
			if (hit.collider != null) {
				OnRaycastHit(hit.collider);
			}
		}

		if (maxTime <= 0 && laserRunning) {
			laserRunning = false;
			DoLaserDisable();
		}
	}

	private void OnRaycastHit(Collider2D collider) {
		if (collider != null) {
			Obstacle obs = collider.GetComponent<Obstacle>();
			if (obs != null) {
				if (obs.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
					obs.OnPlayerHit(out LevelObjectType type);
				} else {
					obs.DoDestroyEffect();
				}
			}
		}
	}

	private void DoLaserDisable() {
		gameObject.SetActive(false);
		Destroy(gameObject, 3f);
	}

	public Obstacle GetTarget() {
		throw new System.NotImplementedException();
	}

	public void UseSkill(object data) {
		throw new System.NotImplementedException();
	}
}
