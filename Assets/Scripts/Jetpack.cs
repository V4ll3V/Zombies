using UnityEngine;
using System.Collections;

// Implements basic jetpack behaviors for up/down movement
public class Jetpack : MonoBehaviour {
	public float boostForceMax = 20.0f;
	public float boostBatteryMax = 100.0f;
	public float boostForceIncrease = 1.5f; // per second
	public float boostForceDecay = 2.0f; // per second
	public float boostBatteryDrain = 20.0f; // per second
	public float boostBatteryRecharge = 10.0f; // per second
	public KeyCode boostKey = KeyCode.Space;
	
	CCMove motor;
	
	float boostForce = 0.0f;
	float boostBattery;
	
	// Debug GUI shit
	Rect panelGroupRect = new Rect(10.0f, 10.0f, 256.0f, 256.0f);
	Rect bgBoxRect = new Rect(0.0f, 0.0f, 256.0f, 256.0f);
	Rect boostForceRect = new Rect(10.0f, 10.0f, 256.0f, 20.0f);
	Rect boostBatteryRect = new Rect(10.0f, 34.0f, 256.0f, 20.0f);
	Rect actorVelRect = new Rect(10.0f, 58.0f, 256.0f, 20.0f);
	//
	
	// Use this for initialization
	void Start () {
		boostBattery = boostBatteryMax;
		motor = GetComponent<CCMove>();
	}
	
	// Update is called once per frame
	void Update () {
		SetBoostForce();
		if (boostForce > 0.0f)
			motor.CurVelocity += boostForce;
	}
	
	void SetBoostForce() {
		if (Input.GetKey(boostKey) && boostBattery > 0.0f) {
			boostBattery -= boostBatteryDrain * Time.deltaTime;
			boostForce += boostForceIncrease * Time.deltaTime;
		}
		else {
			boostBattery += boostBatteryRecharge * Time.deltaTime;
			boostForce -= boostForceDecay * Time.deltaTime;
		}
		
		boostBattery = Mathf.Clamp(boostBattery, 0.0f, boostBatteryMax);
		boostForce = Mathf.Clamp(boostForce, 0.0f, boostForceMax);
	}
	
	void OnGUI() {
		GUI.BeginGroup(panelGroupRect);
		GUI.Box(bgBoxRect, GUIContent.none);
		GUI.Label(boostForceRect, "Boost Force: " + boostForce);
		GUI.Label(boostBatteryRect, "Boost Batt: " + boostBattery);
		GUI.Label(actorVelRect, "Velocity: " + motor.CurVelocity);
		GUI.EndGroup();
	}
}
