using UnityEngine;
using System.Collections;

public class attachToMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		other.transform.parent = transform;
	}
	
	void OnTriggerExit(Collider other) {
		other.transform.parent = null;
	}
}
