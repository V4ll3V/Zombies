using UnityEngine;
using System.Collections;

public class SimpleCamMove : MonoBehaviour {	
	public float moveScale = 10.0f;
	// Update is called once per frame
	void Update () {
		// Check if the w,a,s,or d keys are pressed
		// if so, move camera (gameObject) accordingly
		if (Input.GetKey(KeyCode.W)) { // move forward
			// New Object: Transform
			// - specifies position, rotation, and scale for a game object
			//
			// Access: gameObject.transform OR just transform (the script has one too)
			// Access: transform.position, transform.rotation, transform.scale
			// CAN'T: gameObject.transform.position.x += 5;
			// CAN: gameObject.transform.position = posVariable; 
			Vector3 newPos = gameObject.transform.position;
			// transform.forward is a Vector3 that stores the facing direction of an 
			//	object
			newPos += gameObject.transform.forward * moveScale * Time.deltaTime;
			gameObject.transform.position = newPos;
		}
		
		if (Input.GetKey(KeyCode.S)) { // move backward
			// The short way of doing the code in the W block
			gameObject.transform.position -= gameObject.transform.forward * moveScale * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.A)) {	// strafe left
			// transform.right is a Vector3 that stores the X axis of the object
			gameObject.transform.position -= gameObject.transform.right * moveScale * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.D)) {	// strafe right
			gameObject.transform.position += gameObject.transform.right * moveScale * Time.deltaTime;
		}
	}
}
