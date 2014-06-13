using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	// Window size
	public float menuWidth = 500.0f;
	public float menuHeight = 500.0f;
	// Window and button dimensions
	Rect area_window;
	Rect area_bgBox;	// A rectangle to draw a menu background in
	Rect button_back;
	Rect button_quit;
	
	bool isPaused = false;
	
	void Start() {
		// Tell the engine to NOT DESTROY us when a new level is loaded
		// DontDestroyOnLoad(object to keep alive);
		// - gameObject is a pointer to the GameObject that owns this script!
		// - We MUST keep the object alive!
		//		- Keeping the script itself alive is not enough
		// - Use this carefully, with small/specific objects!
		DontDestroyOnLoad(gameObject);
		
		// Create rect dimensions
		area_window = new Rect( (Screen.width*0.5f) - (menuWidth*0.5f), (Screen.height*0.5f) - (menuHeight*0.5f), menuWidth, menuHeight);
		area_bgBox = new Rect(0.0f, 0.0f, menuWidth, menuHeight);
		button_back = new Rect(menuWidth * 0.1f, menuHeight * 0.1f, menuWidth * 0.8f, 50.0f);
		button_quit = new Rect(button_back.x, button_back.y + button_back.height + 10.0f, button_back.width, button_back.height);
	}
	
	
	void Update() {
		// If player hits escape, toggle the pause state
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = !isPaused; // set isPaused to the opposite of itself
		}
	}
	
	void OnGUI() {
		// If NOT paused, leave the function before drawing anything
		if (!isPaused) {return;}
		
		// Push a new group for our GUI window
		GUI.BeginGroup(area_window);
		
		// GUI.Box(rect, content);
		// - If you just want a background box, use 
		//		GUIContent.none as the content
		GUI.Box(area_bgBox, GUIContent.none);
		
		// Leave the paused state if pressed
		if (GUI.Button(button_back, "Resume")) {
			isPaused = false;
		}
		// Return to level 0 (main menu) if pressed
		if (GUI.Button(button_quit, "Quit")) {
			isPaused = false;
			Application.LoadLevel(0);
		}
		
		// Pop GUI window group
		GUI.EndGroup();
	}
}
