using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour
{

		TerrainData terrainData;
		TreeInstance[] origCoconutInstances; //using cocnut as a tree
	
		void Start ()
		{
				Terrain terrain = GetComponent<Terrain> ();
				terrainData = terrain.terrainData;
		
				// Backup the original tree instances
				origCoconutInstances = terrainData.treeInstances;
		
				// Instantiate a prefab for each of the mass-placed trees
				foreach (TreeInstance ti in terrainData.treeInstances) {
						TreePrototype treePrototype = terrainData.treePrototypes [ti.prototypeIndex];
						GameObject treePrefab = treePrototype.prefab;
			
						if (treePrefab.tag == "nut") {
								Vector3 treePosition = Vector3.Scale (ti.position, terrainData.size) + terrain.transform.position;
				
								//GameObject coconutObject = (GameObject)Instantiate (treePrefab, treePosition, Quaternion.identity);
								//coconutObject.GetComponent<Coconut> ().coconutId = coconutObject.GetInstanceID ();
						}
				}
		
				// Get rid of the mass-placed trees
				terrainData.treeInstances = new TreeInstance[0];
		
				// Refresh the terrain
				float[,] terrainHeights = terrainData.GetHeights (0, 0, 0, 0);
				terrainData.SetHeights (0, 0, terrainHeights);
		}
	
		// Application quits and the original trees are put back onto the terrain
		void OnApplicationQuit ()
		{
				terrainData.treeInstances = origCoconutInstances;
		}
}

