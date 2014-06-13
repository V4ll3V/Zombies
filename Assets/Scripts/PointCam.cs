using UnityEngine;
using System.Collections;

// PointCam
// Implements a camera that simply pivots to track a target
// - The camera does NOT translate
// - The camera tracks its target by attempting to look in the target's 
//		direction
public class PointCam : MonoBehaviour {
	public Transform target; // The transform of the target object
	public float trackingSpeed = 0.5f;	// 1.0 = fastest, 0.0 = stopped
	Vector3 eye; // a position in space directly in front of the cam
	float deadZone = 0.5f;	// how far the eye and target need to be apart in order to get the cam to pivot
	float dotAngle; // the dot product result, pos->eye DOT pos->target
	
	// Use this for initialization
	void Start () {
		// on the first frame, set the eye = target's position
		eye = target.position;
		
		// clamp the trackingSpeed to the right range
		trackingSpeed = Mathf.Clamp (trackingSpeed, 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		// update the eye position
		// - find the direction from eye to target
		// - check if the magnitude of this direction is > deadZone
		//	- if yes:
		//	- scale the direction by trackingSpeed
		//	- ADD the scaled direction to eye position
		//		- this OFFSETs the eye position
		Vector3 dirTarget = target.position - eye;
		// let's use the squared magnitude!!
		// - squared magnitude is MUCH faster to compute vs. the real magnitude
		float sqmag = dirTarget.sqrMagnitude;
		if (sqmag > (deadZone*deadZone)) {
			// time to move the eye
			// we want to move [trackingSpeed] percent along the direction
			Vector3 dirScaled = dirTarget * trackingSpeed;
			// offset our eye by adding the scaled direction to it
			eye += dirScaled;
		}
		
		// make the camera look at the eye
		Vector3 dirEye = eye - transform.position; // from camera to eye
		// normalize the direction and set it as our forward
		// Vector3 has a .normalized property
		transform.forward = dirEye.normalized;
		
		// find the dot product of pos->eye and pos->target
		Vector3 dirCamToTarget = target.position - transform.position;
		dotAngle = Vector3.Dot (dirEye.normalized, dirCamToTarget.normalized);
	}
	
	void OnGUI() {
		// The dot product result is the cosine of the angle between
		//	the two vectors that you dot'd
		// How do we take a dot result then, and make an angle out of it?
		// - By inversing the cosine/getting the ARCOSINE
		// - in Unity's math library, the function is:
		//		Mathf.acos(value);
		//
		// Problem:
		// - the acos function gives us back an angle in RADIANS
		// Solution:
		// - convert the radians value into degrees by:
		//		degrees = radians * 180/PI
		float radians = Mathf.Acos (dotAngle);
		float degrees = radians * (180.0f/Mathf.PI);
		
		// Now that we've done all that work, do something more useful:
		GUI.Label (new Rect(10.0f, 10.0f, 256.0f, 24.0f), "Angle to target: " + degrees);
	}
}





