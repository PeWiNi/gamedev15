using UnityEngine;
using System.Collections;
using System;

public class WASD : MonoBehaviour {

	/*TODO: Make state controller, so you don't connect/split during combat.
	 * 
	 * */

	public int teamNumber; 
	
	public KeyCode moveUp;// = KeyCode.W;
	public KeyCode moveDown;// = KeyCode.S;
	public KeyCode moveRight;// = KeyCode.D;
	public KeyCode moveLeft;// = KeyCode.A;
	private KeyCode sprint = KeyCode.LeftShift;
	public Vector3 position;
	public int boomnanaRange;
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
	private bool moving = false;
	private bool stunned = false;
	private float stunnedTimer = 4.0f;
	private float stunnedStart;
	GameObject coconut;// = GameObject.Find("Coconut");
	Coconut nut;// = coconut.GetComponent<Coconut>();
	//AudioSource
	AudioSource soundPlayer;
	AudioSource movementPlayer;
	AudioSource jumpPlayer;
	AudioSource stunnedPlayer;
	// Audioclips
	public AudioClip boomnanathrowclip;
	public AudioClip jumpclip;
	public AudioClip movementclip;
	public AudioClip stunnedclip; 

	private float timeSinceLastBoom;
	// Use this for initialization
	void Start () {
		GameObject jP = Instantiate(Resources.Load("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                              new Vector3(-1000, 0, -1000), Quaternion.identity) as GameObject;
		jumpPlayer = jP.audio;
		GameObject bP = Instantiate(Resources.Load("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                            new Vector3(-1000, 0, -1000), Quaternion.identity) as GameObject;
		movementPlayer = bP.audio;
		GameObject sP = Instantiate(Resources.Load("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                            new Vector3(-1000, 0, -1000), Quaternion.identity) as GameObject;
		stunnedPlayer = sP.audio;
		timeSinceLastBoom = Time.time * 1000;
		coconut = GameObject.Find ("Coconut");
		nut = coconut.GetComponent<Coconut> ();
		soundPlayer = gameObject.audio;
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
			//Debug.Log("Player pos: "+transform.position);
			Camera cam = Camera.main;//.Find("Main Camera");
			//Debug.Log(cam.name);
			Vector3 p = cam.ScreenToWorldPoint(new Vector3(100, 100, cam.nearClipPlane));
			//Debug.Log("Projectile dir: "+p);
			//Debug.Log(p);
			//Gizmos.color = Color.yellow;
			//Gizmos.DrawSphere(p, 0.1F);

			//Vector3 mousePos = Camera.ScreenToWorldPoint (Input.mousePosition);
			//Debug.Log(mousePos);
			if(((Time.time*1000)-timeSinceLastBoom) >= 1500){
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
					startDir = new Vector3(0, 0, boomnanaRange); 
					break;
				case "NE":
					startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z+10);
					startDir = new Vector3((float)(Math.Cos (45)*boomnanaRange), 0, (float)(Math.Cos (45)*boomnanaRange));
					break;
				case "E":
					startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z);
					startDir = new Vector3(boomnanaRange, 0, 0);
					break;
				case "SE":
					startPos = new Vector3(transform.position.x+10, transform.position.y, transform.position.z-10);
					startDir = new Vector3((float)(Math.Cos(45)*boomnanaRange), 0,(float)(Math.Cos (45)*-boomnanaRange));
					break;
				case "S":
					startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z-10);
					startDir = new Vector3(0, 0, -boomnanaRange);
					break;
				case "SW":
					startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z-10);
					startDir = new Vector3((float)(Math.Cos (45)*-boomnanaRange), 0, (float)(Math.Cos (45)*-boomnanaRange));
					break;
				case "W":
					startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z);
					startDir = new Vector3(-boomnanaRange, 0, 0);
					break;
				case "NW":
					startPos = new Vector3(transform.position.x-10, transform.position.y, transform.position.z+10);
					startDir = new Vector3((float)(Math.Cos (45)*-boomnanaRange), 0, (float)(Math.Cos (45)*boomnanaRange));
					break;
				}
				Vector3 dir = new Vector3(p.x - startPos.x, 0, p.z - startPos.z);
				boomscript.spawn(this.gameObject, boom,  startPos
				                 , startDir);
				timeSinceLastBoom = Time.time * 1000;
				soundPlayer.PlayOneShot(boomnanathrowclip);
			}
			//boomscript.
		}

		if (Input.GetKey (moveUp) && !stunned) {
			up = true;
			if (Input.GetKey (sprint)) {
				position.z += sprintspeed;
			} else { 
				position.z += ms;
			}
			moving = true;
			//StateController.isMoving = true;
		//	changed = true;
		}

		if (Input.GetKey (moveDown) && !stunned) {
			down = true;
			if(Input.GetKey(sprint)){
				position.z -= sprintspeed;
			}else{
				position.z -= ms;
			}
			moving = true;
			//StateController.isMoving = true;
			//changed = true;
		}

		if (Input.GetKey (moveRight) && !stunned) {
			right = true;
			if(Input.GetKey (sprint)){
				position.x += sprintspeed;
			}else{
				position.x += ms;
			}
			moving = true;
			//StateController.isMoving = true;
			//changed = true;
		}
		if (Input.GetKey (moveLeft) && !stunned) {
			left = true;
			if(Input.GetKey (sprint)){
				position.x -= sprintspeed;
			}else{
				position.x -= ms;
			}
			moving = true;
			//StateController.isMoving = true;
			//changed = true;
		}
		if (Input.GetKeyDown (KeyCode.Space) && !jumping && !stunned) {
			jump ();
			//Plays Jumping sound.
			// GameObject.Find("SoundMgr");
			jumpPlayer.PlayOneShot(jumpclip);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			zoom -= 20.0f;
			
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			zoom += 20.0f;
		}

		if (!Input.GetKey (moveLeft) && !Input.GetKey (moveRight) && !Input.GetKey (moveUp) && !Input.GetKey (moveDown)) {
			moving = false;
			//StateController.isMoving = false;
		}
		if (moving && !movementPlayer.isPlaying) {
			movementPlayer.clip = movementclip;
			movementPlayer.Play();
		}if(!moving && movementPlayer.clip == movementclip && movementPlayer.isPlaying){
			movementPlayer.Stop();
		}
		if (stunned){// && (Time.time - stunnedStart)<= stunnedTimer) {
			if((Time.time - stunnedStart)<= stunnedTimer && !stunnedPlayer.isPlaying){//if(!stunnedPlayer.isPlaying){
				stunnedPlayer.clip = stunnedclip;
				stunnedPlayer.Play ();
			}else if(Time.time - stunnedStart >= stunnedTimer){
				stunned = false;
			}

		}if (!stunned && stunnedPlayer.clip == stunnedclip) {
			stunnedPlayer.Stop ();
			//stunned = false;
		}
		//if(statecontroller.isMoving){
		//soundPlayer.clip = movementclip;
		//soundPlayer.Play ();
		//		}else{soundPlayer.Stop();}

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
		//Debug.Log (coll.gameObject.name);
		if (coll.gameObject.name.Equals ("Terrain")) { 
			jumping = false;
		}
		if (Input.GetKey(KeyCode.E)) {

			if (coll.gameObject.name.Equals ("Coconut")) {
				//Debug.Log("E is pressed");
				nut = coll.gameObject.GetComponent<Coconut>();
				if(!nut.isHeldAtm()){
				   nut.setCapture(this.gameObject);
					// Debug.Log
				}
			}

		}
		if (coll.gameObject.name.Equals ("Boomnana(Clone)")) {
			//Debug.Log("Hit with BoomNana");
			if(coll.gameObject.GetComponent<Boomnana>().owner == this.gameObject){
				stunned = true;
				stunnedStart = Time.time;
			}
		}
		if(Input.GetKey(KeyCode.Q)){
			//Debug.Log("q pressed");
			if(nut.getHolder() != null){
				//Debug.Log("Has Holder");
				if(nut.getHolder().Equals(this.gameObject)){
					nut.removeCapture();
					//Debug.Log("Holder Removed");
				}
			}
		}

		//position.y = coll.gameObject.transform.position.y;
		//transform.position = position;
	}
}
