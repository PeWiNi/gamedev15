using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour
{

	bool available = true;
	StateController sc;
	PlayerStats ps;
	TestPlayerBehaviour tpb;
	float lastUsed;
	float lastTick;
	float tickTimer;
	float currentTimer;
	public bool AOEUsedInHidingGrass;

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
		tickTimer = ps.tickTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sc = this.gameObject.GetComponentInParent<StateController> ();
		ps = this.gameObject.GetComponentInParent<PlayerStats> ();
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
		if (Input.GetKeyDown (tpb.aoeKey) && available && !sc.isStunned && !sc.isDead) {
			Debug.Log ("CASTING AOE!!");
			sc.canMove = false;
			lastUsed = Time.time;
			sc.isChanneling = true;
			available = false;
			lastTick = Time.time;
		}
		if (sc.isJumping && !available) {
			sc.canMove = true;
			sc.isChanneling = false;
						
		}
                
		// check timer for duration and Cooldown
		if (Time.time - lastUsed >= (ps.aoeDuration + 0.0001f) && !available) {
			sc.canMove = true;
			sc.isChanneling = false;
			Debug.Log ("AOE DONE!");

		} 
		if (Time.time - lastUsed >= ps.aoeCooldown) {
			available = true;
		}
		currentTimer = Time.time;
	}

	void OnTriggerStay (Collider coll)
	{
		if (coll.gameObject.name == "HidingGrass") {
			if (Input.GetKeyDown (tpb.aoeKey) && available) {
				AOEUsedInHidingGrass = true;
			}
		}
		if (sc.isChanneling && ((currentTimer - lastTick) > tickTimer) || (lastTick == lastUsed)) {
			Debug.Log ("Ticking");   
			lastTick = Time.time;
			IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
			if (coll.gameObject.tag == "player" && sc.isChanneling) {
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
								using (var evnt = AoeEvent.Create(Bolt.GlobalTargets.Everyone)) {
									GameObject go = GameObject.Find ("Canvas");
									HUDScript hs = go.GetComponentInChildren<HUDScript> ();
									
									hs.dmgDealt.text = "" + ps.aoeTickDamageFactor;
									evnt.TargEnt = be;
									evnt.TickDamage = ps.aoeTickDamageFactor;
									Debug.Log ("Ticking for " + ps.aoeTickDamageFactor + ".");
								}
							} else { // check for friendly player, deal 50% dmg.
								// deal half damage!!!
								using (var evnt = AoeEvent.Create(Bolt.GlobalTargets.Everyone)) {
									GameObject go = GameObject.Find ("Canvas");
									HUDScript hs = go.GetComponentInChildren<HUDScript> ();
									
									hs.dmgDealt.text = "" + ps.aoeTickDamageFactor / 2;
									evnt.TargEnt = be;
									evnt.TickDamage = ps.aoeTickDamageFactor / 2;
								}
							}
							sc.initiateCombat ();
						}

					}
				}

			}
                
		}
	}
}
