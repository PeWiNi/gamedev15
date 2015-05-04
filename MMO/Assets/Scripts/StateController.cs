using UnityEngine;
using System.Collections;
 
public class StateController : MonoBehaviour
{

	public bool inCombat = false;
	public bool isDead = false;
	public bool isMoving = false;
	public bool isStunned = false;
	public bool isChanneling = false;
	public bool isHolding = false;
	public bool isJumping = false;
	public bool isBuffed = false;
	public bool canMove = true;
	public bool ressStarted = false;
	public float combatEnteredTime;
	public float lastAttack;
	public float lastCombat;
	public float combatCooldownTime;
	public float globalCooldownTime = 1.5f;
	private float buffStartTime = 0;
	public float buffCoolDownTime;
	public float movementspeed;
	//Combat Speed Reduction?
	public float combatSpeedReduction;
	public float coconutSpeedReduction;
	public float currentSpeed;
	public float respawnTime;
	public float stunnedTimer;
	public int teamNumber;
	float stunnedStartFromCC;
	float stunnedDurationFromCC;
	//public float coconutBuffDuration = 30;
	public float buffedPlayerDuration = 30;
	public float coconutChannelTime = 1f;
	public float resetCoconutChannelTime = 0f;
	GameObject beacon;
	BeaconCaptureScript bZone;
	public float buffMultiplier;
	public bool gotHitInHidingGrass;

	public float getSpeed ()
	{
		return currentSpeed;
	}

	public void initiateCombat ()
	{
		lastCombat = Time.time;
		inCombat = true;
		if (!isHolding) {
			// REMOVE THE BUFF FOR MOVEMENTSPEED!
			if (isBuffed) {
				currentSpeed = (movementspeed - combatSpeedReduction) * buffMultiplier;
			} else {
				currentSpeed = movementspeed - combatSpeedReduction;
			}
		} else {
			if (isBuffed) {
				currentSpeed = (movementspeed - coconutSpeedReduction) * buffMultiplier;
			} else {
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		}
	}

	public void takeBuffDamage (GameObject target, float damage)
	{
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();
		//hs.damageEff ();
        hs.announcementText.text = "- " + Mathf.Ceil(damage);
		target.GetComponent<StateController> ().getHitByBuff (Mathf.Ceil (damage), target);
	}

	public void attack (GameObject target, float damage)
	{
		initiateCombat ();
		StateController targetSC = target.GetComponent<StateController> ();
		targetSC.getHit (damage);

		// FIND HUD AND ANIMATE DAMAGE EFFECT.
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();
		hs.damageEff ();
        hs.announcementText.text = "- " + damage;
		//TestPlayerBehaviour tpb = this.gameObject.GetComponent<TestPlayerBehaviour>();
		//  Debug.Log(tpb.mainCam.GetComponentInChildren<HUDScript>());   
		// tpb.mainCam.gameObject.GetComponentInChildren<HUDScript>().damageEff();
	}
	
//	public void attackBeacon (GameObject target, float damage)
//	{
//		StateController targetSC = target.GetComponent<StateController> ();
//		targetSC.beaconHit (damage);
//	}
	
//	public void beaconHit (float damage)
//	{
//		if (isBuffed) {
//			if (bZone.beaconUnderTeamOneControl == true) {
//				bZone.beaconOneHealth -= Mathf.Ceil (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
//			} 
//			if (bZone.beaconUnderTeamTwoControl == true) {
//				bZone.beaconTwoHealth -= Mathf.Ceil (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
//			} 
//		} else {
//			if (bZone.beaconUnderTeamOneControl == true) {
//				bZone.beaconOneHealth -= Mathf.Ceil (damage);
//			}
//			if (bZone.beaconUnderTeamTwoControl == true) {
//				bZone.beaconTwoHealth -= Mathf.Ceil (damage);
//			}
//		}
//		//	checkIfBeaconDestoryed ();
//	}

	public void getHitByBuff (float damage, GameObject target)
	{
		target.gameObject.GetComponent<PlayerStats> ().hp -= Mathf.Ceil (damage);
		checkIfDead ();
	}

	public void getHit (float damage)
	{
		if (isBuffed) {
			GetComponent<PlayerStats> ().hp -= Mathf.Ceil (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
			gotHitInHidingGrass = true;
		} else {
			GetComponent<PlayerStats> ().hp -= Mathf.Ceil (damage);
			gotHitInHidingGrass = true;
		}
		checkIfDead ();
	}

	public void stun (GameObject target, float duration)
	{
		Debug.Log ("GETTING STUNNED!");
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();
        hs.announcementText.text = "Stunned!";
        hs.announcementText.color = new Color(hs.announcementText.color.r, hs.announcementText.color.g, hs.announcementText.color.b, 1);
		stunnedStartFromCC = Time.time;
		stunnedDurationFromCC = duration;
		isStunned = true;
		canMove = false;
	}

//		public bool checkIfBeaconDestoryed ()
//		{
//			
//		}

	private bool checkIfDying(){
		if(!isDead){
			return true;
		}else{
			return false;
		}
	}


	public bool checkIfDead ()
	{
		if (GetComponent<PlayerStats> ().hp <= 0) {
			if(checkIfDying ()){
				GetComponentInParent<TestPlayerBehaviour> ().animation.wrapMode = WrapMode.Once;
				GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_Death");
			}
			isDead = true;
			isStunned = true;
			// INITIATE DEATHSPAWNER!!!
			if (!ressStarted) {
				ressStarted = true;
				this.gameObject.GetComponent<DeathSpawner> ().startRespawn ();
			}

			switch (teamNumber) {
			case 1:
				using (var evnt = KillScoreOneEvent.Create(Bolt.GlobalTargets.Everyone)) { 
					evnt.Score = 5;
				}
				break;
			case 2:
				using (var evnt = KillScoreOneEvent.Create(Bolt.GlobalTargets.Everyone)) { 
					evnt.Score = 5;
				}
				break;
			}
            
			return isDead;
		} else {
			return false;
		}
	}

	public bool isAbleToBuff ()
	{
		if (buffStartTime == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void checkCombatTime ()
	{
		if (lastCombat != 0 && (Time.time - lastCombat) >= combatCooldownTime) {
			inCombat = false;
			if (isBuffed) {
				currentSpeed = movementspeed;
			} else {
				currentSpeed = movementspeed * buffMultiplier;
			}
		}
	}

	public bool getCombatState ()
	{
		return inCombat;
	}

	void Start ()
	{
//		beacon = GameObject.FindGameObjectWithTag ("beacon");
//		bZone = beacon.GetComponent<BeaconCaptureScript> ();
	}

	void start ()
	{
		currentSpeed = movementspeed;
		isStunned = true;
		canMove = false;
	}

	void checkIfHolding ()
	{
		if (isHolding) {
			if (isBuffed) {
				currentSpeed = (movementspeed - coconutSpeedReduction) * buffMultiplier; 
			} else {
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		}
	}

	public void buff ()
	{
		buffStartTime = Time.time;
		currentSpeed *= buffMultiplier;
		isBuffed = true;
	}

	void originalSpeeds ()
	{
		if (isHolding) {
			if (isBuffed) {
				currentSpeed = (movementspeed - coconutSpeedReduction) * buffMultiplier; 
			} else {
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		} else if (inCombat) {
			if (isBuffed) {
				currentSpeed = (movementspeed - combatSpeedReduction) * buffMultiplier;
			} else {
				currentSpeed = movementspeed - combatSpeedReduction;
			}
		} else {
			if (isBuffed) {
				currentSpeed = movementspeed * buffMultiplier;
			} else {
				currentSpeed = movementspeed;
			} 
		} 
	}

	void checkBuffTimer ()
	{
		if ((Time.time - buffStartTime) >= GetComponent<PlayerStats> ().buffDuration) {
			isBuffed = false;
			if ((Time.time - buffStartTime) >= buffCoolDownTime) {
				buffStartTime = 0;
			}
			originalSpeeds ();
		}
	}

	void updateMovementSpeed ()
	{
		currentSpeed = currentSpeed * buffMultiplier;
	}

	void checkCCStun ()
	{
		if (Time.time - stunnedStartFromCC >= stunnedDurationFromCC) {
			//Debug.Log("Timer: " +(Time.time - stunnedStartFromCC) + " dur: "+ stunnedDurationFromCC);
			isStunned = false;
			stunnedDurationFromCC = 0;
			if (!isChanneling) {
				canMove = true;
			}
            
		}
	}
     
	void Update ()
	{
		//checkBuffTimer ();
		checkIfDead ();
		originalSpeeds ();
		checkCombatTime ();
		checkIfHolding ();
		checkCCStun ();
	}
}