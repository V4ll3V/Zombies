using UnityEngine;
using System.Collections;

// Shoots a projectile when the player presses the left mouse button
public class shoot : MonoBehaviour {
	public GameObject projectile;	// The PREFAB we're using as a projectile
	public float force = 1000.0f;	// The force to apply to the projectile's rigidbody
	
	// Update is called once per frame
	void Update () {
		// Check if the left mouse button was hit THIS FRAME
		// IF statements: used to check if something true/false
		// Syntax: if (condition) { do this if condition is TRUE }
		// Unity Function: Input.GetMouseButton(int buttonID)      -> true if the button is held down
		//				   Input.GetMouseButtonDown(int buttonID)  -> true ONLY on frame button is clicked
		//				   Input.GetMouseButtonUp(int buttonID)    -> true ONLY on frame button is released
		//		These functions ALL return (give back) a true/false value
		if (Input.GetMouseButtonDown (0)) { // "If the left mouse button was clicked this frame"
			// Create a new projectile instance
			// What is an instance??
			// - A copy, or clone, of another GameObject
			// - Often, an instance is a clone of a PREFAB
			// How do we make an instance of a prefab, from a script?
			// As long as we a reference to the prefab, we can instantiate
			// Unity Function: Instantiate(original prefab, spawn position, spawn rotation)
			//		This function returns/gives back an OBJECT
			// To turn an Object into a GameObject: CAST the object with the syntax (GameObject)object
			//		This treats the Object as a GameObject
			GameObject newProj = (GameObject)Instantiate (projectile, transform.position, transform.localRotation);
			// Fire the new projectile
			// Unity Function: AddForce(force direction * force amount);
			//		This function belongs to RIGIDBODY, so we have to do someRigidbody.AddForce(value);
			newProj.rigidbody.AddForce (transform.forward * force);
		}
	}
}

