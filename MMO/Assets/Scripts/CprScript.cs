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
        if (Input.GetKeyDown(KeyCode.C) && available)
        {
            int resources = this.gameObject.GetComponentInParent<PlayerStats>().cprBananas;
            IEnumerator entities = BoltNetwork.entities.GetEnumerator();
            while (entities.MoveNext())
            {
                if (coll.gameObject.tag == "player" && resources > 0)
                {
                    if (entities.Current.GetType().IsInstanceOfType(new BoltEntity()))
                    {
                        BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
                        // Create Event and use the be, if it is the one that is colliding.
                        if (be.gameObject == coll.gameObject)
                        {
                            if (coll.gameObject.GetComponent<PlayerStats>().teamNumber == this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                            {
                                using (var evnt = CprEvent.Create(Bolt.GlobalTargets.Everyone))
                                {
                                    evnt.TargEnt = be;
                                }
                            }
                        }
                    }
                }
                //coll.gameObject.GetComponentInChildren<CprScript>().ress();
            }
            available = false;
            this.gameObject.GetComponentInParent<PlayerStats>().cprBananas--;
        }
    }

    public void ress()
    {
        Debug.Log("I AM BEING RESSED!");
        this.gameObject.GetComponentInParent<PlayerStats>().hp = this.gameObject.GetComponentInParent<PlayerStats>().maxHealth;
        this.gameObject.GetComponentInParent<StateController>().isDead = false;
        this.gameObject.GetComponentInParent<StateController>().isStunned = false;
        this.gameObject.GetComponentInParent<StateController>().ressStarted = false;
        // CALL DEATHSPAWNER -> CANCEL RESPAWN...
        this.gameObject.GetComponentInParent<DeathSpawner>().cancelRespawn();
    }
}
