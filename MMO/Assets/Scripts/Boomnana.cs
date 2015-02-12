using UnityEngine;
using System.Collections;

public class Boomnana : MonoBehaviour {

	public GameObject owner;
	private GameObject thisObj;

	// Use this for initialization
	void Start () {
	
	}

	public void spawn(GameObject owner, GameObject boomnana, Vector3 start, Vector3 direction){
		this.owner = owner;
		this.thisObj = boomnana;
		transform.position = start;
		rigidbody.velocity = direction;
	}

	// Update is called once per frame
	void Update () { 
	
	}
	void OnCollisionEnter(Collision col){
		Destroy (thisObj);
		// Damage col.  Destroy this.object
	}
	void split(){
		GameObject newChar = Instantiate(Resources.Load("Prefabs/PlayerObject", typeof(GameObject)) as GameObject,
		new Vector3(transform.position.x + 20, transform.position.y, transform.position.z + 20), Quaternion.identity) as GameObject;
	}

}
