using UnityEngine;
using System.Collections;

// This is the base class/component for console commands
public class ConsoleCommands : MonoBehaviour {
	// A reference to the console shell itself
	// Protected data:
	// - Hidden from the editor
	// - Exposed to child components
	protected Console console;
	// Adding public properties can allow "public"
	// access to data without exposing it to the editor
	public Console SetConsole {
		set {console = value;}
	}
	
	// The log command
	// - Command function name IS the name of the command
	//	- i.e. in the console you'd type "log" to call this function
	void log() {
		string output = "";
		// Loop through all command words (strings) and 
		// recreate the entered phrase
		for (int i = 1; i < console.CommandWords.Length; i++) {
			// Add this string PLUS a space to the output string
			output += console.CommandWords[i] + " ";
		}
		
		// Tell the console to output our compiled string/phrase
		// - Use the color code 0 for green (or whatever)
		console.Log(output, 0);
	}
}
