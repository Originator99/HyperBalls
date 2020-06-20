using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CollisionRipple : MonoBehaviour {
	public ParticleSystem effect;

	public void DoCollisionRipple(float ripple_radius = 5) {
		effect.startSize = ripple_radius;
		effect.Play();
	}

	private void OnParticleCollision(GameObject other) {
		Debug.Log("Collided with : " + other.name);
	}
}
