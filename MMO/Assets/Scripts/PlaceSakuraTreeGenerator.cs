using UnityEngine;
using System.Collections;

public class PlaceSakuraTreeGenerator : MonoBehaviour {

	public int terrainWidth;
	public int terrainLength;
	public int noOfInstances;
	public GameObject prefab;
	
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < noOfInstances; i++) {
			GameObject fab = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			fab.transform.position = new Vector3 (Random.Range (terrainWidth / 2, - terrainWidth / 2), 2f, Random.Range (terrainLength / 2, -terrainLength / 2));
			fab.transform.localScale = new Vector3 (100f, 100f, 100f);
		}
		Destroy(gameObject);
	}
}
