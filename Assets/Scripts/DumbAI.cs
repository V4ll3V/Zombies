using UnityEngine;
using System.Collections;

public class DumbAI : MonoBehaviour {
	public float moveSpeed = 10.0f;	// how fast we move, meters per second
	Transform target;				// The position we are moving towards, usually the player
	Vector3 dirTarget;				// The direction from us to the target
	CharacterController cc;			// our character controller
	
	// Use this for initialization
	void Start () {
		// Find an object within the scene that has the player tag
		// - This object's transform becomes our target
		// - GameObject.FindGameObjectWithTag finds any object in the scene with the specified tag
		// - someGameObject.GetComponent<ComponentType>() returns the component of ComponentType
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!target) {return;} // bail if no target
		
		// Find the direction from us to the target position
		// directionToTarget = targetPos - ourPosition
		// (0, 0, 7) = (0, 0, 10) - (0, 0, 3);
		// dirTarget = target.position - transform.position;
		// OR:
		// Simply call .LookAt(targetPosition) to instantly rotate ourselves to face the target
		transform.LookAt(target.position);
		cc.Move(transform.forward * moveSpeed * Time.deltaTime);
	}
}
