using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	// The overall size of the main menu's window
	public float menuWidth = 500.0f;
	public float menuHeight = 500.0f;
	
	// DEBUG
	public string optionsDummyText = "This is just some text to use within a GUI window. You could replace this area with some buttons, sliders, actual options.";
	
	// Rectangular areas for drawing GUI elements
	// - These should be defined here
	// - Re-creating them every frame in OnGUI would kill framerate
	Rect area_window;				// The menu frame
	Rect button_level1;				// A button to load the first level of the game
	Rect button_level2;
	Rect button_options;			// A button to open the options screen
	Rect area_optionsDummyText;		// An area to draw some text in
	bool isOptionsScreen = false;	// Whether or not we are in the options screen
	
	void Start() {
		// These variables COULD be init'd up above, but:
		// - Using screen dimensions should be done here in start
		// - We can't use variable values up above, we have to wait till start (at least)
		// Creating Rect objects:
		// yourRectangle = new Rect(x position, y position, width, height);
		// - x and y positions = top-left corner of rectangle
		
		// Screen.width / 2 = screen center on the x axis
		// Screen.height / 2 = screen center on the y axis
		// Subtract width/height / 2 from screen center to get the left/top border of the rectangle
		// - Multiplying by 0.5f is faster than dividing by 2.0f
		area_window = new Rect( (Screen.width*0.5f) - (menuWidth*0.5f), (Screen.height*0.5f) - (menuHeight*0.5f), menuWidth, menuHeight);
		// Create some padding insets
		// x position = 10% of menuWidth = menuWidth*0.1f pixels from 0
		// y position = 10% of menuHeight
		// width = 80% of menuWidth
		//	- This gives us 10% padding on each side
		button_level1 = new Rect(menuWidth * 0.1f, menuHeight * 0.1f, menuWidth * 0.8f, 50.0f);
		// This one uses button_level1's dimensions with a simple offset on y position
		button_level2 = new Rect(button_level1.x, button_level1.y + button_level1.height + 10.0f, button_level1.width, button_level1.height);
		button_options = new Rect(button_level1.x, button_level2.y + button_level2.height + 10.0f, button_level1.width, button_level1.height);
		// Create some padding insets
		// These use fixed values measured in pixels
		area_optionsDummyText = new Rect(10.0f, 100.0f, menuWidth - 20.0f, menuHeight - 110.0f);
	}
	
	// OnGUI is called automatically by the engine EVERY frame
	// - This update happens when the engine is updating the GUI system
	//	- Happens AFTER update() has been called
	// This is THE location where you can make GUI draw calls
	void OnGUI() {
		// GUI.BeginGroup(rectangle)
		// - Forces all gui elements drawn AFTER this point to be relative
		//		to the specified rectangle
		// - For elements drawn after this, the top-left corner (0,0) will be
		//		the top-left corner of THIS rectangle
		// - This lets us specify our element positions using (0,0) as the top-left
		//	- Makes moving the GUI around A LOT EASIER
		GUI.BeginGroup(area_window);
		
		// Now we are in a GUI hiearchy:
		// Main Window
		//		Group, positioned relative to the main window
		//			Elements, positioned relative to the group
		//				Sub-Groups, if we need them
		//					Another tier of elements
		
		// Are we in the options screen?
		// - If NO, show the standard main menu
		// - ELSE, show the options menu block
		if (!isOptionsScreen) {
			// Draw the "main menu"
			// GUI.Button(rect, text or content to use as button title);
			// - Draws a button on the screen at rect.x/rect.y with rect dimensions
			// - Returns true or false, indicating whether the button was pressed
			if (GUI.Button(button_level1, "Play Level 1")) {
				// Use the Application object to load level 1
				// Application.LoadLevel("level name string");
				// Application.LoadLevel(levelIDNumber);
				// - Loads the specified the scene/level
				// - Destroys and unloads the current scene
				//	- Our data does NOT persist
				Application.LoadLevel(1);
			}
			if (GUI.Button (button_level2, "Play Level 2")) {
				Application.LoadLevel (2);
			}
			if (GUI.Button(button_options, "Options")) {
				// Setting this to true will cause the options screen to show
				//	on the next frame
				isOptionsScreen = true;
			}
		}
		else {
			if (GUI.Button(button_level1, "Back")) {
				// Setting this to false will cause the main menu to show
				//	on the next frame
				isOptionsScreen = false;
			}
			// GUI.Label:
			// - Draws text within a given rectangle area
			// GUI.Label(rectangle, text);
			GUI.Label(area_optionsDummyText, optionsDummyText);
		}
		
		// We MUST call this to remove our group from the hierarchy!!!!!
		// - You HAVE to call this ONCE for EACH beginGroup() call
		// - AFTER this line, all drawing is relative to the next-highest
		//	 level in the hierarchy
		GUI.EndGroup();
	}
}
