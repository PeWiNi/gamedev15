using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public KeyCode moveUp;// = KeyCode.W;
	public KeyCode moveDown;// = KeyCode.S;
	public KeyCode moveRight;// = KeyCode.D;
	public KeyCode moveLeft;// = KeyCode.A;
	public KeyCode sprint;
	public Vector3 position;
	private bool jumping = false;
	private float zoom = 200.0f;
	private int ms = 1;
	private float jumpHeight = 150.0f;
	private int sprintspeed = 2;
	private Vector3 gravity = new Vector3(0.0f,0.0f,0.0f);
	private Quaternion rotation = new Quaternion ();

	public Camera mainCam;
	
	// Use this for initialization 
	void Start () {
		position = transform.position;
	}

	void split(){
		GameObject newChar = Instantiate(Resources.Load("Prefabs/PlayerObject", typeof(GameObject)) as GameObject,
		new Vector3(transform.position.x + 20, transform.position.y, transform.position.z + 20), Quaternion.identity) as GameObject;
	}

	void keepSteady(){
		rotation.x = 0;
		rotation.z = 0;
		//rotation.y = transform.rotation.y;
		transform.rotation = rotation;
		
	}

	// Update is called once per frame
	void Update () {
				/*if (gravity.y > -900.0f) {
						gravity.y -= 9.81f;
				} else {
						gravity.y = -10.0f;
				}*/
				keepSteady ();
				bool changed = false;
				Vector3 camPos;
				position = transform.position; 
				if (Input.GetKey (moveUp)) {
						if (Input.GetKey (sprint)) {
								position.z += sprintspeed;
						} else { 
								position.z += ms;
						}

						changed = true;
				}
				//Debug.Log (Input.GetAxis ("Mouse ScrollWheel"));
				//positive = in, negative = out/down;
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			zoom -= 20.0f;

		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoom += 20.0f;
		}
		if (Input.GetKeyDown (KeyCode.Space) &&		 !jumping) {
			gravity.y = jumpHeight;
			//rigidbody.velocity = new Vector3(0.0f,10.0f,0.0f);
			jumping = true;
		}
		if(Input.GetKeyDown(KeyCode.B)){
			split();
		}
		if (Input.GetKey (moveDown)) {
			if(Input.GetKey(sprint)){
				position.z -= sprintspeed;
			}else{
				position.z -= ms;
			}

			changed = true;
		}
		if (Input.GetKey (moveRight)) {
			if(Input.GetKey (sprint)){
				position.x += sprintspeed;
			}else{
				position.x += ms;
			}

			changed = true;
		}
		if (Input.GetKey (moveLeft)) {
				if(Input.GetKey (sprint)){
				position.x -= sprintspeed;
				}else{
					position.x -= ms;
				}

			changed = true;
		}
		if (Input.GetKey (KeyCode.Q)) {
			rotation.x += 1;
			transform.rotation = rotation;
			//mainCam.gameObject.transform.Rotate (new Vector3(0.0f,Input.GetAxis("Vertical"), 1.0f));
		}
		if (Input.GetKey (KeyCode.E)) {
			rotation.x -= 1;
			transform.rotation = rotation;
			//mainCam.gameObject.transform.Rotate (new Vector3(0.0f,Input.GetAxis("Vertical"), -1.0f));
		}
		if (jumping || transform.position.y >= 0) {
			//rigidbody.velocity = gravity;
		}
		else {
			//gravity.y = -10.0f;
			//rigidbody.velocity = gravity;
		}
			
		//if (changed) {
			transform.position = position;
			mainCam.gameObject.transform.position = transform.position;
		    //rigidbody.velocity = new Vector3 (0.0f, -10.0f, 0.0f);
			camPos.z = transform.position.z-50; 
			camPos.x = transform.position.x;
		if (zoom < 50.0f) {
			zoom = 50.0f;
		}
		if (zoom > 200.0f) {
			zoom = 200.0f;
		}
			camPos.y = transform.position.y + zoom ;
			mainCam.gameObject.transform.position= camPos;
			mainCam.gameObject.transform.LookAt (transform.position); 
		//}


	}
	/*
	void OnTriggerEnter(Collider coll){
		jumping = false;
		transform.rigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
		position.y = coll.collider.transform.position.y;
		transform.position = position;
	}*/

	void OnCollisionEnter(Collision coll){ // Working!!
		jumping = false;
		position.y = coll.gameObject.transform.position.y;
		transform.position = position;
	}
}
