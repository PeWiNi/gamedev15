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
		public float coconutDuration = 120f;
		public float coconutChannelTime = 5f;
		GameObject beaconOne;
		GameObject beaconTwo;
		GameObject beaconThree;
		BeaconZone bOne;
		BeaconZone bTwo;
		BeaconZone bThree;
		public float buffMultiplier;

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

	public void takeBuffDamage(GameObject target, float damage){
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();
		//hs.damageEff ();
		hs.dmgDealt.text = "- " +Mathf.Ceil (damage);
		target.GetComponent<StateController>().getHitByBuff(Mathf.Ceil(damage));
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
				hs.dmgDealt.text = "- " + damage;
				//TestPlayerBehaviour tpb = this.gameObject.GetComponent<TestPlayerBehaviour>();
				//  Debug.Log(tpb.mainCam.GetComponentInChildren<HUDScript>());   
				// tpb.mainCam.gameObject.GetComponentInChildren<HUDScript>().damageEff();
		}
	
		public void attackBeacon (GameObject target, float damage)
		{
				StateController targetSC = target.GetComponent<StateController> ();
				targetSC.beaconHit (damage);
		}
	
		public void beaconHit (float damage)
		{
				if (isBuffed) {
						if (bOne.zoneOneTeamOneActive == true) {
								bOne.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						} else if (bOne.zoneOneTeamTwoActive == true) {
								bOne.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						}
						if (bTwo.zoneTwoTeamOneActive == true) {
								bTwo.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						} else if (bTwo.zoneTwoTeamTwoActive == true) {
								bTwo.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						}
						if (bThree.zoneThreeTeamOneActive == true) {
								bThree.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						} else if (bThree.zoneThreeTeamTwoActive == true) {
								bThree.beaconHealth -= (damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
						}
				} else {
						if (bOne.zoneOneTeamOneActive == true) {
								bOne.beaconHealth -= damage;
						} else if (bOne.zoneOneTeamTwoActive == true) {
								bOne.beaconHealth -= damage;
						}
						if (bTwo.zoneTwoTeamOneActive == true) {
								bTwo.beaconHealth -= damage;
						} else if (bTwo.zoneTwoTeamTwoActive == true) {
								bTwo.beaconHealth -= damage;
						}
						if (bTwo.zoneThreeTeamOneActive == true) {
								bThree.beaconHealth -= damage;
						} else if (bTwo.zoneThreeTeamTwoActive == true) {
								bThree.beaconHealth -= damage;
						}
				}
				//	checkIfBeaconDestoryed ();
		}

	public void getHitByBuff(float damage){
		GetComponent<PlayerStats> ().hp -= Mathf.Ceil(damage);
		checkIfDead ();
	}

		public void getHit (float damage)
		{
				if (isBuffed) {
						GetComponent<PlayerStats> ().hp -= Mathf.Ceil(damage / gameObject.GetComponent<PlayerStats> ().buffDamageFactor);
				} else {
						GetComponent<PlayerStats> ().hp -= Mathf.Ceil(damage);
				}
				checkIfDead ();
		}

		public void stun (GameObject target, float duration)
		{
				Debug.Log ("GETTING STUNNED!");
				stunnedStartFromCC = Time.time;
				stunnedDurationFromCC = duration;
				isStunned = true;
				canMove = false;
		}

//		public bool checkIfBeaconDestoryed ()
//		{
//			
//		}

		public bool checkIfDead ()
		{
				if (GetComponent<PlayerStats> ().hp <= 0) {
						isDead = true;
						isStunned = true;
						// INITIATE DEATHSPAWNER!!!
						if (!ressStarted) {
								ressStarted = true;
								this.gameObject.GetComponent<DeathSpawner> ().startRespawn ();
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
				beaconOne = GameObject.Find ("BeaconZone01");
				beaconTwo = GameObject.Find ("BeaconZone02");
				beaconThree = GameObject.Find ("BeaconZone03");
				bOne = beaconOne.GetComponent<BeaconZone> ();
				bTwo = beaconTwo.GetComponent<BeaconZone> ();
				bThree = beaconThree.GetComponent<BeaconZone> ();
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