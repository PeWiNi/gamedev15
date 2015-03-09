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
<<<<<<< HEAD
            int resources = coll.gameObject.GetComponent<PlayerStats>().cprBananas;
            if (coll.gameObject.name.Equals("PlayerObject3d")
                && coll.gameObject.GetComponent<PlayerStats>().teamNumber == this.gameObject.GetComponent<PlayerStats>().teamNumber
=======
            int resources = this.gameObject.GetComponentInParent<PlayerStats>().cprBananas;
            if (coll.gameObject.tag == "player"
                && coll.gameObject.GetComponent<PlayerStats>().teamNumber == this.gameObject.GetComponentInParent<PlayerStats>().teamNumber
>>>>>>> origin/master
                && available
                && resources > 0)
            {
                available = false;
<<<<<<< HEAD
                coll.gameObject.GetComponent<PlayerStats>().cprBananas--;
                coll.gameObject.GetComponent<CprScript>().ress();
=======
                this.gameObject.GetComponentInParent<PlayerStats>().cprBananas--;
                coll.gameObject.GetComponentInChildren<CprScript>().ress();
>>>>>>> origin/master
            }
        }
    }

    public void ress()
    {
<<<<<<< HEAD
        this.gameObject.GetComponent<PlayerStats>().hp = 100;
        this.gameObject.GetComponent<StateController>().isDead = false;
        this.gameObject.GetComponent<StateController>().isStunned = false;
=======
        Debug.Log("I AM BEING RESSED!"); 
        this.gameObject.GetComponentInParent<PlayerStats>().hp = 100;
        this.gameObject.GetComponentInParent<StateController>().isDead = false;
        this.gameObject.GetComponentInParent<StateController>().isStunned = false;
>>>>>>> origin/master
    }
}
