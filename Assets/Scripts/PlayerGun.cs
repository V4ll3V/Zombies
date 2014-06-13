using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {
	public KeyCode key_Fire = KeyCode.LeftShift;
	public GameObject bulletPrefab;
	public float launchForce = 1000.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key_Fire) || Input.GetMouseButtonDown(0)) {
			GameObject newBullet = (GameObject)Instantiate(bulletPrefab, transform.position+transform.forward*1.1f, transform.localRotation);
			newBullet.rigidbody.AddForce(transform.forward * launchForce);
		}
	}
}
