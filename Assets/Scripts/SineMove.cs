using UnityEngine;
using System.Collections;

// Moves the owning object back and forth using a sine wave
public class SineMove : MonoBehaviour {
	public Vector3 moveStrength = new Vector3(1.0f, 0.0f, 0.0f);
	public float moveSpeed = 2.0f;
	
	// So that we don't have to garbage collect it every frame...
	Vector3 newPos;
	float timeSine;
	Vector3 ogPos;
	
	// Use this for initialization
	void Start () {
		// Enforce 0-1 range for move strength values
		moveStrength.x = Mathf.Clamp (moveStrength.x, 0.0f, 1.0f);
		moveStrength.y = Mathf.Clamp (moveStrength.y, 0.0f, 1.0f);
		moveStrength.z = Mathf.Clamp (moveStrength.z, 0.0f, 1.0f);
		// Bake the full move speed into moveStrength
		moveStrength *= moveSpeed;
		
		ogPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// move object based on:
		// sine value of current time
		// - scaled by moveStrength for each axis
		// - scaled by moveSpeed
		
		// Rule #1 of Programming, all programming, esp math, and stuff:
		// - Don't do something that doesn't need to be done
		newPos = transform.position;
		timeSine = Mathf.Sin (Time.time);
		if (moveStrength.x != 0.0f) {
			newPos.x = ogPos.x + timeSine * moveStrength.x;
		}
		if (moveStrength.y != 0.0f) {
			newPos.y = ogPos.y + timeSine * moveStrength.y;
		}
		if (moveStrength.z != 0.0f) {
			newPos.z = ogPos.z + timeSine * moveStrength.z;
		}
		transform.position = newPos;
	}
}










