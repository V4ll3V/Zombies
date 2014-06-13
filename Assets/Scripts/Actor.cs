using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	public float health = 100.0f;
	
	void Start() {
		Debug.Log("actor started");
	}
	
	// This gets called automatically by the engine when an object containing a rigidbody
	//  hits our collider (as long as the collider is not flagged as a trigger)
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Projectile") {
			Debug.Log("actor was hit");
			// TODO: replace damage value with variable, probably within the projectile somewhere
			//		 Placing the damage value within the projectile would allow greater customization/projectile variations
			health -= 20.0f;
			
			// Simple kill:
			if (health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
