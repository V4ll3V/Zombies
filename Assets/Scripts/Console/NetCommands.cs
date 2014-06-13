using UnityEngine;
using System.Collections;

public class NetCommands : ConsoleCommands {
	public GameObject netPlayerPrefab;
	GameObject myNetPlayer;
	static int maxPlayers = 32;
	static int serverPort = 25000;
	static bool serverNat = false;
	
	bool connected = false;
	
	NetworkView myNetView;
	
	void Start() {
		myNetView = GetComponent<NetworkView>();
	}
	
	// configure
	void netMaxPlayers() {
		maxPlayers = int.Parse(console.CommandWords[1]);
	}
	
	void netServerPort() {
		serverPort = int.Parse(console.CommandWords[1]);
	}
	
	void netData() {
		console.Log("IP: " + Network.player.ipAddress, 0);
		console.Log("max players: " + maxPlayers, 0);
		console.Log("incoming port: " + serverPort, 0);
	}
	
	// action
	void launchServer() {
		console.Log("Attemping to launch server with " + maxPlayers + " slots, at port " + serverPort, 1);
		Network.InitializeServer(maxPlayers, serverPort, serverNat);
	}
	
	void connectToServer() {
		console.Log("Attempting to connect to server at " + console.CommandWords[1] + " on port " + console.CommandWords[2], 1);
		Network.Connect(console.CommandWords[1], int.Parse(console.CommandWords[2]));
	}
	
	void spawnNetPlayer() {
		if (connected) {
			myNetPlayer = (GameObject)Instantiate(netPlayerPrefab, netPlayerPrefab.transform.position, netPlayerPrefab.transform.localRotation);
		}
	}
	
	void tell() {
		string tell = Network.player.ipAddress + ": ";
		for (int i = 1; i < console.CommandWords.Length; i++) {
 			tell += " " + console.CommandWords[i];
		}
		
		myNetView.RPC("Chat", RPCMode.All, tell);
	}
	
	
	// messages
	void OnServerInitialized() {
		console.Log("Server was launched successfully", 0);
		connected = true;
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		console.Log("New player connected: " + player.ipAddress, 0);
	}
	
	void OnConnectedToServer() {
		console.Log("Connection to server was successfull", 0);
		connected = true;
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
		console.Log("Connection to server failed because: " + error, 2);
	}
	
	// RPC
	[RPC]
	void Chat(string text) {
		console.Log(text, -1);	
	}
}
