using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	public GameObject door;	// the door object we "open"
	public float openSpeed = 2.0f;	// how fast the door moves while opening
	public float openTime = 2.0f;	// how long it takes to open the door
	float openedAt;			// The time at which we hit the trigger
	bool doorOpen = false;	// has the door already been/started opening?
	
	// Update is called once per frame
	void Update () {
		// Should the door be moving/opening?
		// Complex conditional statements:
		// - conditional statements can be combined within a single check
		// - We do this using the LOGICAL operators
		//	- && = LOGICAL AND
		//	- || = LOGICAL OR
		// - Truth Table
		//	- true  && false = false
		//	- true  && true  = true
		//	- false && false = false
		//	- true  || false = true
		//	- true  || true  = true
		//	- false || false = false
		if (doorOpen && (Time.time-openedAt <= openTime)) {
			// move the door
			door.transform.position += door.transform.right * openSpeed * Time.deltaTime;
		}
	}
	
	// OnTriggerEnter gets called for us by the physics system IF:
	// - An object with a collider AND rigidbody steps into the trigger attached
	//		to the object we are attached to
	void OnTriggerEnter(Collider other) {
		// If the door is already open, leave this function before the logic happens again
		if (doorOpen) {return;}
		
		// Flag the properties that let Update() detect that the door should move
		doorOpen = true;
		openedAt = Time.time;
	}
}
