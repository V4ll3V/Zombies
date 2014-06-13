using UnityEngine;
using System.Collections;

public class loseTrigger : MonoBehaviour {
	public Transform respawnPoint;
	
	void OnTriggerEnter(Collider other) {
		if (!other.gameObject.rigidbody) {
			other.gameObject.transform.position = respawnPoint.position;
		}
	}
}
