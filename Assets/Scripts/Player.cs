using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {
	CharacterController cc;
	
	public KeyCode forward = KeyCode.RightArrow;
	public KeyCode backward = KeyCode.LeftArrow;
	public KeyCode jump = KeyCode.UpArrow;
	public KeyCode drop = KeyCode.DownArrow;
	public float moveSpeed = 4.0f; // meters per second
	public float jumpLength = 2.0f; // jump lifetime in seconds
	public float jumpForce = 13.0f;	// how much upwards force is applied initially (decays over lifetime)
	float jumpedAt = 0.0f;
	float curVelocity = 0.0f;
	
	// Use this for initialization
	void Start () {
		// This retrieves the CharacterController component from THIS object
		// cc is now functioning like a pointer...it IS the component
		cc = GetComponent<CharacterController>();
		jumpedAt = -jumpLength;
	}
	
	// Update is called once per frame
	void Update () {
		// Check for jump input FIRST
		// - Also checks if we're on the ground
		// - We HAVE to check isGrounded BEFORE calling Move()
		//	- Move() will invalidate isGrounded
		//	- We need to check last frame's data before that happened
		if (cc.isGrounded && Input.GetKeyDown(jump)) {
			jumpedAt = Time.time;
		}
		// Check for forward-movement input
		// - Move forward (screen-right) if any input
		if (Input.GetKey(forward)) {
			transform.forward = Vector3.right;
			// Time-scaled movement:
			// - Taking our movement amount (raw) and multiplying
			//	 it with the amount of time the last frame took
			// - Why???
			//	- We can't control our framerate
			//	- If we want to move a fixed amount of distance,
			//		we should really set that target as PER SECOND
			//		and NOT PER FRAME
			//	- Ex of frame-based movement:
			//	 - We move 1 meter per-frame:
			//		- @30FPS = 30 meters of movement per second
			//		- @60FPS = 60 meters of movement per second
			//		- @120FPS= 120 meters of movement per second
			//	- Ex of time-based movement:
			//	 - We move 1 meter per-second
			//	 - Accomplish by multiplying 1 and 1/framerate
			//		- @30FPS = (1m * 0.033) * 30frames = 1m/s
			//		- @60FPS = (1m * 0.016) * 60frames = 1m/s
			//		- @120FPS= (1m * 0.008) * 120frames= 1m/s
			//	- Time.deltaTime is computed for us, and represents
			//		1/framerate
			cc.Move(transform.forward*moveSpeed*Time.deltaTime);
		}
		// Check for backwards input
		// - Move backwards (screen-left) if so
		else if (Input.GetKey(backward)) {
			transform.forward = -Vector3.right;
			cc.Move(transform.forward*moveSpeed*Time.deltaTime);
		}
		
		// Simulate upwards velocity and gravity (downwards vel)
		//  to make the jump work
		// - Linear Interpolation: move a value from A to B over time
		// - Here we interpolate our upwards force from MAX to MIN
		//  - The time value (must be 0-1) is:
		//		- Time we've been jumping for / Max jump length
		if (Time.time - jumpedAt <= jumpLength) {
			float curForce = Mathf.Lerp(jumpForce, 0.0f, (Time.time-jumpedAt)/jumpLength);
			// Our current velocity (upwards) is now whatever Lerp gave us
			curVelocity = curForce;
		}
		// If we aren't grounded, apply gravity (downwards force)
		if (!cc.isGrounded) {
			curVelocity -= 9.8f;
		}
		// Clamp the current velocity so it's never:
		// - less than -9.8
		// - more than 100.0
		curVelocity = Mathf.Clamp(curVelocity, -9.8f, 100.0f);
		// As long as our current velocity is not zero,
		//  we should be moving up or down
		if (curVelocity != 0.0f) {
			cc.Move(transform.up*curVelocity*Time.deltaTime);
		}
	}
	
	// OnGUI:
	// - Will be called for us automatically when 
	//   the GUI system is updating
	void OnGUI() {
		GUI.Label(new Rect(10.0f, 10.0f, 256.0f, 32.0f), "Grounded: " + ((cc.isGrounded)?"Yes":"No"));
		GUI.Label(new Rect(10.0f, 44.0f, 256.0f, 32.0f), "Velocity: " + curVelocity);
	}
}
