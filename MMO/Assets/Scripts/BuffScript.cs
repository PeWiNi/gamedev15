using UnityEngine;
using System.Collections;

public class BuffScript : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        ps = gameObject.GetComponent<PlayerStats>();
        sc = gameObject.GetComponent<StateController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.B) && available)
        {
            Debug.Log("BUFFING!!");
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
        if (Time.time - lastUsed >= ps.buffCooldown && !available) 
        {         
            available = true;
        }
        //CHECK DURATION
        if (Time.time - lastUsed >= ps.buffDuration && sc.isBuffed)
        {
            sc.isBuffed = false;
            float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
            ps.tailSlapDamage = tailSlapDamageBuffed;

            float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
            ps.boomNanaDamage = boomnanaDamageBuffed;
        } 
	}
}
 