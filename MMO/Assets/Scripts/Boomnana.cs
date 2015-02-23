using UnityEngine;
using System.Collections;

public class Boomnana : MonoBehaviour {

	public GameObject owner;
	private GameObject thisObj;
	//private float spawnTime;
	float lateralspeed; 
	bool movingBack = false; 
	Vector3 endpoint;
	// Use this for initialization
	void Start () {
	
	}

	public void spawn(GameObject owner, GameObject boomnana, Vector3 start, Vector3 direction){
		this.owner = owner;
		this.thisObj = boomnana;
		transform.position = start;
		endpoint = new Vector3 (start.x + direction.x, start.y + direction.y, start.z + direction.z);
		//spawnTime = Time.time * 1000;
		Vector2 v2 = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
		lateralspeed = v2.magnitude;
	}

	// Update is called once per frame
	void Update () { 
		if (transform.position == endpoint) {
			movingBack = true;
		}
		if (movingBack){
			rigidbody.velocity = new Vector3 (0, 0, 0);
			transform.position = Vector3.MoveTowards (transform.position, owner.gameObject.transform.position, 4.0f);
		} else {
			transform.position = Vector3.MoveTowards(transform.position, endpoint, 4.0f);	
		}
	}
	void OnCollisionEnter(Collision col){
		// if coll = owner, owner.stun, Destroy(thisObj);
		if (col.gameObject != GameObject.Find ("Coconut") && col.gameObject != GameObject.Find ("Terrain") && col.gameObject != owner) {
			// if(!teammate) -> col.GetComponent<WASD>().damage(boomdmg);
			Debug.Log(owner.name); 
			Destroy(thisObj);
		}
		// Damage col.  Destroy this.o		bject
	}

}
