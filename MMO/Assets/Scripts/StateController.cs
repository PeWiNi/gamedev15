using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {

	bool inCombat = false;
	float combatEnteredTime;
	bool isDead = false;
	bool isMoving = false;
	float lastAttack;
	float lastCombat;
	float combatCooldownTime;
	float hp;
	float movementspeed;
	//Combat Speed Reduction?
	float combatSpeedReduction;
	float currentSpeed;
	float respawnTime;


	public float getSpeed(){
		return currentSpeed;
	}

	public void initiateCombat (){
		lastCombat = Time.time;
		inCombat = true;
		currentSpeed = movementspeed - combatSpeedReduction;
	}

	public void attack(){
		initiateCombat ();
	}

	public void checkCombatTime(){
		if ((Time.time - lastCombat) >= combatCooldownTime) {
			inCombat = false;
			currentSpeed = movementspeed;
		}
	}

	public bool getCombatState(){
		return inCombat;
	}

	void start(){
		currentSpeed = movementspeed;
	}

	// Update is called once per frame
	void Update () {
		checkCombatTime ();
	}
}
/*
 Isn't it just keeping track of how long it has been since you last attacked or took damage and then choose enum or bool accordingly?
*/