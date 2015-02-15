using UnityEngine;
using System.Collections;

public class Boomnana : MonoBehaviour {

	public GameObject owner;
	private GameObject thisObj;
	private float spawnTime;
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
		//rigidbody.velocity = direction;
		endpoint = new Vector3 (start.x + direction.x, start.y + direction.y, start.z + direction.z);
		//transform.position = Vector3.MoveTowards (transform.position , endpoint,4.0f);
		spawnTime = Time.time * 1000;
		Vector2 v2 = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
		lateralspeed = v2.magnitude;
	}

	// Update is called once per frame
	void Update () { 
		float currTime = Time.time * 1000;
		if (transform.position == endpoint) {
			movingBack = true;
		}
		if (movingBack){//currTime - spawnTime >= 2000) {
						// Boomerang going back
						// However, currently it is slowing down when reaching the "caster"
						//rigidbody.velocity.x = transform.forward.x lateralspeed;
						//rigidbody.AddForceAtPosition(transform.position, owner.transform.position);

			rigidbody.velocity = new Vector3 (0, 0, 0);
			transform.position = Vector3.MoveTowards (transform.position, owner.gameObject.transform.position, 4.0f);//Vector3.Lerp(transform.position, owner.transform.position, Time.time);
		} else {
			transform.position = Vector3.MoveTowards(transform.position, endpoint, 4.0f);	
		}
	}
	void OnCollisionEnter(Collision col){
		// if coll = owner, owner.stun, Destroy(thisObj);
		if (movingBack) {
			if (col.gameObject == owner) {
				//Owner.stun();
				Destroy (thisObj);
			}
		} else if (col.gameObject != GameObject.Find ("Coconut") && col.gameObject != GameObject.Find ("Terrain") && col.gameObject != owner) {
			// if(!teammate) -> col.GetComponent<WASD>().damage(boomdmg);
			Destroy(thisObj);
		}
		//Destroy (thisObj);
		// Damage col.  Destroy this.o		bject
	}
	void split(){
		GameObject newChar = Instantiate(Resources.Load("Prefabs/PlayerObject", typeof(GameObject)) as GameObject,
		new Vector3(transform.position.x + 20, transform.position.y, transform.position.z + 20), Quaternion.identity) as GameObject;
	}

}
