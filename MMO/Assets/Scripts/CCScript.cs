using UnityEngine;
using System.Collections;

public class CCScript : MonoBehaviour {

    //Get Input KeyCode
    //Get stun Duration from player
    //Check if the colliding actor is a player, and stun them
    //set Available to false;
    //Update check timer for cooldown.

    bool available = true;
    float lastUsed;
    PlayerStats ps;

	// Use this for initialization
	void Start () {
        ps = this.gameObject.GetComponentInParent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time - lastUsed >= ps.ccCooldown)
        {
            available = true;
        }
	}

    void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.Alpha3 )) 
        {
            if (coll.gameObject.tag == "player")
            {
                if (coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                {
                    if (available)
                    {
                        Debug.Log("STUNNING THE DUDE!");
                        // ADD % to miss!
                       coll.gameObject.GetComponentInParent<StateController>().
                            stun(coll.gameObject, this.gameObject.GetComponentInParent<PlayerStats>().ccDuration);
                        available = false;
                        lastUsed = Time.time;
                    }
                }
            }
        }
    }
}
