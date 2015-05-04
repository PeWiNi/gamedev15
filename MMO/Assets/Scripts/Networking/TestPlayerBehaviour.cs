using UnityEngine;
using System.Collections;
using System;

public class TestPlayerBehaviour : Bolt.EntityBehaviour<ITestPlayerState>
{
	public GameObject player;
	WASD wasd;
	StateController sc;
	PlayerStats ps;
	SoundController sound;
	CoconutEffect cf;
	public int playerId;
	public KeyCode moveUp = KeyCode.W;// = KeyCode.W;
	public KeyCode moveDown = KeyCode.S;// = KeyCode.S;
	public KeyCode moveRight = KeyCode.D;
	public KeyCode moveLeft = KeyCode.A;

	public KeyCode tailSlapKey;
	public KeyCode boomNanaKey;

	private Animator anim;

	public KeyCode ccKey;
	public KeyCode cprKey;
	public KeyCode aoeKey;
	public KeyCode buffKey;
	bool up, down, left, right;
	Vector3 position;
	public GameObject mainCam;
	public GameObject snow;
	public GameObject coconut;
	HUDScript hud;
	public static Bolt.NetworkId playerNetworkId;
	float timeSinceLastBoom;
	float currentRotationFace;
	float zoom = 100.0f;
	Vector3 gravity;
	string currRotStr = "N";
	int startup = 0;
	Vector3 camPos;
	public float playerBuffDmg;
	public float tailSlapDmg;
	public float boomnanaDmg; 
	public float aoeDmg;
	public float ccDuration;
	public float buffedTailSlapDmg;
	public float buffedBoomnanaDmg;
	public float buffedAOEBuffDmg;
	public float buffedCcDuration;
	public float ccDurationFactor = 1.35f;
	public bool boomUsed = false;
	public bool BoomNanaUsedInHidingGrass;
	public bool isInsideHidingGrass;

	public Animation animation;
//
	private AnimationState idle;
	private AnimationState thefish;
	private AnimationState walk;
	private AnimationState death;
	private AnimationState buffAnim;
	private AnimationState jump1;
	private AnimationState jump2;
	private AnimationState tail;
	private AnimationState boom;
	private AnimationState puke_start;
	private AnimationState puke_end;
	private AnimationState fish;

	//KeyCode sprint = KeyCode.LeftShift;

	void Awake ()
	{
		Start ();
		anim = GetComponent<Animator> ();
		Cursor.visible = false;

                

		PlayerCam[] cams = FindObjectsOfType<PlayerCam> ();
		foreach (PlayerCam p in cams) {
			if (p.gameObject.GetComponent<PlayerCam> ()._target == this.gameObject) {
				mainCam = p.gameObject;
				break;
			}
		}

	}

	public override void Attached ()
	{
		state.TestPlayerTransform.SetTransforms (transform);
				
//				if (BoltInit.hasPickedTeamOne == true) {
//						state.TeamMemberId = 1;
//						Debug.Log (state.TeamMemberId.ToString ());
//				}
//				if (BoltInit.hasPickedTeamTwo == true) {
//						state.TeamMemberId = 2;
//						Debug.Log (state.TeamMemberId.ToString ());		
//				}
//				state.TeamMemberId = BoltInit.teamMemberId;
		//Debug.Log ("team" + BoltInit.teamMemberId);
		//state.TeamMemberId = gameObject.GetComponent<BoltInit> ().teamMemberId;
		/*
		if (entity.isOwner) {
			if (BoltInit.hasPickedTeamOne == true) {
				state.TeamMemberId = 1;
				state.TestPlayerColor = new Color (0, 1, 0, 1);
				Debug.Log ("Team nr." + state.TeamMemberId.ToString ());
			}
			if (BoltInit.hasPickedTeamTwo == true) {
				state.TeamMemberId = 2;
				state.TestPlayerColor = new Color (1, 0, 0, 1);
				Debug.Log ("Team nr." + state.TeamMemberId.ToString ());		
			}	
			//state.TestPlayerColor = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			//state.TeamMemberId = BoltInit.teamMemberId;
        } */
		if (entity.isOwner) {
			if (MenuScript.hasPickedTeamOne == true) {
				state.TeamMemberId = 1;
				state.TestPlayerColor = new Color (0, 1, 0, 1);
				Debug.Log ("Team nr." + state.TeamMemberId.ToString ());
			}
			if (MenuScript.hasPickedTeamTwo == true) {
				state.TeamMemberId = 2;
				state.TestPlayerColor = new Color (1, 0, 0, 1);
				Debug.Log ("Team nr." + state.TeamMemberId.ToString ());
			}
			//state.TestPlayerColor = new Color (UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
			//state.TeamMemberId = BoltInit.teamMemberId;
		}  
		state.AddCallback ("TestPlayerColor", ColorChanged);
		state.AddCallback ("TeamMemberId", TeamSelection);
		this.gameObject.GetComponent<PlayerStats> ().makeTheStatChange ();

	}

	public override void SimulateController ()
	{
//		if(animation.isPlaying){
//			animation.CrossFade("M_Idle", 1.0f);
//		}


		if (startup == 0) {
			Debug.Log ("Making the stat change");
			//this.gameObject.GetComponent<PlayerStats> ().makeTheStatChange ();
			mainCam.gameObject.GetComponent<PlayerCam> ().setStartLocation (transform.position);
		}
		startup = 1;
//				Vector3 snowPos = new Vector3 (player.transform.position.x, 250, player.transform.position.z);
//				snow.transform.position = snowPos;  
		if (wasd != null) {
		} 
		position = player.transform.position;

		if (Input.GetKeyDown (boomNanaKey)) {
			VFXScript vfx = gameObject.GetComponent<VFXScript> ();
			Transform aim = this.transform.GetChild (6);
			aim.GetComponent<Renderer> ().enabled = true;
			aim.localScale = new Vector3 (1f, 0, ps.boomnanaRange / 4);
			aim.localPosition = new Vector3 (0, 0, (ps.boomnanaRange / 2));
			aim.localEulerAngles = new Quaternion (90.0f, 0.0f, 0.0f, 0).eulerAngles;
			//vfx.aim.renderer.enabled = true;
			//aimOverlay(1, range, 0.5f);
		}
		if (Input.GetKeyUp (boomNanaKey) && !GetComponent<StateController>().isDead && !sc.isStunned && ! sc.isChanneling) {

			Debug.Log ("BOOOOMNANAAAAA");
			VFXScript vfx = gameObject.GetComponent<VFXScript> ();
			if (isInsideHidingGrass == true) {
				BoomNanaUsedInHidingGrass = true;
			}
			animation.wrapMode = WrapMode.Once;
			animation.Play ("M_BM");

			// Mouse0 = Left Click
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
			if (!boomUsed || ((Time.time * 1000) - timeSinceLastBoom) >= (ps.boomNanaCooldown * 1000) && !sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
				GameObject boom = Instantiate (Resources.Load ("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
				                               new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
				Boomnana boomscript = boom.GetComponent<Boomnana> ();
				boomUsed = true;
				// set position, add velocity.
				// if return after x sec, unless OnCollision triggers.
                             
				/*


								Vector3 startPos = new Vector3 ();
								Vector3 startDir = new Vector3 ();
								switch (currRotStr) {
								case "N": // Angles might be oposite....
										startPos = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 10);
										startDir = new Vector3 (0, 0, ps.boomnanaRange); 
										break;
								case "NE":
										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z + 10);
										startDir = new Vector3 ((float)(Math.Cos (45) * ps.boomnanaRange), 0, (float)(Math.Cos (45) * ps.boomnanaRange));
										break;
								case "E":
										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
										startDir = new Vector3 (ps.boomnanaRange, 0, 0);
										break;
								case "SE":
										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z - 10);
										startDir = new Vector3 ((float)(Math.Cos (45) * ps.boomnanaRange), 0, (float)(Math.Cos (45) * -ps.boomnanaRange));
										break;
								case "S":
										startPos = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
										startDir = new Vector3 (0, 0, -ps.boomnanaRange);
										break;
								case "SW":
										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z - 10);
										startDir = new Vector3 ((float)(Math.Cos (45) * -ps.boomnanaRange), 0, (float)(Math.Cos (45) * -ps.boomnanaRange));
										break;
								case "W":
										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z);
										startDir = new Vector3 (-ps.boomnanaRange, 0, 0);
										break;
								case "NW":
										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z + 10);
										startDir = new Vector3 ((float)(Math.Cos (45) * -ps.boomnanaRange), 0, (float)(Math.Cos (45) * ps.boomnanaRange));
										break;
								}*/
				sc.initiateCombat ();
				//Vector3 dir = new Vector3(p.x - startPos.x, 0, p.z - startPos.z);
				Vector3 shot = new Vector3 (transform.position.x, transform.position.y, gameObject.GetComponentInParent<PlayerStats> ().boomnanaRange + transform.position.z);
				Vector3 offset = transform.position - shot;
				Vector3 end = (transform.forward * gameObject.GetComponentInParent<PlayerStats> ().boomnanaRange);
				float desiredAngle = transform.eulerAngles.y;

				Debug.Log ("Angle = " + desiredAngle);

				Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
				//Vector3 retry = desiredAngle;

				Vector3 endPos = (transform.position) - (rotation * offset);
				Debug.Log ("EndPos = " + endPos.x + "," + endPos.y + "," + endPos.z);
				Debug.Log ("FIRING BOOMNANA FROM PLAYERBEHAVIOUR");  
				boomscript.spawn (this.gameObject, boom, transform.position
				                  ,/* startDir,*/endPos);
				timeSinceLastBoom = Time.time * 1000;
				sound.getSoundPlayer ().PlayOneShot (sound.boomnanathrowclip);
			}
			//boomscript.
		}
		if (Input.GetKey (moveUp) && !sc.isStunned && !sc.isDead) {
			up = true;
			if (sc.canMove) {
								
				//position = position + (transform.forward * sc.movementspeed * Time.deltaTime);
				//position.z += sc.getSpeed ();
								
				sc.isMoving = true;
			}
		}

		if (Input.GetKey (moveDown) && !sc.isStunned && !sc.isDead) {
			down = true;
			if (sc.canMove) {
								
				//position = position - (transform.forward * sc.movementspeed * Time.deltaTime);
				//position.z -= sc.getSpeed ();
								
				sc.isMoving = true;
			}
		}

		if (Input.GetKey (moveRight) && !sc.isStunned && !sc.isDead) {
			right = true;
			if (sc.canMove) {
								
				//position = position + (transform.right * sc.movementspeed * Time.deltaTime);
				//position.x += sc.getSpeed ();
								
				sc.isMoving = true;
			}
						
		}
		if (Input.GetKey (moveLeft) && !sc.isStunned && !sc.isDead) {
			left = true;
			if (sc.canMove) {
								
				//position = position - (transform.right * sc.movementspeed * Time.deltaTime);
				//position.x -= sc.getSpeed ();
								
				sc.isMoving = true;
			}
		}


		if(up){
			if (sc.canMove) {
				if(right){
					position = position + (transform.forward * sc.movementspeed * Time.deltaTime)/2;
					position = position + (transform.right * sc.movementspeed * Time.deltaTime)/2;
				}else if(left){
					position = position + (transform.forward * sc.movementspeed * Time.deltaTime)/2;
					position = position - (transform.right * sc.movementspeed * Time.deltaTime)/2;
				}else{
					position = position + (transform.forward * sc.movementspeed * Time.deltaTime);
				}
			}
		}else 
		if(down){
			if (sc.canMove) {
				if(right){
					position = position - (transform.forward * sc.movementspeed * Time.deltaTime)/2;
					position = position + (transform.right * sc.movementspeed * Time.deltaTime)/2;
				}else if(left){
					position = position - (transform.forward * sc.movementspeed * Time.deltaTime)/2;
					position = position - (transform.right * sc.movementspeed * Time.deltaTime)/2;
				}else{
					position = position - (transform.forward * sc.movementspeed * Time.deltaTime);
				}
			}
		}else 
		if(right){position = position + (transform.right * sc.movementspeed * Time.deltaTime);}
		else if(left){position = position - (transform.right * sc.movementspeed * Time.deltaTime);}

		if (position != Vector3.zero) {
			transform.position = transform.position + (position.normalized * sc.getSpeed () * BoltNetwork.frameDeltaTime);
		}
//		if (Input.GetKeyDown (buffKey) && !sc.isBuffed && sc.isAbleToBuff () && !sc.isDead) {
//			buff ();
//		}
		if (Input.GetKeyDown (KeyCode.Space) && !sc.isJumping && !sc.isStunned && !sc.isDead) {
			jump ();
			sound.getJumpPlayer ().PlayOneShot (sound.jumpclip);
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
		if (sc.isMoving) {
			animation.PlayQueued ("M_Walk");
			//animation.wrapMode = WrapMode.Loop;
		}
		if (!sc.isMoving && animation.IsPlaying ("M_Walk")) {
			animation.wrapMode = WrapMode.Once;
			animation.Play ("M_Idle");
		}
//		if(!sc.isChanneling && animation.IsPlaying("M_BP_Start")){
//			animation.wrapMode = WrapMode.Once;
//			animation.Play("M_BP_End");
//			animation.CrossFadeQueued("M_Idle", 0.2f, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
//		}

		if (sc.isMoving && !sound.getMovementPlayer ().isPlaying) {
			sound.getMovementPlayer ().clip = sound.movementclip;
			sound.getMovementPlayer ().Play ();
		}
		if (!sc.isMoving && sound.getMovementPlayer ().clip == sound.movementclip && sound.getMovementPlayer ().isPlaying) {
			sound.getMovementPlayer ().Stop ();
		}
		if (sc.isStunned) {// && (Time.time - stunnedStart)<= stunnedTimer) {
			//Debug.Log(wasd.stunnedStart);
			if ((Time.time - wasd.stunnedStart) <= sc.stunnedTimer && !sound.getStunnedPlayer ().isPlaying) {//if(!stunnedPlayer.isPlaying){
				sound.getStunnedPlayer ().clip = sound.stunnedclip;
				sound.getStunnedPlayer ().Play ();
			} else if (Time.time - wasd.stunnedStart >= sc.stunnedTimer) {
				sc.isStunned = false;
			}
			
		}
		if (!sc.isStunned && sound.getStunnedPlayer ().clip == sound.stunnedclip) {
			sound.getStunnedPlayer ().Stop ();
		}
		player.transform.position = position;
		//checkCameraAngle (); 
		//setRotation (up, down, left, right);
		right = false;
		left = false;
		up = false;
		down = false;

		if (this.gameObject.GetComponent<PlayerStats> ().IsInCoconutArea == true) {
			if (Input.GetKeyDown (KeyCode.T) && !sc.isStunned && !sc.isDead) {
				if (this.gameObject.GetComponent<PlayerStats> ().stoppedInCoconutConsume == false) {
					consumeCoconut ();
				}
			}
		}
		if (this.gameObject.GetComponent<PlayerStats> ().hasCoconutEffect == true) {
			if (this.gameObject.GetComponent<PlayerStats> ().canPickUpCoconut == true) {
				IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						// Create Event and use the be, if it is the one that is colliding.
						//CoconutEffect buff the player
						if (be.gameObject == this.gameObject) {
							using (var evnt = PlayerBeingBuffedEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
								evnt.CanPickUpCoconut = false;
							}
						}
					}
				
				}
			}
			if (Time.time - this.gameObject.GetComponent<PlayerStats> ().coconutEffectDuration >= this.gameObject.GetComponent<StateController> ().buffedPlayerDuration) {
				Debug.Log (Time.time);
				coconutEffectExpire ();
			}	
			if (sc.isDead) {
				coconutEffectExpire ();
			}
		}
		//				if (startup == 0) {
//						Start ();
//				}	
//				startup = 1;

//		if (Input.GetKeyDown (keyCpr)) {
//
//		}
		if (MenuScript.keybindChanged == true) {
			KeyCode[] bindings = MenuScript.KeyBindings;
			tailSlapKey = bindings [0];
			boomNanaKey = bindings [1];
			aoeKey = bindings [2];
			ccKey = bindings [3];
			cprKey = bindings [4];
			MenuScript.keybindChanged = false;
		}
	}

	void animateMovement ()
	{
		if (sc.isMoving) {
			animation ["M_Walk"].wrapMode = WrapMode.Loop;
			animation ["M_Walk"].layer = 1;
			animation.Play ("M_Walk");
		}
	}

	// Use this for initialization
	void Start ()
	{

		player = this.gameObject;
//				GameObject camObj = Instantiate (mainCam) as GameObject;// this.mainCam.gameObject;
		//GameObject camObj = Instantiate(Resources.Load("Prefabs/PlayerCam", typeof(GameObject)) as GameObject,
		//   new Vector3(1000, 200,1000), Quaternion.identity) as GameObject;
//				Destroy (mainCam);
//				mainCam = camObj;//this.transform.Find("PlayerCam").camera; //camObj.camera;
//				Vector3 newPos = new Vector3 (transform.position.x, 200, transform.position.z - 50);
//				mainCam.transform.position = newPos;
//				mainCam.transform.LookAt (this.transform.position);
		//Debug.Log ("player = "+player); 
		timeSinceLastBoom = Time.time;
		wasd = gameObject.GetComponent<WASD> (); 
		sc = gameObject.GetComponent<StateController> ();
		ps = gameObject.GetComponent<PlayerStats> ();
		sound = gameObject.GetComponent<SoundController> ();
		playerId = gameObject.GetInstanceID ();
		coconut = GameObject.FindGameObjectWithTag ("nut");


		/*
		childObject = Camera.main.gameObject;
		childAnim = childObject.animation;
		chMotor = GetComponent(CharacterMotor);
		ch = GetComponent(CharacterController);
		height = ch.height;
		h = height;
		speed = walkSpeed;
	}
	
	function Update()
	{
		if(childAnim.GetClip("fpsrun") && childAnim.GetClip("fpswalk"))
		{
			if (ch.isGrounded && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				speed = runSpeed;
				childObject.animation.CrossFade("fpsrun", 0.25f);
			}
			
			else
				childObject.animation.CrossFade("fpswalk", 0.25f);    
		}
		*/
		

		//ANIMATIONS
		animation = this.gameObject.GetComponent<Animation> ();
		animation.wrapMode = WrapMode.Loop;

		walk = animation ["M_Walk"];
		death = animation ["M_Death"];
		buffAnim = animation ["M_Buff"];
		jump1 = animation ["M_Jump1"];
		jump2 = animation ["M_Jump2"];
		tail = animation ["M_TS"];
		boom = animation ["M_BM"];
		puke_start = animation ["M_BP_Start"];
		puke_end = animation ["M_BP_End"];
		fish = animation ["M_FS"];

//		AnimationClip ac = animation.GetClip("M_Death");
//		Debug.Log(ac.name);
		//animation.CrossFade("M_Death",0.25f);
		//idle = animation["M_Death"];
		//animation.Play("M_Death");
		animation ["M_Walk"].layer = 0;
		animation ["M_Death"].wrapMode = WrapMode.Once;
		animation ["M_Death"].layer = 1;
		animation ["M_Jump1"].wrapMode = WrapMode.Once;
		animation ["M_Jump1"].speed = 2;
		animation ["M_Jump1"].layer = 1;
		animation ["M_Jump2"].wrapMode = WrapMode.Once;
		animation ["M_Jump2"].speed = 2;
		animation ["M_Jump2"].layer = 1;
		animation ["M_TS"].wrapMode = WrapMode.Once;
		animation ["M_TS"].speed = 2;
		animation ["M_TS"].layer = 1;
		animation ["M_BM"].wrapMode = WrapMode.Once;
		animation ["M_BM"].speed = 3;
		animation ["M_BM"].layer = 1;
		animation ["M_FS"].wrapMode = WrapMode.Once;
		animation ["M_FS"].speed = 1;
		animation ["M_FS"].layer = 1;
		animation ["M_BP_Start"].wrapMode = WrapMode.Once;
		animation ["M_BP_Start"].speed = 1;
		animation ["M_BP_Start"].layer = 1;
		animation ["M_BP_End"].wrapMode = WrapMode.Once;
		animation ["M_BP_End"].speed = 1;
		animation ["M_BP_End"].layer = 1;






		//				mainCam.camera.enabled = true;
//				mainCam.camera.gameObject.SetActive (true);
		//Destroy (camObj);
		KeyCode[] bindings = MenuScript.KeyBindings;
		tailSlapKey = bindings [0];
		boomNanaKey = bindings [1];
		aoeKey = bindings [2];
		ccKey = bindings [3];
		cprKey = bindings [4];
	}
		
	/*void checkCameraAngle ()
		{
				mainCam.gameObject.transform.position = transform.position;
				camPos.z = transform.position.z - 50; 
				camPos.x = transform.position.x;
				if (zoom < 50.0f) {
						zoom = 50.0f;
				}
				if (zoom > 200.0f) {
						zoom = 200.0f;
				}
				camPos.y = transform.position.y + zoom;
			
				mainCam.gameObject.transform.position = camPos;
				mainCam.gameObject.transform.LookAt (transform.position);
		}*/
		
//		// Update is called once per frame
//		void Update ()
//		{ 
//				Vector3 snowPos = new Vector3 (player.transform.position.x, 250, player.transform.position.z);
//				snow.transform.position = snowPos;
//				position = player.transform.position;
//				//snow.transform.rotation = new Quaternion(0, 0, 90, 0);
//				//Vector3 position = player.transform.position;  
//				if (Input.GetKeyDown (boomNanaKey)) { // Mouse0 = Left Click
//						//Debug.Log("Player pos: "+transform.position);
//						//Camera cam = Camera.main;//.Find("Main Camera");
//						//Debug.Log(cam.name);
//						//Vector3 p = cam.ScreenToWorldPoint(new Vector3(100, 100, cam.nearClipPlane));
//						//Debug.Log("Projectile dir: "+p);
//						//Debug.Log(p);
//						//Gizmos.color = Color.yellow;
//						//Gizmos.DrawSphere(p, 0.1F);
//				
//						//Vector3 mousePos = Camera.ScreenToWorldPoint (Input.mousePosition);
//						//Debug.Log(mousePos);
//						if (((Time.time * 1000) - timeSinceLastBoom) >= 1500) {
//								GameObject boom = Instantiate (Resources.Load ("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
//					                              new Vector3 (player.transform.position.x + 20, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
//								Boomnana boomscript = boom.GetComponent<Boomnana> ();
//								// set position, add velocity.
//								// if return after x sec, unless OnCollision triggers.
//								Vector3 startPos = new Vector3 ();
//								Vector3 startDir = new Vector3 ();
//								switch (currRotStr) {
//								case "N": // Angles might be oposite....
//										startPos = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 10);
//										startDir = new Vector3 (0, 0, ps.boomnanaRange); 
//										break;
//								case "NE":
//										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z + 10);
//										startDir = new Vector3 ((float)(Math.Cos (45) * ps.boomnanaRange), 0, (float)(Math.Cos (45) * ps.boomnanaRange));
//										break;
//								case "E":
//										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
//										startDir = new Vector3 (ps.boomnanaRange, 0, 0);
//										break;
//								case "SE":
//										startPos = new Vector3 (player.transform.position.x + 10, player.transform.position.y, player.transform.position.z - 10);
//										startDir = new Vector3 ((float)(Math.Cos (45) * ps.boomnanaRange), 0, (float)(Math.Cos (45) * -ps.boomnanaRange));
//										break;
//								case "S":
//										startPos = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
//										startDir = new Vector3 (0, 0, -ps.boomnanaRange);
//										break;
//								case "SW":
//										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z - 10);
//										startDir = new Vector3 ((float)(Math.Cos (45) * -ps.boomnanaRange), 0, (float)(Math.Cos (45) * -ps.boomnanaRange));
//										break;
//								case "W":
//										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z);
//										startDir = new Vector3 (-ps.boomnanaRange, 0, 0);
//										break;
//								case "NW":
//										startPos = new Vector3 (player.transform.position.x - 10, player.transform.position.y, player.transform.position.z + 10);
//										startDir = new Vector3 ((float)(Math.Cos (45) * -ps.boomnanaRange), 0, (float)(Math.Cos (45) * ps.boomnanaRange));
//										break;
//								}
//								sc.initiateCombat ();
//								//Vector3 dir = new Vector3(p.x - startPos.x, 0, p.z - startPos.z);
//								boomscript.spawn (this.gameObject, boom, startPos
//					                 , startDir);
//								timeSinceLastBoom = Time.time * 1000;
//								sound.getSoundPlayer ().PlayOneShot (sound.boomnanathrowclip);
//						}
//						//boomscript.
//				}
//				//movementInput ();
////				if (Input.GetKey (moveUp) && !sc.isStunned) {
////						up = true;
////						if (Input.GetKey (sprint)) {
////								position.z += sc.getSpeed ();
////						} else { 
////								position.z += sc.getSpeed ();
////						}
////						sc.isMoving = true;
////				}
////			
////				if (Input.GetKey (moveDown) && !sc.isStunned) {
////						down = true;
////						if (Input.GetKey (sprint)) {
////								position.z -= sc.getSpeed ();
////						} else {
////								position.z -= sc.getSpeed ();
////						}
////						sc.isMoving = true;
////				}
////			
////				if (Input.GetKey (moveRight) && !sc.isStunned) {
////						right = true;
////						if (Input.GetKey (sprint)) {
////								position.x += sc.getSpeed ();
////						} else {
////								position.x += sc.getSpeed ();
////						}
////						sc.isMoving = true;
////				}
////				if (Input.GetKey (moveLeft) && !sc.isStunned) {
////						left = true;
////						if (Input.GetKey (sprint)) {
////								position.x -= sc.getSpeed ();
////						} else {
////								position.x -= sc.getSpeed ();
////						}
////						sc.isMoving = true;
////				}
//				if (Input.GetKeyDown (buffKey) && !sc.isBuffed && sc.isAbleToBuff ()) {
//						buff ();
//				}
//				if (Input.GetKeyDown (KeyCode.Space) && !sc.isJumping && !sc.isStunned) {
//						jump ();
//						sound.getJumpPlayer ().PlayOneShot (sound.jumpclip);
//				}
//				if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
//						zoom -= 20.0f;
//				
//				}
//				if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
//						zoom += 20.0f;
//				}
//			
//				if (!Input.GetKey (moveLeft) && !Input.GetKey (moveRight) && !Input.GetKey (moveUp) && !Input.GetKey (moveDown)) {
//						sc.isMoving = false;
//				}
//				if (sc.isMoving && !sound.getMovementPlayer ().isPlaying) {
//						sound.getMovementPlayer ().clip = sound.movementclip;
//						sound.getMovementPlayer ().Play ();
//				}
//				if (!sc.isMoving && sound.getMovementPlayer ().clip == sound.movementclip && sound.getMovementPlayer ().isPlaying) {
//						sound.getMovementPlayer ().Stop ();
//				}
//				if (sc.isStunned) {// && (Time.time - stunnedStart)<= stunnedTimer) {
//						//Debug.Log(wasd.stunnedStart);
//						if ((Time.time - wasd.stunnedStart) <= sc.stunnedTimer && !sound.getStunnedPlayer ().isPlaying) {//if(!stunnedPlayer.isPlaying){
//								sound.getStunnedPlayer ().clip = sound.stunnedclip;
//								sound.getStunnedPlayer ().Play ();
//						} else if (Time.time - wasd.stunnedStart >= sc.stunnedTimer) {
//								sc.isStunned = false;
//						}
//				
//				}
//				if (!sc.isStunned && sound.getStunnedPlayer ().clip == sound.stunnedclip) {
//						sound.getStunnedPlayer ().Stop ();
//				}
//				player.transform.position = position;
//				checkCameraAngle (); 
//				setRotation (up, down, left, right);
//				right = false;
//				left = false;
//				up = false;
//				down = false;
//			
//		}

//		void movementInput ()
//		{
//				up = false;
//				down = false;
//				left = false;
//				right = false;
//				position = player.transform.position;
//				if (Input.GetKey (moveUp) && !sc.isStunned) {
//						up = true;
//						if (Input.GetKey (sprint)) {
//								position.z += sc.getSpeed ();
//						} else { 
//								position.z += sc.getSpeed ();
//						}
//						sc.isMoving = true;
//				}
//		
//				if (Input.GetKey (moveDown) && !sc.isStunned) {
//						down = true;
//						if (Input.GetKey (sprint)) {
//								position.z -= sc.getSpeed ();
//						} else {
//								position.z -= sc.getSpeed ();
//						}
//						sc.isMoving = true;
//				}
//		
//				if (Input.GetKey (moveRight) && !sc.isStunned) {
//						right = true;
//						if (Input.GetKey (sprint)) {
//								position.x += sc.getSpeed ();
//						} else {
//								position.x += sc.getSpeed ();
//						}
//						sc.isMoving = true;
//				}
//				if (Input.GetKey (moveLeft) && !sc.isStunned) {
//						left = true;
//						if (Input.GetKey (sprint)) {
//								position.x -= sc.getSpeed ();
//						} else {
//								position.x -= sc.getSpeed ();
//						}
//						sc.isMoving = true;
//				}
//		} 
	/*
		void setRotation (bool up, bool down, bool left, bool right)
		{
				if (up && right) {
						currentRotationFace = 45;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);
						currRotStr = "NE";
						//transform.rotation = Quaternion.AngleAxis(45 ,Vector3.up);//rotation;
				} else if (up && left) {
						currentRotationFace = 315;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "NW";
						//transform.rotation = Quaternion.AngleAxis(315 ,Vector3.up);//rotation;
				} else if (up) {
						currentRotationFace = 0;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "N";
						//transform.rotation = Quaternion.AngleAxis(0 ,Vector3.up);//rotation;
				} else if (down && left) {
						currentRotationFace = 225;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "SW";
						//transform.rotation = Quaternion.AngleAxis(225 ,Vector3.up);//rotation;
				} else if (down && right) {
						currentRotationFace = 135;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "SE";
						//transform.rotation = Quaternion.AngleAxis(135 ,Vector3.up);//rotation;
				} else if (down) {
						currentRotationFace = 180;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "S";
						//transform.rotation = Quaternion.AngleAxis(180 ,Vector3.up);//rotation;
				} else if (right) {
						currentRotationFace = 90;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "E";
						//transform.rotation = Quaternion.AngleAxis(90 ,Vector3.up);//rotation;
				} else if (left) {
						currentRotationFace = 270;
						Quaternion rot = new Quaternion (0, 360 - currentRotationFace, 0, 1);
						gameObject.GetComponentInChildren<Canvas> ().transform.rotation = rot;// new Quaternion (0, 315, 0, Quaternion.identity);

						currRotStr = "W";
						//transform.rotation = Quaternion.AngleAxis(270 ,Vector3.up);//rotation;
				}
			
				transform.rotation = Quaternion.AngleAxis (currentRotationFace, Vector3.up);//rotation;		
		}*/
		
	void jump ()
	{
		gravity.y = ps.jumpHeight;
		transform.GetComponent<Rigidbody> ().velocity = gravity;
		sc.isJumping = true;
		animation.wrapMode = WrapMode.Once;
		animation.CrossFade ("M_Jump2", 0.5f, PlayMode.StopAll);
		//animation.Play("M_Jump2");
	}
		
	void buff ()
	{
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

	public void TeamSelection ()
	{
		this.gameObject.GetComponent<PlayerStats> ().teamNumber = state.TeamMemberId; 
	}
    
	public void ColorChanged ()
	{
		//GetComponent<Renderer> ().material.color = state.TestPlayerColor;
	}

	public void consumeCoconut ()
	{
		playerDmgBuff ();
		playerCcDurationBuff ();
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				// Create Event and use the be, if it is the one that is colliding.
				//CoconutEffect buff the player
				if (be.gameObject == this.gameObject) {
					using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TargEnt = be;	
						evnt.isAffectedByCoconut = true;
						evnt.CoconutEffectDuration = Time.time;
						evnt.CoconutCCBuffDuration = buffedCcDuration;
						evnt.CoconutTailSlapDmgBuff = buffedTailSlapDmg;
						evnt.CoconutBoomnanaDmgBuff = buffedBoomnanaDmg;
						evnt.CoconutAOEDmgBuff = buffedAOEBuffDmg;
						evnt.StoppedInCoconutConsume = true;
					}
				}
			}
						
		}
	}

	public void coconutEffectExpire ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				// Create Event and use the be, if it is the one that is colliding.
				//CoconutEffect buff expire from the player
				if (be.gameObject == this.gameObject) {
					using (var evnt = PlayerBeingBuffedEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TargEnt = be;
						evnt.CanPickUpCoconut = true;
					}
					using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TargEnt = be;	
						evnt.isAffectedByCoconut = false;
						evnt.CoconutTailSlapDmgBuff = tailSlapDmg;
						evnt.CoconutBoomnanaDmgBuff = boomnanaDmg;
						evnt.CoconutAOEDmgBuff = aoeDmg;
						evnt.CoconutCCBuffDuration = ccDuration;
						evnt.StoppedInCoconutConsume = false;
					}
				}
			}
		}
	}

	public void breakOffTheCoconutChanneling ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				// Create Event and use the be, if it is the one that is colliding.
				if (be.gameObject == this.gameObject) {
					using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TargEnt = be;
						evnt.isAffectedByCoconut = false;
						evnt.CoconutTailSlapDmgBuff = tailSlapDmg;
						evnt.CoconutBoomnanaDmgBuff = boomnanaDmg;
						evnt.CoconutAOEDmgBuff = aoeDmg;
						evnt.CoconutCCBuffDuration = ccDuration;
						evnt.StoppedInCoconutConsume = false;
					}
				}
			}
		}
	}

	public void playerDmgBuff ()
	{
		playerBuffDmg = this.gameObject.GetComponent<PlayerStats> ().buffDamageFactor;
		tailSlapDmg = this.gameObject.GetComponent<PlayerStats> ().tailSlapDamage;
		boomnanaDmg = this.gameObject.GetComponent<PlayerStats> ().boomNanaDamage;
		aoeDmg = this.gameObject.GetComponent<PlayerStats> ().aoeTickDamageFactor;
		buffedTailSlapDmg = tailSlapDmg * playerBuffDmg;
		buffedBoomnanaDmg = boomnanaDmg * playerBuffDmg;
		buffedAOEBuffDmg = aoeDmg * playerBuffDmg;
	}

	public void playerCcDurationBuff ()
	{
		ccDuration = this.GetComponent<PlayerStats> ().ccDuration;
		buffedCcDuration = ccDuration * ccDurationFactor;
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
