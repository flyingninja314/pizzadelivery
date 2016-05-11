using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

		public int healthToGive;

		public AudioSource pickupSound;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<PlayerController> () == null) {
			return;
		}

		HealthManager.hurtPlayer (-healthToGive);

		pickupSound.Play ();

		Destroy (gameObject);
	}
}
