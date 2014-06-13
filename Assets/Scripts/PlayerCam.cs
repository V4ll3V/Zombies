using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {
	public GameObject player;
	Vector3 offset = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (offset == Vector3.zero) {
			offset = transform.position - player.transform.position;
		}
		
		transform.position = player.transform.position + offset;
	}
}
