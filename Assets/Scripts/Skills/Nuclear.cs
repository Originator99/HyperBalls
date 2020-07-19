using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Nuclear : MonoBehaviour, ISkill {
	public GameObject bomb, skull, ripple; 
	public ParticleSystem explosion;
	public AudioSource explosionSound;

	private Vector3 rippleOriginalScale, skullOriginalScale;

	private void Awake() {
		rippleOriginalScale = ripple.transform.localScale;
		skullOriginalScale = skull.transform.localScale;
	}

	private void Start() {
		bomb.SetActive(false);
		skull.SetActive(false);
		ripple.SetActive(false);
		explosion.gameObject.SetActive(false);

		skull.GetComponent<SpriteRenderer>().color = Helper.HexToColor("FFFFFF00");
	}

	private IEnumerator DoSequence(Vector3 end_position) {
		yield return new WaitForSeconds(0.25f);
		skull.transform.position = end_position;
		bomb.transform.position = new Vector3(end_position.x, end_position.y + 20);
		bomb.SetActive(true);
		bomb.transform.DOMoveY(0, 2f).OnComplete(delegate() {
			bomb.SetActive(false);
			explosion.gameObject.SetActive(true);
			explosion.Play();
			explosionSound.Play();
		});
		yield return new WaitForSeconds(2f);

		skull.gameObject.SetActive(true);
		ripple.gameObject.SetActive(true);
		skull.transform.localScale = new Vector3(0, 0, 0);
		ripple.transform.localScale = new Vector3(0, 0, 0);
		skull.transform.DOScale(skullOriginalScale, 1f);
		skull.GetComponent<SpriteRenderer>().DOFade(1, 1f).OnComplete(delegate () {
			skull.transform.DOShakePosition(3.5f, 0.1f);
			ripple.transform.DOScale(rippleOriginalScale, 2.5f);
		});

		yield return new WaitForSeconds(3.5f);

		Collider2D[] objects = Physics2D.OverlapCircleAll(skull.transform.position, 5f);
		foreach (var collision in objects) {
			Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
			if (obs != null && obs.gameObject.activeSelf) {
				if (obs.TypeOfObstacle == LevelObjectType.GOOD_BLOCK) {
					obs.OnPlayerHit(out LevelObjectType type);
				} else {
					obs.DoDestroyEffect();
				}
			}
		}
		StartCoroutine(DoExitSequence());
	}

	private IEnumerator DoExitSequence() {
		yield return new WaitForSeconds(1f);
		skull.GetComponent<SpriteRenderer>().DOFade(0, 1f);
		ripple.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(delegate () {
			gameObject.SetActive(false);
			Destroy(gameObject, 3f);
		});
	}

	public Obstacle GetTarget() {
		throw new System.NotImplementedException();
	}

	public void UseSkill(object data) {
		gameObject.SetActive(true);
		if (data != null && data.GetType() == typeof(Vector3)) {
			StartCoroutine(DoSequence((Vector3)data));
		} else {
			Debug.LogError("Start position for Nuclear is null or has a different type ");
		}
	}
}
