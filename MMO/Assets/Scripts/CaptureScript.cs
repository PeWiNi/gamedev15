using UnityEngine;
using System.Collections;

public class CaptureScript : MonoBehaviour {
	public GameObject theNut;
	GameObject theHolder;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		//Debug.Log (theNut.name);
		if (coll.gameObject.name == "Coconut") {
			//Debug.Log("IT MATCHES");
			Coconut nutScript = coll.GetComponent<Coconut>();
			theHolder = nutScript.getHolder();
			Vector3 origin = nutScript.startPos;
			//setScore +1 for ScoreScript.score(theHolder.teamNumber, 1);
			nutScript.removeCapture();
			nutScript.transform.position = origin;

		}
	}
}
