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
	private string currRotStr = "N";
	private bool holding = false;
	GameObject coconut;// = GameObject.Find("Coconut");
	Coconut nut;// = coconut.GetComponent<Coconut>();
	// Use this for initialization
	void Start () {
		coconut = GameObject.Find ("Coconut");
		nut = coconut.GetComponent<Coconut> ();
		//Debug.Log (nut.name + "  " + coconut.name);
	}

	public string getRotStr(){
		return currRotStr;
	}
	void keepSteady(){ 
		//rotation.y = transform.rotation.y;
		//transform.rotation = rotation;
		
	}
	public int getFacing(){
		return currentRotationFace;
	}

	void jump(){
			gravity.y = jumpHeight;
			rigidbody.velocity = gravity;
			jumping = true;
	}

	void setRotation(bool up, bool down, bool left, bool right){
	
		if (up && right) {
			currentRotationFace = 45;
			currRotStr = "NE";
			//transform.rotation = Quaternion.AngleAxis(45 ,Vector3.up);//rotation;
		}else if(up && left){
			currentRotationFace = 315;
			currRotStr = "NW";
			//transform.rotation = Quaternion.AngleAxis(315 ,Vector3.up);//rotation;
		}else if(up){
			currentRotationFace = 0;
			currRotStr = "N";
			//transform.rotation = Quaternion.AngleAxis(0 ,Vector3.up);//rotation;
		}else if(down && left){
			currentRotationFace = 225;
			currRotStr = "SW";
			//transform.rotation = Quaternion.AngleAxis(225 ,Vector3.up);//rotation;
		}else if(down && right){
			currentRotationFace = 135;
			currRotStr = "SE";
			//transform.rotation = Quaternion.AngleAxis(135 ,Vector3.up);//rotation;
		}else if(down){
			currentRotationFace = 180;
			currRotStr = "S";
			//transform.rotation = Quaternion.AngleAxis(180 ,Vector3.up);//rotation;
		}else if(right){
			currentRotationFace = 90;
			currRotStr = "E";
			//transform.rotation = Quaternion.AngleAxis(90 ,Vector3.up);//rotation;
		}else if(left){
			currentRotationFace = 270;
			currRotStr = "W";
			//transform.rotation = Quaternion.AngleAxis(270 ,Vector3.up);//rotation;
		}

			transform.rotation = Quaternion.AngleAxis(currentRotationFace ,Vector3.up);//rotation;
		
		//transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//rotation;

	}
	/* 
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
		if(Input.GetKeyDown(KeyCode.Mouse0)){ // Mouse0 = Left Click
			GameObject boom = Instantiate(Resources.Load("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
			new Vector3(transform.position.x + 20, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			Boomnana boomscript = boom.GetComponent<Boomnana>();
			// set position, add velocity.
			// if return after x sec, unless OnCollision triggers.
			Vector3 startPos = new Vector3();
			Vector3 startDir = new Vector3();
			switch(currRotStr){
			case "N": // Angles might be oposite....
				startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z+10);
				startDir = new Vector3(0, 0, 100); 
				break;
			case "NE":
				startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z+10);
				startDir = new Vector3(100, 0, 100);
				break;
			case "E":
				startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z);
				startDir = new Vector3(100, 0, 0);
				break;
			case "SE":
				startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z-10);
				startDir = new Vector3(100, 0, -100);
				break;
			case "S":
				startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z-10);
				startDir = new Vector3(0, 0, -100);
				break;
			case "SW":
				startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z-10);
				startDir = new Vector3(-100, 0, -100);
				break;
			case "W":
				startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z);
				startDir = new Vector3(-100, 0, 0);
				break;
			case "NW":
				startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z+10);
				startDir = new Vector3(-100, 0, 100);
				break;
			}
			boomscript.spawn(this.gameObject, boom,  startPos
			                 , startDir);
			//boomscript.
		}

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
		/*if (Input.GetKey (KeyCode.Q)) {  
			rotation.y -= 10; 
			transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//rotation;
		}
		if (Input.GetKey (KeyCode.E)) {
			rotation.y += 10;
			transform.rotation = Quaternion.AngleAxis(rotation.y ,Vector3.up);//
		}*/



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
		if (coll.gameObject.name.Equals ("Terrain")) { 
			jumping = false;
		}
		if (Input.GetKey(KeyCode.E)) {

			if (coll.gameObject.name.Equals ("Coconut")) {
				Debug.Log("E is pressed");
				nut = coll.gameObject.GetComponent<Coconut>();
				if(!nut.isHeldAtm()){
				   nut.setCapture(this.gameObject);
					// Debug.Log
				}
			}

		}
		if(Input.GetKey(KeyCode.Q)){
			Debug.Log("q pressed");
			if(nut.getHolder() != null){
				Debug.Log("Has Holder");
				if(nut.getHolder().Equals(this.gameObject)){
					nut.removeCapture(this.gameObject);
					Debug.Log("Holder Removed");
				}
			}
		}

		//position.y = coll.gameObject.transform.position.y;
		//transform.position = position;
	}
}
