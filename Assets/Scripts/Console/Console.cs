/// <summary>
/// Implements the main window of the console
/// Handles text input and output for the console window
/// Intercepts Debug.Log, Debug.LogWarning, Debug.LogError messages and prints them to the console window
/// </summary>
using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
	public KeyCode openKey = KeyCode.BackQuote;	// The key used to open and close the console window
	public bool collectLog = true;				// Do we want to intercept Debug.Log's?
	public int historySize = 256;				// How many history entries to store in the log
	public Color defaultColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);	// Text color for non-color-coded entries
	public Color[] colorCodes;					// Configurable color-codes for entries
	
	enum eConsoleStates {CLOSED=0, OPEN, STARTUP, SHUTDOWN};	// The possible console states
	eConsoleStates currentState = eConsoleStates.CLOSED;		// The current console state
	
	Vector2 scrollPosition = Vector2.zero;		// Current position of the scrolling history window
	float scrollHeight = 3.0f;					// Console window height * scrollHeight = height of scrolling history window
	
	bool isActive = false;						// Is the console currently "on" (OPEN, STARTUP, OR SHUTDOWN)
	float transitionTime = 1.0f;				// The time it takes for the window to slide in and out
	float transitionStarted = 0.0f;				// The time that a transition was started
	
	Rect consoleArea = new Rect(0.0f, 0.0f, Screen.width, Screen.height*0.5f-26.0f);	// The area for the console background
	Rect historyArea;							// The area to print scrollable history text into
	Rect entryArea = new Rect(0.0f, Screen.height*0.5f-26.0f, Screen.width, 24.0f);	// The area to enter commands into
	
	Vector2 consolePos = new Vector2();			// The current center of the console window
	string currentEntry = "";					// The current command being entered
	
	// ArrayList:
	// An array that can change size dynamically
	ArrayList history;							// The history text elements (entries)
	ArrayList priorities;						// The color-codes for each history entry
	
	string[] commandWords;						// Parsed strings from currentEntry. Used for determining commands and parameters
	
	// Keeps additional consoles from being opened when switching levels
	// static means that this variable is shared between ALL instances of the Console class
	// - i.e. they all have the same values
	static bool alive = false;
	
	// get/set properties:
	// - Let you call a function without ()
	// - The code will appear as if you are just accessing a data member (variable.property)
	// - If we implement the "get": we can read this data by saying value = variable.property;
	// - If we implement the "set": we can assign this data by saying variable.property = value;
	//
	// Syntax:
	// public returnType PropertyName {
	//		get { return some variable that is returnType }
	//		set { variable = value; }
	// }
	public string[] CommandWords {
		get {return commandWords;}
	}

	// Use this for initialization
	void Start () {
		if (alive == false) {
			// make this object not get destroyed when a new level is loaded
			DontDestroyOnLoad(this);
			history = new ArrayList(historySize);
			priorities = new ArrayList(historySize);
			
			// Set the size for the scrolling window
			// Note: the -18.0f in the width is to make sure the text doesn't go behind the scroll bar
			historyArea = new Rect(0.0f, 0.0f, Screen.width-18.0f, Screen.height*scrollHeight);
			
			// Register a Debug.Log callback, if we want one
			if (collectLog) {
				// Application object has a function to register this callback: RegisterLogCallback(functionName)
				Application.RegisterLogCallback(OnDebugLog);
			}
			
			// set the console variable for all console command scripts attached
			ConsoleCommands[] comms = gameObject.GetComponentsInChildren<ConsoleCommands>();
			foreach (ConsoleCommands cc in comms) {
				cc.SetConsole = this;
			}
			
			alive = true;	// don't forget this!
		}
		else {
			Destroy(this);
		}
	}
	
	void OnGUI() {
		// Inside the OnGUI function, we can't rely on the Input object
		// So, we need to rely on the Event system to retrieve text-based input
		Event e = Event.current; // the current event happening this frame
		// Finite State Machine
		// A finite set of states that can be "switched"
		// Each state has a dedicated set of behaviors
		// The only way to change the state is by meeting a specific condition inside the current state
		switch (currentState)
		{
		case eConsoleStates.CLOSED:
			if (e.type == EventType.KeyDown && e.keyCode == openKey) {
				// position the console in horizontal center, half-height above screen top
				consolePos.x = Screen.width * 0.5f;
				consolePos.y = -(Screen.height*0.5f);
				
				// set the scrolling window position to the bottom of the scrolling window area
				scrollPosition.x = 0.0f;
				scrollPosition.y = Screen.height * scrollHeight;
				
				// set data needed for the next state
				isActive = true;
				transitionStarted = Time.time;
				currentEntry = "";
				currentState = eConsoleStates.STARTUP;	// move to the next state
			}
			break;
		case eConsoleStates.OPEN:
			if (e.type == EventType.KeyDown && e.keyCode == openKey) {
				transitionStarted = Time.time;
				currentState = eConsoleStates.SHUTDOWN;
				return;
			}
			break;
		case eConsoleStates.STARTUP:
			consolePos.y = Mathf.Lerp(consolePos.y, Screen.height*0.25f, (Time.time-transitionStarted)/transitionTime);
			if (Time.time - transitionStarted >= transitionTime) {
				consolePos.y = Screen.height * 0.25f; // just in case we go a little too far with lerp
				currentState = eConsoleStates.OPEN;
			}
			break;
		case eConsoleStates.SHUTDOWN:
			consolePos.y = Mathf.Lerp(consolePos.y, -(Screen.height*0.5f), (Time.time-transitionStarted)/transitionTime);
			if (Time.time - transitionStarted >= transitionTime) {
				isActive = false;
				currentState = eConsoleStates.CLOSED;
			}
			break;
		default:
			break;
		}
		
		// If we're not active, don't draw anything below!
		if (!isActive) {return;}
		
		
		// start the gui group for the whole console
		GUI.BeginGroup(Rect.MinMaxRect(0.0f, consolePos.y-Screen.height*0.25f, Screen.width, consolePos.y+Screen.height*0.25f));
		
		GUI.Box(consoleArea, GUIContent.none);	// bg box
		GUI.SetNextControlName("entry");		// sets a reference name for the next item we draw
		
		// If we're in an OPEN state, draw the text entry field and accept text input
		if (currentState == eConsoleStates.OPEN) {
			if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return) {
				// capture command
				OnCommand(currentEntry);
				currentEntry = "";
			}
			else {
				// draw a text field and capture its input
				currentEntry = GUI.TextField(entryArea, currentEntry);
				GUI.FocusControl("entry"); // make the text field have focus EVERY FRAME while open
			}
		}
		
		// Draw the scrolling window
		scrollPosition = GUI.BeginScrollView(consoleArea, scrollPosition, historyArea, false, true);
		
		// draw the history
		// This is part of the scroll-view group, so will be positioned correctly within the scrolling window
		int i = 0;
		// foreach syntax: foreach (dataType variable in array) { }
		foreach (string s in history) {
			if ((int)priorities[i] < 0 || (int)priorities[i] > colorCodes.Length-1) {
				GUI.color = defaultColor;
			}
			else {
				GUI.color = colorCodes[(int)priorities[i]];
			}
			
			GUI.Label(new Rect(4.0f, historyArea.height - (30.0f+(24.0f*i)), Screen.width, 24.0f), s);
			i++;
		}
		GUI.EndScrollView();
		
		GUI.EndGroup();
	}
	
	// Splits the command entry into individual command and parameter words
	void OnCommand(string command) {
		commandWords = command.Split(new char[]{' '});
		// SendMessage:
		// - Takes in a string as the parameter
		// - Then it searches all the scripts attached to the game object
		//	- It compares the string to all the function names in the scripts
		//	- If there is a match, that function is executed
		gameObject.SendMessage(commandWords[0]);
	}
	
	void OnDebugLog(string logString, string stackTrace, LogType type) {
		switch (type)
		{
		case LogType.Assert:
		case LogType.Error:
		case LogType.Exception:
			Log(logString, 2);
			break;
		case LogType.Log:
			Log(logString, -1);
			break;
		case LogType.Warning:
			Log(logString, 1);
			break;
		}
	}
	
	public void Log(string entry, int priority) {
		history.Insert(0, entry);
		priorities.Insert(0, priority);
	}
}

















