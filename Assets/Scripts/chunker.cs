/// <summary>
/// Takes a rectangular mesh and subdivides it a specified number of times
/// When the last subdivision has been reached, the script spawns chunk objects,
/// 	and applies an explosion force to them
/// </summary>
using UnityEngine;
using System.Collections;

public class chunker : MonoBehaviour {

	public GameObject chunk;			// The final chunk prefab that gets spawned and exploded upon last subdivision
	public int subdivisionLevels = 0;	// How many times the original mesh gets subdivided before being "chunked"
	public int subdividesAcross = 1;	// How many columns to divide the mesh into
	public int subdividesHigh = 1;		// How many rows to divide the mesh into
	public int subdividesDeep = 1;		// How many layers deep to divide the mesh into
	public int chunksAcross = 2;		// How many columns to create chunks for
	public int chunksHigh = 2;			// How many rows to create chunks for
	public int chunksDeep = 1;			// How many layers of depth for chunks
	public float health = 100.0f;		// How much health each base mesh has
	public float force = 100.0f;		// How much explosion force to apply to each chunk
	public float radius = 10.0f;		// The radius for the chunk explosion
	public float upMod = 0.0f;			// How much UP force to apply on the explosion
	public int layerIgnore = 8;			// Set this to ignore objects that shouldn't cause a health drop
	public bool changeGravity = false;	// Do we want chunks to have gravity?
	int currentSubdivision = 0;			// For tracking where we are, relative to the start
	float damageAmount = 20.0f;			// How much health is subtracted on each collision
	bool chunked = false;				// Flagged when chunks are created
	
	Mesh master;						// The original mesh (the mesh we spawned with)
	float masterWidth;					// Bounds of mesh * game object scale
	float masterHeight;					/////  " "
	float masterDepth;					/////  " "
	
	// Use this for initialization
	void Start () {
		// Grab the "master", the mesh that our game object owns
		master = GetComponent<MeshFilter>().mesh;
		// Get proper bounds by scaling mesh bounds * game object scale
		masterWidth = master.bounds.size.x * transform.localScale.x;
		masterHeight = master.bounds.size.y * transform.localScale.y;
		masterDepth = master.bounds.size.z * transform.localScale.z;
	}
	
	// New Function: OnCollisionEnter(Collision collision)
	// 		If we have an object with a collider attached, we can implement this function
	//		 in any script attached to that object. When the collider is hit, this function
	//		 will be called for us.
	//
	// We are using this to actually do the subdivisions/chunking
	void OnCollisionEnter(Collision collision)
	{
		if (chunked) {return;}	// return if we've already blown up (multiple hits per frame scenario)
		if (collision.gameObject.layer == layerIgnore) {return;} // return if this object is using the ignored layer
		if (collision.rigidbody) {	
			health -= damageAmount;
			if (health <= 0.0f) {	
				chunked = true;
				// now setup the brick dimensions and data
				// these variables determine the size and quantity per row/column/depth of bricks
				float across;
				float high;
				float deep;
				float chunkWidth;
				float chunkHeight;
				float chunkDepth;
				bool useExplosionForce = false;
				GameObject brick;
				if (currentSubdivision >= subdivisionLevels) {
					Debug.Log("CHUNKED!");
					Debug.Log("curSub: " + currentSubdivision);
					brick = chunk;
					useExplosionForce = true;
					across = chunksAcross;
					high = chunksHigh;
					deep = chunksDeep;
				}
				else {
					brick = gameObject;
					across = subdividesAcross;
					high =  subdividesHigh;
					deep = subdividesDeep;
				}
				currentSubdivision++;
				
				chunkWidth = masterWidth / across;
				chunkHeight = masterHeight  / high;
				chunkDepth = masterDepth / deep;
				
	
				
				// note: these are in LOCAL space
				float left = masterWidth * -0.5f + (chunkWidth*0.5f);
				float bot  = masterHeight * -0.5f + (chunkHeight*0.5f);
				float back = masterDepth * -0.5f + (chunkDepth*0.5f);
				
				//Debug.Log(chunkWidth);
				//Debug.Log(chunkHeight);
				//Debug.Log(chunkDepth);
				
				Mesh child;
				float childWidth;
				float childHeight;
				float childDepth;
				
				Vector3 pos;
				Vector3 scale;
				// the chunked mesh is filled like so:
				// from left to right, bottom to top: row by row
				// if there are multiple chunks deep, each row is done as deep as it goes, then the next row up is done
				float column;
				float depth;
				
				chunker newChunker;
				for (int i = 0; i < high; i++) {
					column = 0;
					depth = 0;
					for (int j = 0; j < across*deep; j++, column++) {
						if (column == across) {
							column = 0;
							depth++;
						}
						
						pos.x = left + chunkWidth*column;
						pos.y = bot + chunkHeight*i;
						pos.z = back + chunkDepth*depth;
						pos = transform.TransformDirection(pos);
						
						brick = (GameObject)Instantiate(brick, pos+transform.position, transform.rotation);
						child = brick.GetComponent<MeshFilter>().mesh;	// this HAS to be done here, instead of once above, otherwise it corrupts the asset in the project (weird?)
						childWidth = child.bounds.size.x;
						childHeight = child.bounds.size.y;
						childDepth = child.bounds.size.z;
						
						scale.x = chunkWidth / childWidth;
						scale.y = chunkHeight / childHeight;
						scale.z = chunkDepth / childDepth;
						brick.transform.localScale = scale;
						
						newChunker = brick.GetComponent<chunker>();
						if (newChunker) {
							newChunker.currentSubdivision = currentSubdivision+1;
							//Debug.Log("Found chunker");
						}
						
						if (useExplosionForce) {
							brick.rigidbody.velocity = Vector3.zero;
							brick.rigidbody.useGravity = !changeGravity;
							brick.rigidbody.AddExplosionForce(force, collision.transform.position, radius, upMod);
						}
					}
				}
				
				Destroy(gameObject);
			}
		}
	}
}
