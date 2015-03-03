using UnityEngine;
using System.Collections;
using System;

public class WASD : MonoBehaviour
{

		/*TODO: Make state controller, so you don't connect/split during combat.
	 * 
	 * */
		//Bolt.EntityEventListener<ICoconutState> state;
		BoltEntity entity;
		BoltEntity me;
		public float stunnedStart;
		GameObject coconut;
		Coconut nut;
		StateController sc;
		PlayerStats ps;
		int nutId;
		// Use this for initialization
		void Start ()
		{
				
				//coconut = BoltNetwork.Attach (BoltPrefabs.Coconut_1) as GameObject;
				//GameObject.Find ("Coconut 1(Clone)");
				//coconut = GameObject.Find ("Coconut 1(Clone)");
				//nut = coconut.GetComponent<Coconut> ();
				//Debug.Log ("Coconut: " + coconut + ", nutScript: " + nut);
				sc = this.gameObject.GetComponent<StateController> ();
				ps = this.gameObject.GetComponent<PlayerStats> ();
				nutId = (GameObject.FindWithTag ("nut")as GameObject).GetInstanceID ();
				coconut = GameObject.FindWithTag ("nut") as GameObject;
				
				

		}

		// Update is called once per frame
		void Update ()
		{
				if (nut == null) {
						//nutId = (GameObject.FindWithTag ("nut")as GameObject).GetInstanceID ();
						nut = (GameObject.FindWithTag ("nut") as GameObject).GetComponent<Coconut> ();
				}
				if (coconut == null || nut == null) {
						try {
								coconut = GameObject.FindWithTag ("nut") as GameObject;

								nut = coconut.GetComponent<Coconut> ();

						} catch {
						}
				}
				if (sc.isHolding) {
						foreach (BoltEntity b in BoltNetwork.entities) {
								GameObject bGo = b.gameObject;
								if (bGo.tag == "player") {
										if (bGo.FindComponent<WASD> ().isOwnerOfNut ()) {
												//nut = b.gameObject.GetComponent<Coconut> ();
												Vector3 newPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
												bGo.gameObject.GetComponent<WASD> ().updateNutPosWhenHeld (newPos);
												//b.gameObject.GetComponent<WASD> ().setCapture (this.gameObject);
												//sc.isHolding = true;
										}
								}
				
						}
				}
		}

		void OnCollisionEnter (Collision coll)
		{ // Working!!
				if (coll.gameObject.name.Equals ("Terrain")) { 
						sc.isJumping = false;
				}

//				if (Input.GetKey (KeyCode.E)) {
//						if (coll.gameObject.Equals (GameObject.FindWithTag ("nut") as GameObject)) {
//								//Debug.Log (coll.gameObject.name); 
//								nut = coll.gameObject.GetComponent<Coconut> ();
//								if (!nut.isHeldAtm ()) {
//										nut.setCapture (this.gameObject);
//										sc.isHolding = true;
//										//Apply grabbing to nutId
//										//CoconutEvent.Create(
//										//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = true;
//										//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;
////										using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
////												if (evnt.CoconutId == nutId) {
////														evnt.isPickedUp = true;
////														evnt.CoconutPosition = transform.position;
////												}
////										}
//								}
//						}
//				}

				if (coll.gameObject.name.Equals ("Boomnana(Clone)")) {
						if (coll.gameObject.GetComponent<Boomnana> ().owner == this.gameObject) {
								sc.isStunned = true;
								stunnedStart = Time.time;
								Destroy (coll.gameObject); 
						}
				}

//				if (Input.GetKey (KeyCode.Q)) {
//						if (nut.getHolder () != null) {
//								if (nut.getHolder ().Equals (this.gameObject)) {
//										nut.removeCapture ();
//										sc.isHolding = false;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = false;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;
////										using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
////												evnt.isPickedUp = false;
////												evnt.CoconutPosition = transform.position;
////										}
//								}
//						}
//				}
		}

		public void updateNutPosWhenHeld (Vector3 pos)
		{
				this.nut.updateNutPositionRemote (pos);
		}

		public void updateNutPositionRemote (Vector3 newPos, GameObject go)
		{
				nut.updateNutPositionRemote (newPos);
				nut.setCapture (go);
		}
		public void removeNutCapture (Vector3 pos)
		{
				nut.removeCapture (pos);
		}

		public bool isOwnerOfNut ()
		{
				return (GameObject.FindWithTag ("nut") as GameObject).GetComponent<Coconut> ().entity.isOwner;
		}

		void OnTriggerStay (Collider coll)
		{
				if (Input.GetKeyDown (KeyCode.E)) {
						if (coll.gameObject.Equals (GameObject.FindWithTag ("nut") as GameObject)) {
								GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.TakeControl ();
								coll.GetComponent<Coconut> ().setCapture (this.gameObject);
//								foreach (BoltEntity b in BoltNetwork.entities) {
//										GameObject bGo = b.gameObject;
//										if (bGo.tag == "player") {
//												if (bGo.FindComponent<WASD> ().isOwnerOfNut ()) {
//														//nut = b.gameObject.GetComponent<Coconut> ();
//														Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
//														bGo.gameObject.GetComponent<WASD> ().updateNutPositionRemote (newPos, gameObject);
//														//b.gameObject.GetComponent<WASD> ().setCapture (this.gameObject);
//														sc.isHolding = true;
//												}
//										}
//										
//								}


//								nut = coll.gameObject.GetComponent<Coconut> ();
//								if (!nut.isHeldAtm ()) {
//										nut.setCapture (this.gameObject);
//										sc.isHolding = true;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = true;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;
////										using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
////												evnt.isPickedUp = true;
////												evnt.CoconutPosition = transform.position;
////										}
//								}
						}
				}

				if (Input.GetKeyDown (KeyCode.Q)) {
						if (coll.gameObject.Equals (GameObject.FindWithTag ("nut") as GameObject) && sc.isHolding) {
								Debug.Log ("Q pressed");
								//GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.TakeControl ();
								coll.GetComponent<Coconut> ().removeCapture (new Vector3 ());
								GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.ReleaseControl ();
								this.gameObject.GetComponent<WASD> ().entity.TakeControl ();
//								foreach (BoltEntity b in BoltNetwork.entities) {
//										GameObject bGo = b.gameObject;
//										if (bGo.tag == "player") {
//												if (bGo.FindComponent<WASD> ().isOwnerOfNut ()) {
//														//nut = b.GetComponent<Coconut> ();
//														Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
//														b.gameObject.GetComponent<WASD> ().removeNutCapture (newPos);
//														sc.isHolding = false;
//												}
//										}						
//								}
						}
				}
				
				// If attack (melee), deal damage to that enemy
				if (Input.GetKey (transform.gameObject.GetComponent<TestPlayerBehaviour> ().tailSlapKey)) {
						if (coll.gameObject.name.Equals ("PlayerObject3d")) {
								if (coll.gameObject.GetComponent<StateController> ().teamNumber != sc.teamNumber) {
										coll.gameObject.GetComponent<StateController> ().initiateCombat ();
										//If hit
										coll.gameObject.GetComponent<StateController> ().hp -= ps.tailSlapDamage;
										//ANIMATE TAILSLAP!
								}
						}
				}
		}


}




/*void jump(){
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

/*void keepSteady(){ 
		//rotation.y = transform.rotation.y;
		//transform.rotation = rotation;
		
	}*/


// INSIDE UPDATE
//////////////////////
/*bool up, down, left, right;
		up = false;
		down = false;
		left = false;
		right = false;

		keepSteady ();

		position = transform.position; 
		if(Input.GetKeyDown(KeyCode.Mouse0)){ // Mouse0 = Left Click
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
				//Vector3 dir = new Vector3(p.x - startPos.x, 0, p.z - startPos.z);
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

/*

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
		down = false;*/

//////////////


