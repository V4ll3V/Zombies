using UnityEngine;
using System.Collections;

public class physicsActivator : MonoBehaviour {
	public Rigidbody[] targets;
	
	void OnTriggerEnter(Collider other) {
		//if (other.gameObject == gameObject) {return;}
		foreach (Rigidbody t in targets) {
			t.isKinematic = false;
			t.WakeUp();
		}
	}
}
