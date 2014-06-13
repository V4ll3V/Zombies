using UnityEngine;
using System.Collections;

public class GameObjectCommands : ConsoleCommands {
	public GameObject toSpawn; // an object to spawn via command
	public GameObject spawnLoc;// a position to spawn an object at
	
	// Creating a new command
	// - Name MUST be unique
	// - Name should be easy to type
	//	- not too long
	//	- not hard to spell
	// - All command functions should return NOTHING
	//	- i.e. void myCommandName()...
	// - Command functions do not have parameters
	void testspawn() {
		// Simple, no extra data needed...
		// Just spawn the prefab
		Instantiate(toSpawn, spawnLoc.transform.position, Quaternion.identity);
		
		// best practice: print some feedback to the console for ALL commands
		console.Log ("Spawned a new instance of our object", -1);
	}
	
	// This is a simple spawn command, similar to testspawn
	// This one allows the console user to specify how many clones
	//	to create.
	void spawnmany() {
		// The user should be able to type something like:
		//  spawnmany 10
		// Our console.CommandWords array would look like:
		//	console.CommandWords[0] would be "spawnmany"
		//	console.CommandWords[1] would be "10"
		//
		// Things we should do to make sure our data is good:
		// - Check if CommandWords length > 1
		// - Create an int variable/representation of the 
		//		count string
		if (console.CommandWords.Length > 1) {
			// We have the data (hopefully)
			// Convert the data from string to int
			int count = int.Parse (console.CommandWords[1]);
			// Loop through as many items as the data requests
			for (int i = 0; i < count; i++) {
				Instantiate(toSpawn, spawnLoc.transform.position, Quaternion.identity);
			}
			
			console.Log ("Spawned " + count + " objects", 0);
		}
		else {
			// We don't have enough data
			console.Log ("You didn't give me data...WTF...by the way, this is the spawnmany command!", 2);
		}
	}
}
