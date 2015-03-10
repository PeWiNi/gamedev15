using UnityEngine;
using System.Collections;

public class CprScript : MonoBehaviour
{
    /*
     * Costs banana resources
     * Has 4.5 sec CD
     */

    float lastUsed;
    bool available = true;

    void Update()
    {
        if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats>().cprCooldown)
        {
            available = true;
        }
    }


    void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int resources = this.gameObject.GetComponentInParent<PlayerStats>().cprBananas;
            if (coll.gameObject.tag == "player"
                && coll.gameObject.GetComponent<PlayerStats>().teamNumber == this.gameObject.GetComponentInParent<PlayerStats>().teamNumber
                && available
                && resources > 0)
            {
                available = false;
                this.gameObject.GetComponentInParent<PlayerStats>().cprBananas--;
                coll.gameObject.GetComponentInChildren<CprScript>().ress();
            }
        }
    }

    public void ress()
    {
        Debug.Log("I AM BEING RESSED!"); 
        this.gameObject.GetComponentInParent<PlayerStats>().hp = 100;
        this.gameObject.GetComponentInParent<StateController>().isDead = false;
        this.gameObject.GetComponentInParent<StateController>().isStunned = false;
        this.gameObject.GetComponentInParent<StateController>().ressStarted = false;
        // CALL DEATHSPAWNER -> CANCEL RESPAWN...
        this.gameObject.GetComponentInParent<DeathSpawner>().cancelRespawn();
    }
}
