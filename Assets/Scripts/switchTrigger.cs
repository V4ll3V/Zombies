using UnityEngine;
using System.Collections;

public class switchTrigger : MonoBehaviour {
	public GameObject target;
	
	void OnTriggerEnter(Collider other) {
		target.SetActive(true);
	}
}
