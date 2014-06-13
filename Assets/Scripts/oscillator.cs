using UnityEngine;
using System.Collections;

public class oscillator : MonoBehaviour {
	public Vector3 moveStrength = new Vector3 (0.0f, 1.0f, 0.0f);
	public float speed = 1.0f; // cycles per second
	Vector3 originalPosition = Vector3.zero;
	Vector3 newPosition = Vector3.zero;
	float lastCycleTime = 0.0f;
	float lastSine = 0.0f;
	
	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		if (moveStrength.x == 0.0f && moveStrength.y == 0.0f && moveStrength.z == 0.0f) {
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		lastCycleTime += speed * Time.deltaTime;
		newPosition = transform.position;
		lastSine = Mathf.Sin(lastCycleTime);
		if (moveStrength.x != 0.0f) {
			newPosition.x = originalPosition.x + (lastSine*moveStrength.x);
		}
		if (moveStrength.y != 0.0f) {
			newPosition.y = originalPosition.y + (lastSine*moveStrength.y);
		}
		if (moveStrength.z != 0.0f) {
			newPosition.z = originalPosition.z + (lastSine*moveStrength.z);
		}
		transform.position = newPosition;
	}
}
