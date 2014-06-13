using UnityEngine;
using System.Collections;

public class elevator : MonoBehaviour {
	public float travelTime = 2.0f; // in seconds
	public float idleTime = 2.0f; // in seconds
	public Transform top;
	public Transform bottom;
	Transform src;
	Transform dest;
	float movedAt = 0.0f;
	bool moving = false;
	
	// Use this for initialization
	void Start () {
		//movedAt = -travelTime;
		transform.position = bottom.position;
		dest = top;
		src = bottom;
		moving = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			UpdateMove();
		}
		else if (Time.time-movedAt > travelTime+idleTime) {
			movedAt = Time.time;
			moving = true;
			Transform temp = src;
			src = dest;
			dest = temp;
		}
	}
	
	void UpdateMove() {
		transform.position = Vector3.Lerp(src.position, dest.position, (Time.time-movedAt)/travelTime);
		
		if (Time.time-movedAt > travelTime) {
			moving = false;
		}
	}
}
