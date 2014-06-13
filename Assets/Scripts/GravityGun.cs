using UnityEngine;
using System.Collections;

// When mouse is pressed (SHOOT)
// - cast a ray into the scene
// - if ray hits a ridigbody, "grab" it
// If we have grabbed an object
// - keep it floating in front of us
// If we have grabbed an object and then release the mouse button
// - launch object with some specified force

public class GravityGun : MonoBehaviour {
	public float launchForce = 300.0f; // how much force to shoot object out with
	Rigidbody ammo;	// the object we've grabbed
	bool ready = false;		// true if we have grabbed an object and not yet shot it
	
	// Update is called once per frame
	void Update () {
		Vector3 gunPos = transform.position + (transform.forward * 3.2f);
		//gunPos.y += 1.0f; // modify this offset to suit preferences/needs
		
		if (ready == false) { // ready to pick up an object
			if (Input.GetMouseButton(0)) {
				// Raycasting
				// Shoot a ray (a line with a fixed length) into the scene
				// If the ray hits anything with a collider, it will be returned to us
				// - If the object we hit has a rigidbody, we will "pick it up"
				// New Function: Physic.Raycast()
				RaycastHit hitObject;
				if (Physics.Raycast(gunPos, transform.forward, out hitObject) && hitObject.rigidbody) {
					Debug.Log("Gravity Gun caught: " + hitObject.rigidbody.gameObject.name);
					ammo = hitObject.rigidbody;
					ready = true;
				}
				// Extra raycast notes:
				// Using layerMask
				// EX: let's say that we want to only catch things on the chunkerStatic layer
				//	   - chunkerStatic is layer 8 (9th layer, starting from 0)
				//     - 8 itself is a bit value (4th bit, aka 4th layer)
				//	   - We need to create bitmask that makes the correct binary value for the 9th layer
				//     - 1<<8
				//		- reads as bit 1, shifted 8 bits to the left
				//		- bit 1: 			00000001
				//		- bit 1 shifted: 	10000000
				//   Then, the statement becomes:
				// Physics.Raycast(origin, direction, out hit, distance, 1<<8);
			}
		}
		else if (Input.GetMouseButtonUp(0)) { // shoot the object back out into the scene
			// Shoot the object forward, a-la first person shooter controls
			// Use the launch force to affect how far/fast the object shoots
			ammo.AddForce(transform.forward*launchForce);
			ammo = null;
			ready = false;
		}
		else { // we have an object, but haven't shot it yet...make it float out in front of us
			ammo.transform.position = gunPos;
			ammo.rigidbody.velocity = Vector3.zero;
		}
	}
}










