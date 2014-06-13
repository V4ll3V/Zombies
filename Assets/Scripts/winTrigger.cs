using UnityEngine;
using System.Collections;

public class winTrigger : MonoBehaviour {
	bool winning = false;
	
	void OnTriggerEnter(Collider other) {
		winning = true;
	}
	
	void OnTriggerExit(Collider other) {
		winning = false;
	}
	
	void OnGUI() {
		if (winning) {
			GUI.Label(new Rect(Screen.width*0.5f, Screen.height*0.5f, 128.0f, 64.0f), "You Win!");
		}
	}
}
