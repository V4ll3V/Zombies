using UnityEngine;
using System.Collections;

public class ObjectCycler : MonoBehaviour {
	// data:
	// - an array of GameObject's to store the prefabs we cycle through
	// - a single GameObject that "points" to the currently-spawned object
	public GameObject[] objects; // our prefabs to clone
	GameObject curObject;		 // the currently-active clone
	int curIndex = 0; // which index in the array was spawned last
	
	// Use this for initialization
	void Start () {
		CycleObject ();
	}
	
	// Update is called once per frame
	void Update () {
		// If CheckInput returns true, call CycleObject
		// else do nothing about it
		if (CheckInput ()) {
			CycleObject ();
		}
	}
	
	bool CheckInput() {
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			curIndex++;
			if (curIndex >= objects.Length) {
				// We've gone past the last element,
				// reset to the first element
				curIndex = 0;
			}
			return true;
		}
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			curIndex--;
			if (curIndex < 0) {
				// We've gone past the first element, 
				// reset to the last element
				curIndex = objects.Length - 1;
			}
			return true;
		}
		
		return false;
	}
	
	void CycleObject() {
		// Destroy the current object
		// - ONLY do this if the current object exists!
		// How can we tell if curObject exists?
		// i.e. how can we tell if curObject is valid?
		// IF curObject is NOT valid (which happens at Start):
		// - the value of curObject will be null
		//
		// null:
		// - a value that equates to zero
		// - used to represent a useable value for an invalid variable/object
		if (curObject != null) {
			// if curObject does not equal null, it is safe
			//  to assume that it is valid
			//
			// Function: Destroy(object);
			// - Destroys/unloads "object" and removes it from the scene
			// Catches:
			// - It will destroy the ENTIRE hierarchy of "object"
			//	- that means all attached components are destroyed
			//  - all child objects are destroyed
			// - If you delete a prefab (non-clone, non-scene object)...
			//  - It WILL REMOVE THE PREFAB FROM THE PROJECT!!!
			//  - EX: our objects array stores prefabs from the project folder
			//  	- IF we call Destroy on something in that array, 
			//			the prefab will literally be deleted from the project and off your disk
			Destroy (curObject);
		}
		
		// Spawn a new clone of the prefab at curIndex in the array
		// Function: Instantiate
		// - Creates a new clone of the given original object
		// - Can optionally position and rotate the clone
		// - returns the object that was just made
		//
		// Casting variables:
		// - Makes the system treat a variable as <X> datatype when it is really <Y> dataType
		curObject = (GameObject)Instantiate (objects[curIndex], transform.position, transform.localRotation);
	}
}









