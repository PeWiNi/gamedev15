using UnityEngine;
using System.Collections;

public class RandomPlacementGenerator : MonoBehaviour {

	public int terrainWidth;
	public int terrainLength;
	public int noOfInstances;
	public GameObject prefab;
	public float scaleX;
	public float scaleY;
	public float scaleZ;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < noOfInstances; i++) {
			GameObject fab = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			fab.transform.position = new Vector3 (Random.Range (gameObject.transform.position.x + (gameObject.transform.localScale.x / 2), gameObject.transform.position.x - (gameObject.transform.localScale.x / 2)), 
			                                      0.1f, 
			                                      Random.Range (gameObject.transform.position.z + (gameObject.transform.localScale.z / 2), gameObject.transform.position.z - (gameObject.transform.localScale.z / 2)));
			if(scaleX != 0 && scaleY != 0 && scaleY !=0) {
				fab.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
			}
			fab.transform.parent = gameObject.transform;
		}
//		for (int i = 0; i < noOfInstances; i++) {
//			GameObject fab = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
//			fab.transform.position = new Vector3 (Random.Range (terrainWidth / 2, - terrainWidth / 2), 0.1f, Random.Range (terrainLength / 2, -terrainLength / 2));
//			if(scaleX != 0 && scaleY != 0 && scaleY !=0) {
//				fab.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
//			}
//		}
//		Destroy(gameObject);
	}
}
