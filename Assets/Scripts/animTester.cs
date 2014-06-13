using UnityEngine;
using System.Collections;

public class animTester : MonoBehaviour {
	public KeyCode next = KeyCode.RightArrow;
	public KeyCode prev = KeyCode.LeftArrow;
	public KeyCode speedUp = KeyCode.UpArrow;
	public KeyCode slowDown = KeyCode.DownArrow;
	public float blendTime = 0.3f;
	int curAnim = 0;
	float speedStep = 0.05f;
	float curSpeed = 1.0f;
	ArrayList states = new ArrayList();
	
	Rect infoGroup = new Rect(10.0f, 10.0f, 256.0f, 256.0f);
	Rect curAnimArea = new Rect(0.0f, 0.0f, 256.0f, 24.0f);
	Rect totalAnimArea = new Rect(0.0f, 26.0f, 256.0f, 24.0f);
	Rect speedArea = new Rect(0.0f, 52.0f, 256.0f, 24.0f);
	Rect blendArea = new Rect(0.0f, 78.0f, 256.0f, 24.0f);
	
	// Use this for initialization
	void Start () {
		if (!animation) {
			Debug.Log("This character has no animation component! You can't test animations without animations...");
			return;
		}
		
		foreach (AnimationState s in animation) {
			states.Add(s.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// cycle through animation states
		if (Input.GetKeyDown(next)) {
			curAnim++;
			if (curAnim >= animation.GetClipCount()) {
				curAnim = 0;
			}
			FadeToCurrent();
		}
		if (Input.GetKeyDown(prev)) {
			curAnim--;
			if (curAnim < 0) {
				curAnim = animation.GetClipCount()-1;
			}
			FadeToCurrent();
		}
		
		// change the speed of the animation states
		if (Input.GetKeyDown(speedUp)) {
			AlterSpeedBy(speedStep);
		}
		if (Input.GetKeyDown(slowDown)) {
			AlterSpeedBy(-speedStep);
		}
	}
	
	void FadeToCurrent() {
		animation.CrossFade((string)states[curAnim], blendTime);
	}
	
	void AlterSpeedBy(float change) {
		curSpeed += change;
		foreach (AnimationState s in animation) {
			s.speed = curSpeed;
		}
	}
	
	void OnGUI() {
		GUI.BeginGroup(infoGroup);
		
		GUI.Label(curAnimArea, "Current Animation: " + states[curAnim]);
		GUI.Label(totalAnimArea, "Total Animations: " + states.Count);
		GUI.Label(speedArea, "Animation Speed: " + curSpeed);
		GUI.Label(blendArea, "Blend Time: " + blendTime);
		
		GUI.EndGroup();
	}
}
