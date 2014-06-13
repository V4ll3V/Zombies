using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {
	// How much rotation to apply to each axis
	// 1.0f = MAX
	public Vector3 amount = new Vector3(0.0f, 1.0f, 0.0f);
	// How much rotation per second
	public float speed = 15.0f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(amount * (speed*Time.deltaTime));
	}
}
