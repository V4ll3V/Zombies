using UnityEngine;
using System.Collections;

public class ExplosiveTrigger : MonoBehaviour {
	public Explosive bomb; // link to the bomb we trigger
	
	// New Function:
	// OnTriggerEnter()
	// - If this script is attached to a game object that has a trigger,
	//	 this function will get called every time something enters the trigger
	void OnTriggerEnter() {
		// something has entered the trigger
		// make the bomb explode
		bomb.Explode();
		// single-use, destroy ourselves
		Destroy(gameObject);
	}
}
