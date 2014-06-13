using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnDelay = 2.0f;
	public int totalSpawns = 10;
	public GameObject enemyPrefab;
	float spawnClock;
	int spawnsLeft;
	
	// Use this for initialization
	void Start () {
		spawnClock = spawnDelay;
		spawnsLeft = totalSpawns;
	}
	
	// Update is called once per frame
	void Update () {
		spawnClock -= Time.deltaTime;
		
		if (spawnClock <= 0.0f) {
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			
			spawnsLeft--;
			// Check if there are spawns left, otherwise disable ourselves
			if (spawnsLeft > 0)
				spawnClock = spawnDelay;
			else 
				gameObject.SetActive(false);
		}
	}
}
