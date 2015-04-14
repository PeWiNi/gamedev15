using UnityEngine;
using System.Collections;

public class HidingGrassScript : MonoBehaviour
{
	Vector3 biggestScaledMonguin;

	// Use this for initialization
	void Start ()
	{
		biggestScaledMonguin = new Vector3 (3, 3, 3);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerStay (Collider coll)
	{
		// should also consider unit size!
		if (coll.gameObject.tag == "player") {
			if (coll.transform.localScale != biggestScaledMonguin) {
				var child = coll.transform.GetChild (5);
				coll.GetComponent<Renderer> ().enabled = false;
				child.GetComponent<Canvas> ().enabled = false;
			}
		}
	}
		
	void OnTriggerExit (Collider coll)
	{
		// should also consider unit size!
		if (coll.gameObject.tag == "player") {
			if (coll.transform.localScale != biggestScaledMonguin) {
				var child = coll.transform.GetChild (5);
				coll.GetComponent<Renderer> ().enabled = true;
				child.GetComponent<Canvas> ().enabled = true;
			}
		}
	}
}
