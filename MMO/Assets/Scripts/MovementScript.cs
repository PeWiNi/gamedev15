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
	private int ms = 1;
	private int sprintspeed = 2;

	public Camera mainCam;
	
	// Use this for initialization 
	void Start () {
		position = transform.position;
	}

	void split(){

	}

	// Update is called once per frame
	void Update () {
		bool changed = false;
		Vector3 camPos;
		position = transform.position; 
		if(Input.GetKey(moveUp)){
			if(Input.GetKey (sprint)){
				position.z += sprintspeed;
			}else{
				position.z += ms;
			}

			changed = true;
		}
		if (Input.GetKeyDown (KeyCode.Space) && !jumping) {
			rigidbody.velocity = new Vector3(0.0f,10.0f,0.0f);
			jumping = true;
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
			mainCam.gameObject.transform.Rotate (new Vector3(0.0f,Input.GetAxis("Vertical"), 1.0f));
		}
		if (Input.GetKey (KeyCode.E)) {
			mainCam.gameObject.transform.Rotate (new Vector3(0.0f,Input.GetAxis("Vertical"), -1.0f));
		}
			
		//if (changed) {
			transform.position = position;
			mainCam.gameObject.transform.position = transform.position;
		    //rigidbody.velocity = new Vector3 (0.0f, -10.0f, 0.0f);
			camPos.z = transform.position.z-50; 
			camPos.x = transform.position.x;
			camPos.y = transform.position.y + 200 ;
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
