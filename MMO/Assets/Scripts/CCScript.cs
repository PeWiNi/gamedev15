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
        if (Input.GetKeyDown(KeyCode.Alpha4) && available)
        {
            IEnumerator entities = BoltNetwork.entities.GetEnumerator();
            if (coll.gameObject.tag == "player")
            {
                while (entities.MoveNext())
                {
                    if (entities.Current.GetType().IsInstanceOfType(new BoltEntity()))
                    {
                        BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
                        // Create Event and use the be, if it is the one that is colliding.
                        if (be.gameObject == coll.gameObject)
                        { // Check for enemy, deal full damage
                            if (coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                            {
                                // deal full damage!!!
                                using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
                                {
                                    evnt.TargEnt = be;
                                    evnt.Duration = this.gameObject.GetComponentInParent<PlayerStats>().ccDuration;
                                }
                            }
                            else // check for friendly player, deal 50% dmg.
                            {
                                // deal half damage!!!
                                using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
                                {
                                    evnt.TargEnt = be;
                                    evnt.Duration = this.gameObject.GetComponentInParent<PlayerStats>().ccDuration / 2;
                                }
                            }
                        }
                    }
                }
            }
            available = false;
            lastUsed = Time.time;
        }
    }
}
