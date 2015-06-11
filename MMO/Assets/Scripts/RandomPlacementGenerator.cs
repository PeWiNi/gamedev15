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
	public bool isBorder = true;// Whether or not it is an edge of the world.
	public bool isLocalised = true;// Set to false if we want to expand it all over the terrain area.
	
	// Use this for initialization
	void Start () {
		if (isLocalised) {
			for (int i = 0; i < noOfInstances; i++) {
				GameObject fab = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
				if (isBorder) {
					fab.transform.position = new Vector3 (Random.Range (gameObject.transform.position.x + (gameObject.transform.localScale.x / 2), gameObject.transform.position.x - (gameObject.transform.localScale.x / 2)), 
				                                      0.1f, 
				                                      Random.Range (gameObject.transform.position.z + (gameObject.transform.localScale.z / 2), gameObject.transform.position.z - (gameObject.transform.localScale.z / 2)));
				}
				if (!isBorder) {
					fab.transform.position = new Vector3 (Random.Range (0f, gameObject.GetComponent<BoxCollider> ().bounds.center.x), 
				                                      0.1f, 
					                                      Random.Range (0f, gameObject.GetComponent<BoxCollider> ().bounds.center.z));
					fab.transform.rotation = gameObject.transform.rotation;//set the same rotation as this gameObject.
					Debug.Log ("Fab " + fab + ", Center x = " + gameObject.GetComponent<BoxCollider> ().bounds.center.x);
					Debug.Log ("Fab " + fab + ", Center z = " + gameObject.GetComponent<BoxCollider> ().bounds.center.z);
				}
				if (scaleX != 0 && scaleY != 0 && scaleY != 0) {
					fab.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
				}
				fab.transform.parent = gameObject.transform;
			}
		} else {
			for (int i = 0; i < noOfInstances; i++) {
				GameObject fab = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
				fab.transform.position = new Vector3 (Random.Range (terrainWidth / 2, - terrainWidth / 2), 0.1f, Random.Range (terrainLength / 2, -terrainLength / 2));
				if (scaleX != 0 && scaleY != 0 && scaleY != 0) {
					fab.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
				}
				fab.transform.parent = gameObject.transform;
			}
		}
//		Destroy(gameObject);
	}
}
