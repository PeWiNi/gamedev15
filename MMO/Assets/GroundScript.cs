using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	/*void OnTriggerEnter(Collider coll){
		MovementScript ms = coll.gameObject.GetComponent<MovementScript> ();
		ms.position.y = transform.position.y;
		GameObject go = GameObject.Find ("PlayerObject");
		Vector3 newPos;
		if (coll.gameObject.Equals(go)) {
			newPos = go.transform.position;
			newPos.y = transform.position.z;
				go.transform.position = newPos;
			go.rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);

		}
		//coll.gameObject.transform.position.y = transform.position.y;
	}*/
}
