using UnityEngine;
using System;
using System.Collections;

public class TutorialPlayerBehaviour : MonoBehaviour {

        public GameObject player;
        WASD wasd;
        StateController sc;
        PlayerStats ps;
        SoundController sound;
        CoconutEffect cf;
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
        bool up, down, left, right;
        Vector3 position;
        public GameObject mainCam;
        public GameObject snow;
        public GameObject coconut;
        HUDScript hud;

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
        
        void Update ()
        {
            if (startup == 0) {
                Debug.Log ("Making the stat change");
                //this.gameObject.GetComponent<PlayerStats> ().makeTheStatChange ();
                mainCam.gameObject.GetComponent<TutorialPlayerCam> ().setStartLocation (transform.position);
                startup = 1;
            }
            position = player.transform.position;
            if (Input.GetKeyDown (boomNanaKey)) {
                VFXScript vfx = gameObject.GetComponent<VFXScript> ();
                Transform aim = this.transform.GetChild (6);
                aim.GetComponent<Renderer> ().enabled = true;
                aim.localScale = new Vector3 (1f, 0, ps.boomnanaRange / 4);
                aim.localPosition = new Vector3 (0, 0, (ps.boomnanaRange / 2));
                aim.localEulerAngles = new Quaternion (90.0f, 0.0f, 0.0f, 0).eulerAngles;
            }
            if (Input.GetKeyUp (boomNanaKey) && !GetComponent<StateController>().isDead && !sc.isStunned && ! sc.isChanneling) {
                
                Debug.Log ("BOOOOMNANAAAAA");
                VFXScript vfx = gameObject.GetComponent<VFXScript> ();
                if (isInsideHidingGrass == true) {
                    BoomNanaUsedInHidingGrass = true;
                }
                animation.wrapMode = WrapMode.Once;
                animation.Play ("M_BM");
                if (!boomUsed || ((Time.time * 1000) - timeSinceLastBoom) >= (ps.boomNanaCooldown * 1000) && !sc.isStunned && sc.canMove && !sc.isChanneling && !sc.isDead) {
                    GameObject boom = Instantiate (Resources.Load ("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
                                                   new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
                    Boomnana boomscript = boom.GetComponent<Boomnana> ();
                    boomUsed = true;
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
                    sc.isMoving = true;
                }
            }
            
            if (Input.GetKey (moveDown) && !sc.isStunned && !sc.isDead) {
                down = true;
                if (sc.canMove) {
                    sc.isMoving = true;
                }
            }
            
            if (Input.GetKey (moveRight) && !sc.isStunned && !sc.isDead) {
                right = true;
                if (sc.canMove) {
                    sc.isMoving = true;
                }
                
            }
            if (Input.GetKey (moveLeft) && !sc.isStunned && !sc.isDead) {
                left = true;
                if (sc.canMove) {
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
                transform.position = transform.position + (position.normalized * sc.getSpeed ());
            }
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
                walk.speed = 6;
                animation ["M_Walk"].speed = 6;
                animation.PlayQueued ("M_Walk");
                //animation.wrapMode = WrapMode.Loop;
            }
            if (!sc.isMoving && animation.IsPlaying ("M_Walk")) {
                animation.wrapMode = WrapMode.Once;
                animation.Play ("M_Idle");
            }
            //      if(!sc.isChanneling && animation.IsPlaying("M_BP_Start")){
            //          animation.wrapMode = WrapMode.Once;
            //          animation.Play("M_BP_End");
            //          animation.CrossFadeQueued("M_Idle", 0.2f, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
            //      }
            
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
//                    IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
//                    while (entities.MoveNext()) {
//                        if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
//                            BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
//                            // Create Event and use the be, if it is the one that is colliding.
//                            // CoconutEffect buff the player
//                            if (be.gameObject == this.gameObject) {
//                                using (var evnt = PlayerBeingBuffedEvent.Create(Bolt.GlobalTargets.Everyone)) {
//                                    evnt.TargEnt = be;
//                                    evnt.CanPickUpCoconut = false;
//                                }
//                            }
//                        }
//                    }
                }
                if (Time.time - this.gameObject.GetComponent<PlayerStats> ().coconutEffectDuration >= this.gameObject.GetComponent<StateController> ().buffedPlayerDuration) {
                    Debug.Log (Time.time);
                    coconutEffectExpire ();
                }   
                if (sc.isDead) {
                    coconutEffectExpire ();
                }
            }
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
                animation ["M_Walk"].speed = 6;
                animation ["M_Walk"].wrapMode = WrapMode.Loop;
                animation ["M_Walk"].layer = 1;
                animation.Play ("M_Walk");
            }
        }
        
        // Use this for initialization
        void Start ()
        {
            
            player = this.gameObject;
            timeSinceLastBoom = Time.time;
            wasd = gameObject.GetComponent<WASD> (); 
            sc = gameObject.GetComponent<StateController> ();
            ps = gameObject.GetComponent<PlayerStats> ();
            sound = gameObject.GetComponent<SoundController> ();
            coconut = GameObject.FindGameObjectWithTag ("nut");
            
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
            
            //      AnimationClip ac = animation.GetClip("M_Death");
            //      Debug.Log(ac.name);
            //animation.CrossFade("M_Death",0.25f);
            //idle = animation["M_Death"];
            //animation.Play("M_Death");
            animation ["M_Walk"].speed = 4;
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

            KeyCode[] bindings = MenuScript.KeyBindings;
            tailSlapKey = bindings [0];
            boomNanaKey = bindings [1];
            aoeKey = bindings [2];
            ccKey = bindings [3];
            cprKey = bindings [4];
        }
        
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
        
        public void consumeCoconut ()
        {
            playerDmgBuff ();
            playerCcDurationBuff ();
//            IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
//            while (entities.MoveNext()) {
//                if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
//                    BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
//                    // Create Event and use the be, if it is the one that is colliding.
//                    //CoconutEffect buff the player
//                    if (be.gameObject == this.gameObject) {
//                        using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
//                            evnt.TargEnt = be;  
//                            evnt.isAffectedByCoconut = true;
//                            evnt.CoconutEffectDuration = Time.time;
//                            evnt.CoconutCCBuffDuration = buffedCcDuration;
//                            evnt.CoconutTailSlapDmgBuff = buffedTailSlapDmg;
//                            evnt.CoconutBoomnanaDmgBuff = buffedBoomnanaDmg;
//                            evnt.CoconutAOEDmgBuff = buffedAOEBuffDmg;
//                            evnt.StoppedInCoconutConsume = true;
//                        }
//                    }
//                }
//            }
        }
        
        public void coconutEffectExpire ()
        {
//            IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
//            while (entities.MoveNext()) {
//                if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
//                    BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
//                    // Create Event and use the be, if it is the one that is colliding.
//                    //CoconutEffect buff expire from the player
//                    if (be.gameObject == this.gameObject) {
//                        using (var evnt = PlayerBeingBuffedEvent.Create(Bolt.GlobalTargets.Everyone)) {
//                            evnt.TargEnt = be;
//                            evnt.CanPickUpCoconut = true;
//                        }
//                        using (var evnt = CoconutEffectEvent.Create(Bolt.GlobalTargets.Everyone)) {
//                            evnt.TargEnt = be;  
//                            evnt.isAffectedByCoconut = false;
//                            evnt.CoconutTailSlapDmgBuff = tailSlapDmg;
//                            evnt.CoconutBoomnanaDmgBuff = boomnanaDmg;
//                            evnt.CoconutAOEDmgBuff = aoeDmg;
//                            evnt.CoconutCCBuffDuration = ccDuration;
//                            evnt.StoppedInCoconutConsume = false;
//                        }
//                    }
//                }
//            }
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
}