using UnityEngine;
using System.Collections;

public class WASD : MonoBehaviour {

	/*TODO: Make state controller, so you don't connect/split during combat.
	 * 
	 * */
	
	public KeyCode moveUp;// = KeyCode.W;
	public KeyCode moveDown;// = KeyCode.S;
	public KeyCode moveRight;// = KeyCode.D;
	public KeyCode moveLeft;// = KeyCode.A;
	private KeyCode sprint = KeyCode.LeftShift;
	public Vector3 position;
	private bool jumping = false;
	private float zoom = 200.0f;
	private int ms = 1;
	public Camera mainCam;
	private float jumpHeight = 200.0f; 
	private int sprintspeed = 2;
	Vector3 camPos;
	private Vector3 gravity;
	private Quaternion rotation;
	private int currentRotationFace;
	// Use this for initialization
	void Start () {
	
	}

	void keepSteady(){ 
		//rotation.y = transform.rotation.y;
		//transform.rotation = rotation;
		
	}

	void jump(){
			gravity.y = jumpHeight;
			rigidbody.velocity = gravity;
			jumping = true;
	}

	void setRotation(bool up, bool down, bool left, bool right){
	
		if (up && right) {
			currentRotationFace = 45;
			//transform.rotation = Quaternion.AngleAxis(45 ,Vector3.up);//rotation;
		}else if(up && left){
			currentRotationFace = 315;
			//transform.rotation = Quaternion.AngleAxis(315 ,Vector3.up);//rotation;
		}else if(up){
			currentRotationFace = 0;
			//transform.rotation = Quaternion.AngleAxis(0 ,Vector3.up);//rotation;
		}else if(down && left){
			currentRotationFace = 225;
			//transform.rotation = Quaternion.AngleAxis(225 ,Vector3.up);//rotation;
		}else if(down && right){
			currentRotationFace = 135;
			//transform.rotation = Quaternion.AngleAxis(135 ,Vector3.up);//rotation;
		}else if(down){
			currentRotationFace = 180;
			//transform.rotation = Quaternion.AngleAxis(180 ,Vector3.up);//rotation;
		}else if(right){
			currentRotationFace = 90;
			//transform.rotation = Quaternion.AngleAxis(90 ,Vector3.up);//rotation;
		}else if(left){
			currentRotationFace = 270;
			//transform.rotation = Quaternion.AngleAxis(270 ,Vector3.up);//rotation;
		}

			transform.rotation = Quaternion.AngleAxis(currentRotationFace ,Vector3.up);//rotation;
		
		//transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//rotation;

	}
	/* TODO:
		 * Make it so that the rotation of the character changes depending on which direction 
		 * it is going.
		 * 8 directions -> N, NE, E, SE, S, SW, W, NW
		 * N = 0 degrees
		 * NE = 45 deg
		 * E = 90 deg
		 * SE = 135 deg
		 * S = 180 deg
		 * SW = 225 deg
		 * W = 270 deg
		 * NW = 315 deg
		 */
	
	// Update is called once per frame
	void Update () {
		bool up, down, left, right;
		up = false;
		down = false;
		left = false;
		right = false;

		keepSteady ();

		position = transform.position; 
		if (Input.GetKey (moveUp)) {
			up = true;
			if (Input.GetKey (sprint)) {
				position.z += sprintspeed;
			} else { 
				position.z += ms;
			}
			
		//	changed = true;
		}

		if (Input.GetKey (moveDown)) {
			down = true;
			if(Input.GetKey(sprint)){
				position.z -= sprintspeed;
			}else{
				position.z -= ms;
			}
			
			//changed = true;
		}

		if (Input.GetKey (moveRight)) {
			right = true;
			if(Input.GetKey (sprint)){
				position.x += sprintspeed;
			}else{
				position.x += ms;
			}
			
			//changed = true;
		}
		if (Input.GetKey (moveLeft)) {
			left = true;
			if(Input.GetKey (sprint)){
				position.x -= sprintspeed;
			}else{
				position.x -= ms;
			}
			
			//changed = true;
		}
		if (Input.GetKeyDown (KeyCode.Space) && !jumping) {
			jump ();		
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			zoom -= 20.0f;
			
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoom += 20.0f;
		}
		if (Input.GetKey (KeyCode.Q)) {  
			rotation.y -= 10; 
			transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//rotation;
		}
		if (Input.GetKey (KeyCode.E)) {
			rotation.y += 10;
			transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//
		}



		/* TODO:
		 * Make it so that the rotation of the character changes depending on which direction 
		 * it is going.
		 * 8 directions -> N, NE, E, SE, S, SW, W, NW
		 * N = 0 degrees
		 * NE = 45 deg
		 * E = 90 deg
		 * SE = 135 deg
		 * S = 180 deg
		 * SW = 225 deg
		 * W = 270 deg
		 * NW = 315 deg
		 */

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
		setRotation (up, down, left, right);
		right = false;
		left = false;
		up = false;
		down = false;
		
	}
	void OnCollisionEnter(Collision coll){ // Working!!
		jumping = false;
		//position.y = coll.gameObject.transform.position.y;
		//transform.position = position;
	}
}
