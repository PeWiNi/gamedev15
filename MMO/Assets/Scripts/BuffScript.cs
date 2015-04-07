﻿using UnityEngine;
using System.Collections;

public class BuffScript : MonoBehaviour
{

		/*
 * Costs 5% of currentHP
 * Buffs 20% dmg and damage Reduction
 * Duration 3.5 sec
 * Cooldown 10 sec 
 */


      
		//Get Current HP -> -= currentHP*0.05;
		//Increase damage -> damage += damage*0.2;
		//Increase damageReduction -> damageReduction += damageReduction*0.2;
		//Set startTime for buff -> buffStart = Time.time;
		// Availability = false
		//In update() => check cooldown.
		// BUFF ONLY INCREASES YOURSELF!

		bool available = true;
		float lastUsed;
		PlayerStats ps;
		StateController sc;
        TestPlayerBehaviour tpb;
		public static bool TrapBuff;
		public static float orgBuffDur;
		public static float playerOneBuffDmg;
		public static float tailSlapDmg;
		public static float boomnanaDmg;

		// Use this for initialization
		void Start ()
		{
				ps = gameObject.GetComponent<PlayerStats> ();
				sc = gameObject.GetComponent<StateController> ();
                tpb = gameObject.GetComponent<TestPlayerBehaviour>();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (ps.teamNumber == 1) {
						if (BeeHive.playerOneIsBuffed == false) {
								if (ps.trapBeeHiveBuffed == true) {
										float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
										ps.tailSlapDamage = tailSlapDamageBuffed;
										Debug.Log ("" + ps.tailSlapDamage);
										float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
										ps.boomNanaDamage = boomnanaDamageBuffed;
										ps.trapBeeHiveBuffed = false;
								}
						}
				}
				if (ps.teamNumber == 2) {
						if (BeeHive.playerTwoIsBuffed == false) {
								if (ps.trapBeeHiveBuffed == true) {
										float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
										ps.tailSlapDamage = tailSlapDamageBuffed;
										Debug.Log ("" + ps.tailSlapDamage);
										float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
										ps.boomNanaDamage = boomnanaDamageBuffed;
										ps.trapBeeHiveBuffed = false;
								}
						}
				}
			

				if (Input.GetKeyDown (tpb.buffKey) && available) {
						Debug.Log ("BUFFING!!");
						float currentHP = (ps.hp * ps.buffCostFactor);
						ps.hp -= currentHP;

						float tailSlapDamageBuffed = ps.tailSlapDamage * ps.buffDamageFactor;
						ps.tailSlapDamage = tailSlapDamageBuffed;

						float boomnanaDamageBuffed = ps.boomNanaDamage * ps.buffDamageFactor;
						ps.boomNanaDamage = boomnanaDamageBuffed;

						lastUsed = Time.time;

						available = false;
						sc.isBuffed = true;
				}

				//CHECK COOLDOWN
				if (Time.time - lastUsed >= ps.buffCooldown && !available) {         
						available = true;
				}
				//CHECK DURATION
				if (Time.time - lastUsed >= ps.buffDuration && sc.isBuffed) {
						sc.isBuffed = false;
						float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
						ps.tailSlapDamage = tailSlapDamageBuffed;

						float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
						ps.boomNanaDamage = boomnanaDamageBuffed;
				} 
		}
}
