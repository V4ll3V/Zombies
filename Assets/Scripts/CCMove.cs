using UnityEngine;
using System.Collections;

public class CCMove : MonoBehaviour {
	CharacterController cc;
	public float moveSpeed = 10.0f;
	
	// Vertical movement variables
	public float maxVelocity = 20.0f; // The fastest we can be moving upwards
	public float minVelocity = -20.0f;// The fastest we can be falling
	float curVelocity = 0.0f;		  // Our current vertical speed
	public float CurVelocity { // Property to make this public to other classes
		get {return curVelocity;}
		set {curVelocity=value; curVelocity=Mathf.Clamp (curVelocity,minVelocity,maxVelocity);}
	}
	
	// Use this for initialization
	void Start () {
		cc = GetComponentInChildren<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			cc.Move(transform.forward*moveSpeed*Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			cc.Move (transform.forward*-moveSpeed*Time.deltaTime);
		}
		
		if (Input.GetKey (KeyCode.A)) {
			cc.Move (transform.right*-moveSpeed*Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			cc.Move (transform.right*moveSpeed*Time.deltaTime);
		}
		
		// gravity and upwards velocity
		curVelocity -= 9.8f * Time.deltaTime;
		curVelocity = Mathf.Clamp (curVelocity, minVelocity, maxVelocity);
		cc.Move (transform.up*curVelocity*Time.deltaTime);
	}
}
