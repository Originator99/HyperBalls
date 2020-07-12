using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Nuclear : MonoBehaviour, ISkill {
	public GameObject bomb, skull, ripple;

	private void Start() {
		bomb.SetActive(false);
		skull.SetActive(false);
		ripple.SetActive(false);
	}

	private IEnumerator DoSequence(Vector3 end_position) {
		skull.transform.position = end_position;
		bomb.transform.position = new Vector3(end_position.x, end_position.y + 20);

		bomb.transform.DOMoveY(0, 0.75f);
		yield return new WaitForSeconds(2f);
	}

	public Obstacle GetTarget() {
		throw new System.NotImplementedException();
	}

	public void UseSkill(object data) {

	}
}
