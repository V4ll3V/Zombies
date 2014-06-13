using UnityEngine;
using System.Collections;

// Implements a simple lifespan
// After said lifespan, the object we're attached to dies (it gets deleted)
public class killTimer : MonoBehaviour {
	public float lifespan = 7.0f;
	// Use this for initialization
	void Start () {
		// Unity Function: Destroy(gameObject)
		//		Removes the GameObject given immediately
		// Another version: Destroy(gameObject, float "when")
		//		Removes the GameObject given in "when" seconds
		Destroy (gameObject, lifespan);
	}
}
