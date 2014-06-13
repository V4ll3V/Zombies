// LOOPS
// What is the purpose of a loop?
// - *To perform repetitive tasks*
// - Loops repeat a "task" or a block of code for a specified duration
// 	- Duration is aka number of iterations
// - 1 iteration = 1 execution of the loop code
// - If you need to something more than once in a row, use a loop!
//
// ObjectEmitter:
// This script will emit objects from our left, right, and above
// - Each of these 3 options will be buond to a mouse button press
// 	- Left click emits objects from the left
//	- Right click emits objects from the right
//	- Middle click emits objects from above
//
// We'll need some variables and functions
// - GameObject for left anchor, right anchor, top anchor
// - GameObject for the prefab of the emitted object
// - float xRange, yRange: determine the bounds/area that objects spawn in
// - int emission count: how many objects to spawn per trigger/event
// - A function to handle input, check for mouse presses
// - A function to handle spawning a new object at some location
// - 3 functions to handle spawning from from each respective anchor
using UnityEngine;
using System.Collections;

public class ObjectEmitter : MonoBehaviour {
	// emitted/spawned object
	public GameObject emitObject;
	// anchor points
	public GameObject leftAnchor;
	public GameObject rightAnchor;
	public GameObject topAnchor;
	// numerical values for object count and placement
	public float xRange = 10.0f;
	public float yRange = 10.0f;
	public int emitCount = 10;
	
	// Update is called once per frame
	// Will call CheckInput()
	void Update () {
		CheckInput ();
	}
	
	// Check for mouse presses
	// Will call the appropriate Emit__() based on which button is pressed
	void CheckInput() {
		// Check left button
		if (Input.GetMouseButtonDown (0)) {
			EmitLeft (emitCount);
		}
		// Check right button
		if (Input.GetMouseButtonDown (1)) {
			EmitRight (emitCount);
		}
		// Check middle button
		if (Input.GetMouseButtonDown(2)) {
			EmitTop (emitCount);
		}
	}
	
	// Spawns a single object, based on anchor position and ranges
	void EmitObject(GameObject anchor) {
		// make a Vector3 to store a random position in
		Vector3 randPos = anchor.transform.position;
		
		// randomize the position based on range and anchor point
		// New Function: int Random.Range(int min, int max)
		//			   float Random.Range(float min, float max)
		// This code below will give us position values within
		//	(-range, range), offset from the anchor's position
		//
		// Dot Syntax:
		// - Allows us to access variables that belong to another variable
		// - Used for complex data types (data types that store multiple values)
		// - The '.' is called the "dot operator"
		// Syntax:
		// variableName.subVariableName etc
		randPos.x = Random.Range (randPos.x-xRange, randPos.x+xRange);
		randPos.y = Random.Range (randPos.y-yRange, randPos.y+yRange);
		
		// spawn the prefab clone at that position
		// New Function: Instantiate
		// - Creates a new game object using a template/prefab
		//	- This new object is a clone, aka INSTANCE of the prefab
		//	- This clone is an EXACT duplicate of the prefab
		//	- BUT, it is NOT the prefab!
		// - Needs to know position and rotation for the new clone
		Instantiate (emitObject, randPos, anchor.transform.localRotation);
	}
	
	// Loop for count and emit objects from left anchor
	void EmitLeft(int count) {
		// WHILE loop
		// - A basic conditional loop
		// - Keeps going as long as its condition is true
		//
		// Steps:
		//	- checks condition
		//	- if true, executes the code block
		// 		- then go back to step one and recheck the condition
		//	- else, moves on
		//
		// Syntax:
		// while (condition) {
		//		code to execute for each loop iteration
		// }
		//
		// Infinite Loops:
		// - a loop where the condition never becomes false
		// - WE are responsible for making sure that doesn't happen
		// - An infinite loop will cause the app to stall and appear to freeze
		while (count > 0) {
			Debug.Log ("Inside while, count: " + count);
			EmitObject (leftAnchor);
			// Prevent an infinite loop by subtracting from count 
			//	during each iteration
			count--;
		}
	}
	
	// Loop for count and emit objects from right anchor
	void EmitRight(int count) {
		// DO WHILE loop
		// - conditional loop, like while
		// - Unlike the while loop, do while guarantees at least 1 iteration
		// - It does this by checking the condition AFTER iterating
		//
		// Syntax:
		// do {
		//		code to execute each iteration
		// } while (condition);
		do {
			Debug.Log ("Inside do-while, count: " + count);
			EmitObject (rightAnchor);
			count--;
		} while (count > 0);
	}
	
	// Loop for count and emit objects from top anchor
	void EmitTop(int count) {
		// FOR loop
		// - A conditional loop type that builds in the variable update step
		// - Slightly cleaner looking vs. the while loops
		// - Slightly less code overall vs. the while loops
		// - Still need to be careful to not create an infinite loop!
		//
		// Syntax:
		// for (dataType loopVar = initValue; condition; loopVar mods) {
		//		code to execute each iteration
		// }
		for (int i = 0; i < count; i++) {
			Debug.Log ("for loop iteration: " + i);
			EmitObject (topAnchor);
		}
	}
}











