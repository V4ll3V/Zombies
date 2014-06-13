using UnityEngine;
using System.Collections;

public class rotatorPivot : MonoBehaviour {
	public Vector3 rotationAxis;
	public float rotation;
	public Transform pivot;
	
	// Update is called once per frame
	void Update () {
		if (pivot) {
			transform.RotateAround(pivot.position, rotationAxis, rotation*Time.deltaTime);
		}
		else {
			transform.Rotate(rotationAxis, rotation*Time.deltaTime);
		}
	}
}
