﻿using UnityEngine;
using System.Collections;
using System;

public class TestPlayerBehaviour : Bolt.EntityEventListener<ITestPlayerState>
{
	GameObject player;
	WASD wasd;
	StateController sc;
	PlayerStats ps;
	SoundController sound;
	
	public KeyCode moveUp = KeyCode.W;// = KeyCode.W;
	public KeyCode moveDown = KeyCode.S;// = KeyCode.S;
	public KeyCode moveRight = KeyCode.D;
	public KeyCode moveLeft = KeyCode.A;
	public KeyCode tailSlapKey;
	public KeyCode boomNanaKey = KeyCode.Alpha2;
	public KeyCode ccKey;
	public KeyCode cprKey;
	public KeyCode vomitKey;
	public KeyCode buffKey = KeyCode.R;
	
	public GameObject mainCam;
	public GameObject snow;
	
	float timeSinceLastBoom;
	float currentRotationFace;
	float zoom = 200.0f;
	Vector3 gravity;
	
	string currRotStr = "N";
	int startup = 0;
	
	Vector3 camPos;
	KeyCode sprint = KeyCode.LeftShift;

		public override void Attached ()
		{
				state.TestPlayerTransform.SetTransforms (transform);
		
				if (entity.isOwner) {
						state.TestPlayerColor = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

				}  
		
				state.AddCallback ("TestPlayerColor", ColorChanged);

		}
		public override void SimulateOwner (){
			if (startup == 0) {
				Start ();
			}	
			startup = 1;
		}

	

		// Use this for initialization
		void Start () {
			GameObject snowInit = Instantiate(snow) as GameObject; 
			Destroy (snow);
			snow = snowInit;
			Vector3 snowpos = new Vector3 (this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z);
		    snow.transform.position = snowpos;
			snow.transform.Rotate (new Vector3 (90,0,0));
			player = this.gameObject;
		    GameObject camObj = Instantiate (mainCam) as GameObject;// this.mainCam.gameObject;
			//GameObject camObj = Instantiate(Resources.Load("Prefabs/PlayerCam", typeof(GameObject)) as GameObject,
			                              //   new Vector3(1000, 200,1000), Quaternion.identity) as GameObject;
			Destroy(mainCam);
			mainCam = camObj ;//this.transform.Find("PlayerCam").camera; //camObj.camera;
			Vector3 newPos = new Vector3 (transform.position.x,200,transform.position.z - 50);
			mainCam.transform.position = newPos;
			mainCam.transform.LookAt (this.transform.position);
			//Debug.Log ("player = "+player); 
			timeSinceLastBoom = Time.time;
			wasd = gameObject.GetComponent<WASD>(); 
			sc = gameObject.GetComponent<StateController> ();
			ps = gameObject.GetComponent<PlayerStats> ();
			sound = gameObject.GetComponent<SoundController> ();
		    mainCam.camera.enabled = true;
		    mainCam.camera.gameObject.SetActive (true);
		//Destroy (camObj);
		}
		
		void checkCameraAngle (){
			mainCam.gameObject.transform.position = transform.position;
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
		}
		
		// Update is called once per frame
		void Update () { 
			bool up, down, left, right;
			up = false;
			down = false;
			left = false;
			right = false;
		Vector3 snowPos = new Vector3 (player.transform.position.x, 250, player.transform.position.z);
		snow.transform.position = snowPos;

		//snow.transform.rotation = new Quaternion(0, 0, 90, 0);
			Vector3 position = player.transform.position;  
			if(Input.GetKeyDown(boomNanaKey)){ // Mouse0 = Left Click
				//Debug.Log("Player pos: "+transform.position);
				//Camera cam = Camera.main;//.Find("Main Camera");
				//Debug.Log(cam.name);
				//Vector3 p = cam.ScreenToWorldPoint(new Vector3(100, 100, cam.nearClipPlane));
				//Debug.Log("Projectile dir: "+p);
				//Debug.Log(p);
				//Gizmos.color = Color.yellow;
				//Gizmos.DrawSphere(p, 0.1F);
				
				//Vector3 mousePos = Camera.ScreenToWorldPoint (Input.mousePosition);
				//Debug.Log(mousePos);
				if(((Time.time*1000)-timeSinceLastBoom) >= 1500){
					GameObject boom = Instantiate(Resources.Load("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
					                              new Vector3(player.transform.position.x + 20, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
					Boomnana boomscript = boom.GetComponent<Boomnana>();
					// set position, add velocity.
					// if return after x sec, unless OnCollision triggers.
					Vector3 startPos = new Vector3();
					Vector3 startDir = new Vector3();
					switch(currRotStr){
					case "N": // Angles might be oposite....
						startPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+10);
						startDir = new Vector3(0, 0, ps.boomnanaRange); 
						break;
					case "NE":
						startPos = new Vector3(player.transform.position.x+10, player.transform.position.y, player.transform.position.z+10);
						startDir = new Vector3((float)(Math.Cos (45)*ps.boomnanaRange), 0, (float)(Math.Cos (45)*ps.boomnanaRange));
						break;
					case "E":
						startPos = new Vector3(player.transform.position.x+10, player.transform.position.y, player.transform.position.z);
						startDir = new Vector3(ps.boomnanaRange, 0, 0);
						break;
					case "SE":
						startPos = new Vector3(player.transform.position.x+10, player.transform.position.y, player.transform.position.z-10);
						startDir = new Vector3((float)(Math.Cos(45)*ps.boomnanaRange), 0,(float)(Math.Cos (45)*-ps.boomnanaRange));
						break;
					case "S":
						startPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z-10);
						startDir = new Vector3(0, 0, -ps.boomnanaRange);
						break;
					case "SW":
						startPos = new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z-10);
						startDir = new Vector3((float)(Math.Cos (45)*-ps.boomnanaRange), 0, (float)(Math.Cos (45)*-ps.boomnanaRange));
						break;
					case "W":
						startPos = new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z);
						startDir = new Vector3(-ps.boomnanaRange, 0, 0);
						break;
					case "NW":
						startPos = new Vector3(player.transform.position.x-10, player.transform.position.y, player.transform.position.z+10);
						startDir = new Vector3((float)(Math.Cos (45)*-ps.boomnanaRange), 0, (float)(Math.Cos (45)*ps.boomnanaRange));
						break;
					}
					sc.initiateCombat();
					//Vector3 dir = new Vector3(p.x - startPos.x, 0, p.z - startPos.z);
					boomscript.spawn(this.gameObject, boom,  startPos
					                 , startDir);
					timeSinceLastBoom = Time.time * 1000;
					sound.getSoundPlayer().PlayOneShot(sound.boomnanathrowclip);
				}
				//boomscript.
			}
			
			if (Input.GetKey (moveUp) && !sc.isStunned) {
				up = true;
				if (Input.GetKey (sprint)) {
					position.z += sc.getSpeed();
				} else { 
					position.z += sc.getSpeed();
				}
				sc.isMoving = true;
			}
			
			if (Input.GetKey (moveDown) && !sc.isStunned) {
				down = true;
				if(Input.GetKey(sprint)){
					position.z -= sc.getSpeed();
				}else{
					position.z -= sc.getSpeed();
				}
				sc.isMoving = true;
			}
			
			if (Input.GetKey (moveRight) && !sc.isStunned) {
				right = true;
				if(Input.GetKey (sprint)){
					position.x += sc.getSpeed();
				}else{
					position.x += sc.getSpeed();
				}
				sc.isMoving = true;
			}
			if (Input.GetKey (moveLeft) && !sc.isStunned) {
				left = true;
				if(Input.GetKey (sprint)){
					position.x -= sc.getSpeed();
				}else{
					position.x -= sc.getSpeed();
				}
				sc.isMoving = true;
			}
			if(Input.GetKeyDown(buffKey) && !sc.isBuffed && sc.isAbleToBuff()){
				buff();
			}
			if (Input.GetKeyDown (KeyCode.Space) && !sc.isJumping && !sc.isStunned) {
				jump ();
				sound.getJumpPlayer().PlayOneShot(sound.jumpclip);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				zoom -= 20.0f;
				
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				zoom += 20.0f;
			}
			
			if (!Input.GetKey (moveLeft) && !Input.GetKey (moveRight) && !Input.GetKey (moveUp) && !Input.GetKey (moveDown)) {
				sc.isMoving = false;
			}
			if (sc.isMoving && !sound.getMovementPlayer().isPlaying) {
				sound.getMovementPlayer().clip = sound.movementclip;
				sound.getMovementPlayer().Play();
			}if(!sc.isMoving && sound.getMovementPlayer().clip == sound.movementclip && sound.getMovementPlayer().isPlaying){
				sound.getMovementPlayer().Stop();
			}
			if (sc.isStunned){// && (Time.time - stunnedStart)<= stunnedTimer) {
			//Debug.Log(wasd.stunnedStart);
				if((Time.time - wasd.stunnedStart)<= sc.stunnedTimer && !sound.getStunnedPlayer().isPlaying){//if(!stunnedPlayer.isPlaying){
					sound.getStunnedPlayer().clip = sound.stunnedclip;
					sound.getStunnedPlayer().Play ();
				}else if(Time.time - wasd.stunnedStart >= sc.stunnedTimer){
					sc.isStunned = false;
				}
				
			}if (!sc.isStunned && sound.getStunnedPlayer().clip == sound.stunnedclip) {
				sound.getStunnedPlayer().Stop ();
			}
			player.transform.position = position;
			checkCameraAngle (); 
			setRotation (up, down, left, right);
			right = false;
			left = false;
			up = false;
			down = false;
			
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
		}
		
		void jump(){
			gravity.y = ps.jumpHeight;
			transform.rigidbody.velocity = gravity;
			sc.isJumping = true;
		}
		
		void buff (){
			sc.buff ();
		}

////		var ms = 4f;
////		var mpos = Vector3.zero;
////		if (Input.GetKey (KeyCode.W)) {
////			mpos.z += 1;
////		}
////		if (Input.GetKey (KeyCode.S)) {
////			mpos.z -= 1;
////		}
////		if (Input.GetKey (KeyCode.A)) {
////			mpos.x -= 1;
////			
////		}
////		if (Input.GetKey (KeyCode.D)) {
////			mpos.x += 1;
////		}
////		if (mpos != Vector3.zero) {
////			transform.position = transform.position + (mpos.normalized * ms * BoltNetwork.frameDeltaTime);
////		}

        
		
    
		public void ColorChanged ()
		{
				renderer.material.color = state.TestPlayerColor;
		}

		/*void OnGUI ()
		{
				if (entity.isOwner) {
						GUI.color = state.TestPlayerColor;
						GUILayout.Label ("@@@");
						GUI.color = Color.white;
				}
		}*/
}