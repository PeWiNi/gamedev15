using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour
{

	bool available = true;
	StateController sc;
	PlayerStats ps;
    TestPlayerBehaviour tpb;
    GameObject puke;
	float lastUsed;
	float lastTick;
	float tickTimer;
	float currentTimer;

	public bool AOEUsedInHidingGrass;

	bool animating = false;

	/* channeled, so canceled if moving.
     * should be going on for as long as not moving, or until duration is done.
     * Lock movement, but be able to rotate
     * DMG: 20/tick (2 % dmg from Max Heatlh )

 Ability Distance Factor: 1.5

 DUR: 4.5 sec (3 ticks)

 Range: UR x 1.5

 Radius of eect: .....

 CD: 7.5 sec
     * 
     //Get Input KeyCode
     //Set Player-> canMove = false;
     //Check if the colliding actor is a player, then deal AOE dmg pr. tick
     //set Available to false;
     //Check timer for duration;
     //Check if Jumping? Jump = cancel?
     //Update check timer for cooldown.
     * 
     */


	// Use this for initialization
	void Start ()
	{
		sc = this.gameObject.GetComponentInParent<StateController> ();
		ps = this.gameObject.GetComponentInParent<PlayerStats> ();
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
        puke = GameObject.Find("puke");
        puke.SetActive(false);
		tickTimer = ps.tickTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (sc == null || ps == null || tpb == null || tpbTutorial == null)
        {
            sc = this.gameObject.GetComponentInParent<StateController>();
            ps = this.gameObject.GetComponentInParent<PlayerStats>();
            if(gameObject.transform.parent.tag == "TutorialPlayer") {
                tpbTutorial = this.gameObject.GetComponentInParent<TutorialPlayerBehaviour>();
            } else {
                tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour>();
            }
        }
		if (Input.GetKeyDown (tpb.aoeKey) && available && !sc.isStunned && !sc.isDead) {
            puke.SetActive(true);
			Debug.Log ("CASTING AOE!!");
			sc.canMove = false;
			lastUsed = Time.time;
			sc.isChanneling = true;
			available = false;
			lastTick = Time.time;
            if(gameObject.transform.parent.tag == "TutorialPlayer") {
                GetComponentInParent<TutorialPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
                GetComponentInParent<TutorialPlayerBehaviour> ().animation.Play ("M_BP_Start");
            }
            else {
			    GetComponentInParent<TestPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
                GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_BP_Start");
            }

			//Using AOESTARTANIM
            var evnt = AoeStartAnimEvent.Create(Bolt.GlobalTargets.Everyone);
		    evnt.TargEnt = GetComponentInParent<TestPlayerBehaviour> ().entity;
            evnt.Send();
//			GetComponentInParent<TestPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
//			GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_BP_Start");
			animating = true;
			
//			GetComponentInParent<TestPlayerBehaviour>().animation.PlayQueued("M_BP_End");
//			GetComponentInParent<TestPlayerBehaviour>().animation.wrapMode = WrapMode.Loop;
		}
        if (sc.isJumping && !available) {
            puke.SetActive(false);
			sc.canMove = true;
			sc.isChanneling = false;
						
		}

		if (Time.time - lastUsed >= (ps.aoeDuration - 0.5f) && animating) {
			Debug.Log ("gonna start END ANIM");
            if(gameObject.transform.parent.tag == "TutorialPlayer") {
                GetComponentInParent<TutorialPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
                GetComponentInParent<TutorialPlayerBehaviour> ().animation.Play ("M_BP_End");
                GetComponentInParent<TutorialPlayerBehaviour> ().animation.CrossFadeQueued ("M_Idle", 0.2f, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
            }
            else {
			    GetComponentInParent<TestPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
			    GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_BP_End");
                GetComponentInParent<TestPlayerBehaviour> ().animation.CrossFadeQueued ("M_Idle", 0.2f, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
            }
            var evnt = AoeEndAnimEvent.Create(Bolt.GlobalTargets.Everyone) ;
			evnt.TargEnt = GetComponentInParent<TestPlayerBehaviour> ().entity;
            evnt.Send();
//			GetComponentInParent<TestPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
//			GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_BP_End");
//			GetComponentInParent<TestPlayerBehaviour> ().animation.CrossFadeQueued ("M_Idle", 0.2f, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
			animating = false;
		}
                
		// check timer for duration and Cooldown
        if (Time.time - lastUsed >= (ps.aoeDuration + 0.0001f) && !available) {
            puke.SetActive(false);
			sc.canMove = true;
			sc.isChanneling = false;

			Debug.Log ("AOE DONE!");

		} 
		if (Time.time - lastUsed >= ps.aoeCooldown) {
			available = true;
		}
		currentTimer = Time.time;
//		if (sc.isChanneling && GetComponentInParent<TestPlayerBehaviour> ().animation.IsPlaying ("M_BP_Start")) {
//			GetComponentInParent<TestPlayerBehaviour>().animation.CrossFadeQueued("M_BP_End", 0.05f, QueueMode.PlayNow);
//			GetComponentInParent<TestPlayerBehaviour>().animation.wrapMode = WrapMode.Loop;

			//GetComponentInParent<TestPlayerBehaviour>().animation.wrapMode = WrapMode.Loop;
			//GetComponentInParent<TestPlayerBehaviour>().animation.Play("M_BP_End", PlayMode.StopSameLayer);
//		}
	}


	void OnTriggerStay (Collider coll)
	{
		if (coll.gameObject.tag == "grass") {
            if(tpbTutorial != null){
                if (Input.GetKeyDown (tpbTutorial.aoeKey) && available) {
                    AOEUsedInHidingGrass = true;
                }
            } else {
                if (Input.GetKeyDown (tpb.aoeKey) && available) {
                    AOEUsedInHidingGrass = true;
                }
            }
		}
		if (sc.isChanneling && ((currentTimer - lastTick) > tickTimer) || (lastTick == lastUsed)) {
			Debug.Log ("Ticking");   
			lastTick = Time.time;
			IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
            if (coll.gameObject.tag == "player" || coll.gameObject.tag == "TutorialEnemeyDummy" && sc.isChanneling) {
                Debug.Log("Tagsies = " + coll.gameObject.tag);
                if(coll.gameObject.transform.parent.tag == "TutorialPlayer") {
                    if (coll.gameObject.tag != gameObject.transform.parent.tag) {// Check to see if the Dummy receives DMG.
                        Debug.Log("SHEEEEEEIT");
                    }
                } else {
				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						// Create Event and use the be, if it is the one that is colliding.
						if (be.gameObject == coll.gameObject/* && coll.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour> ().gameObject*/) { // Check for enemy, deal full damage
							//Debug.Log("AOE TICKING");
							Debug.Log ("Found the colliding GO");
							if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != this.gameObject.GetComponentInParent<PlayerStats> ().teamNumber) {
								// deal full damage!!!
								Debug.Log ("Sending Event with dmg = " + ps.aoeTickDamageFactor);
								var evnt = AoeEvent.Create(Bolt.GlobalTargets.Everyone);
								GameObject go = GameObject.Find ("Canvas");
								HUDScript hs = go.GetComponentInChildren<HUDScript> ();

                                hs.announcementText.text = "" + ps.aoeTickDamageFactor;
								evnt.TargEnt = be;
								evnt.TickDamage = ps.aoeTickDamageFactor;
								Debug.Log ("Ticking for " + ps.aoeTickDamageFactor + ".");

                                evnt.Send();
							} else { // check for friendly player, deal 50% dmg.
								// deal half damage!!!
                                var evnt = AoeEvent.Create(Bolt.GlobalTargets.Everyone);
								GameObject go = GameObject.Find ("Canvas");
								HUDScript hs = go.GetComponentInChildren<HUDScript> ();

                                hs.announcementText.text = "" + ps.aoeTickDamageFactor / 2;
								evnt.TargEnt = be;
								evnt.TickDamage = ps.aoeTickDamageFactor / 2;

                                evnt.Send();
							}
							sc.initiateCombat ();
						}
					}
                    }
                }
			}
		}
	}
}
