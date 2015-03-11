using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour
{

		bool available = true;
		StateController sc;
		PlayerStats ps;
		float lastUsed;
		float lastTick;
		bool isChanneling = false;
		float tickTimer;
		float currentTimer;
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
				tickTimer = ps.tickTime;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.R) && available) {
						sc.canMove = false;
						lastUsed = Time.time;
						isChanneling = true;
						available = false;
						lastTick = Time.time;
				}
				if (sc.isJumping) {
						sc.canMove = true;
						isChanneling = false;
						
				}
				// check timer for duration and Cooldown
				if (Time.time - lastUsed >= ps.aoeDuration + 0.5f) {
						sc.canMove = true;
						isChanneling = false;

				} 
				if (Time.time - lastUsed >= ps.aoeCooldown) {
						available = true;
				}
				currentTimer = Time.time;
		}
	 
		void OnTriggerStay (Collider coll)
		{
				if (coll.gameObject.tag == "player" && isChanneling) {
						//Debug.Log ("STAY: " + (currentTimer - lastTick));
						if ((currentTimer - lastTick) > tickTimer) {
								lastTick = currentTimer;
								coll.gameObject.GetComponent<StateController> ().attack (coll.gameObject, ps.aoeTickDamageFactor);
								sc.initiateCombat (); 
						}
				}
		}

		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player" && isChanneling) {
						//Debug.Log ("STAY: " + (currentTimer - lastTick));
						if ((currentTimer - lastTick) > tickTimer) {
								lastTick = currentTimer;
								coll.gameObject.GetComponent<StateController> ().attack (coll.gameObject, ps.aoeTickDamageFactor);
								sc.initiateCombat (); 
						}
				}
		}
}
