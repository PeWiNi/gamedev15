using UnityEngine;
using System.Collections;

public class CprScript : MonoBehaviour
{
    /*
     * Costs banana resources
     * Has 4.5 sec CD
     */

    float lastUsed;
    bool available;

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
            int resources = coll.gameObject.GetComponent<PlayerStats>().cprBananas;
            if (coll.gameObject.name.Equals("PlayerObject3d")
                && coll.gameObject.GetComponent<PlayerStats>().teamNumber == this.gameObject.GetComponent<PlayerStats>().teamNumber
                && available
                && resources > 0)
            {
                available = false;
                coll.gameObject.GetComponent<PlayerStats>().cprBananas--;
                coll.gameObject.GetComponent<CprScript>().ress();
            }
        }
    }

    public void ress()
    {
        this.gameObject.GetComponent<PlayerStats>().hp = 100;
        this.gameObject.GetComponent<StateController>().isDead = false;
        this.gameObject.GetComponent<StateController>().isStunned = false;
    }
}
