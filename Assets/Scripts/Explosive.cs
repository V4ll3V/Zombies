using UnityEngine;
using System.Collections;

public class Explosive : MonoBehaviour {
	public float force = 2000.0f;	// required by unity explosion method
	public float radius = 100.0f;	// required by unity explosion method
	public bool changeGravity = false;	// allow the exploded objects to lose gravity
	
	// The explode function triggers the explosion
	public void Explode() {
		// Gather all ridigbodies within radius
		// 1) make an array of colliders
		// 2) go through all the colliders, apply explosion force to them
		
		// syntax: Type[] name; makes an array of <Type>
		// New Function:
		// - Physics.OverlapSphere(position, radius)
		// - returns an array of colliders within the sphere
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		
		// foreach loop:
		// - a simple for loop type that can be used to quickly loop through
		//	 all the elements in an array
		// Syntax:
		// foreach (Type variableName in Array)
		// {
		// }
		foreach (Collider c in colliders)
		{
			// check if this is a rigidbody or static mesh
			//  if static mesh: ignore
			// New Command:
			// continue;
			// - Makes a loop move on to the next iteration
			// - code below the continue command gets ignored
			// 
			// the "!" means NOT
			// the line below means "true if the rigidbody is false"
			if (!c.rigidbody) {
				continue;	// ignore and skip
			}
			
			// changeGravity and useGravity mean opposite things
			// we assign useGravity the opposite value of changeGravity to
			//	account for that
			c.rigidbody.useGravity = !changeGravity;
			
			// apply the actual explosion force to the ridigbody
			// New Function:
			// rigidbody.AddExplosionForce(force, position, radius);
			// - apply to a rigidbody to simulate explosion
			// - position and radius are from the explosive itself
			c.rigidbody.AddExplosionForce(force, transform.position, radius);
		}
		
		// Remove ourselves from the scene
		Destroy(gameObject);
	}
}
