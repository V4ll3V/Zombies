using UnityEngine;
using System.Collections;

// Functions
// What is a function?
// - A way to reuse code
//	- they are modules, containers, prefabs, etc.
// Why would we use functions?
// - Makes us more efficient
// - Easier to fix bugs later
// - Helps us create less bugs to begin with
//	- The less code we write, the less bugs we have
//
// How does a function work?
// - We define a function, and add executable code to it
// - Then we call that function to invoke/execute that block
// - The function and the code calling it do NOT have to be
//	 one after the other (top down order)
//
// What are the core components of a function?
// - return value: a value given back to the caller of the function
// - function name: the common name we use to call the function
// - parameters: data required for the function to operate correctly
// - function body: the actual block of code that executes when this function is called
// - signature: return value + name + parameters
//
// Syntax:
// returnDatatype functionName(parameters) {
//		function body
// }
//
// returnDatatype specifies the data type of the value you will return/pass back
// - This can be any data type, even custom types
// - IF we DON'T want to return a value, we use void as the data type
// - IF we DO want to return a value:
//	- The function MUST return a value if it has a non-void return type
//	- The value/variable MUST have the same data type as the return type
//
// functionName gives a function an identity within the system
// - When we call any function, we call it by name
// - The name is arbitrary, but it should be descriptive
//	- We have to call it by name, so it should be something we can remember and makes sense to us
// - Even though the name can be anything, calling it requires the EXACT NAME (case sensitive)
//
// parameters are a way to give a function custom data needed to operate
// - We can have any number of params, including none
// - Params can be any data type
// - Separate parameters by a comma

public class SimpleMove : MonoBehaviour {
	// range variables
	// public variables:
	// - In Unity, a public variable exposes that value to the editor
	// - To make a variable public, insert the "public" keyword in FRONT OF
	//	 the variable declaration
	public float rangeX = 4.0f;
	public float rangeZ = 4.0f;
	// movement variables
	// these are private, which is the default for variables
	float moveX = 0.0f;	// how much x movement to do this frame
	float moveZ = 0.0f; // how much z movement to do this frame
	
	// Update is called once per frame
	void Update () {
		// Check for input
		//
		// Calling a function:
		// Syntax
		// functionName(any parameters needed);
		//
		// Catching return values:
		// - You can assign the return value of a function
		//	 to variable of the same data type
		bool wasInput = CheckInput();
		
		// If there's input, move
		// Conditional shorthand:
		// - If a variable value is true/false (aka a bool),
		//	 you don't need the == true or == false in the if
		//	 statement
		if (wasInput) {
			Move (moveX, moveZ);
		}
	}
	
	// Polls keyboard input and return true if there is any
	// - returns false if there is no input
	bool CheckInput() {
		// reset move variables to 0
		moveX = 0.0f;
		moveZ = 0.0f;
		// make our return value
		bool retValue = false;
		
		// check forward/backward movement
		// A function with a return value equates to that value
		// - therefore we can use the function call as if it were
		// 	 a value or variable
		if (Input.GetKeyDown (KeyCode.W)) {
			moveZ += 1.0f;
			retValue = true;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			moveZ -= 1.0f;
			retValue = true;
		}
		// check left/right strafe movement
		if (Input.GetKeyDown (KeyCode.A)) {
			moveX -= 1.0f;
			retValue = true;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			moveX += 1.0f;
			retValue = true;
		}
		
		// returning a value from a function
		// return valueOfCorrectDatatype;
		return retValue;
	}
	
	// Moves the player based on current move values
	void Move(float x, float z) {
		// update our transform's position based on x and z values
		// - All game objects have a transform
		// - The transform has a position
		// - The position has an x, y, and z value
		//	- x, y, and z are all floats
		//
		// If we want to modify the transform on the game object
		//	we're attached to:
		// The long way:
		// gameObject.transform.position
		// The short way:
		// transform.position
		// 
		// position values:
		// position.x, position.y, position.z
		// BUT...
		// We can't modify transform's position values directly!!!
		// So...
		// Vector3 pos = transform.position;
		// pos.x = whatever
		Vector3 pos = transform.position;
		pos.x += x;
		pos.z += z;
		
		// Clamp the position values within our set range
		if (pos.x > rangeX) { // we've gone higher than our range
			pos.x = rangeX;
		}
		else if (pos.x < -rangeX) {
			pos.x = -rangeX;
		}
		
		if (pos.z > rangeZ) {
			pos.z = rangeZ;
		}
		else if (pos.z < -rangeZ) {
			pos.z = -rangeZ;
		}
		
		// however, we can change transform.position by setting it
		//	a different Vector3 variable, and not modifying .x, .y, and .z direclty
		transform.position = pos;
	}
}











