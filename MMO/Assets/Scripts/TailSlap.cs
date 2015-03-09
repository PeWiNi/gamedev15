﻿using UnityEngine;
using System.Collections;

public class TailSlap : MonoBehaviour
{

    bool available = true;
    float lastUsed;

    void start()
    {
    }

    void Update()
    {
        if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats>().tailSlapCooldown)
        {
            available = true;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
<<<<<<< HEAD
            if (coll.gameObject.name.Equals("PlayerObject3d") && coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponent<PlayerStats>().teamNumber && available)
            {
                gameObject.GetComponent<StateController>().attack(coll.gameObject, gameObject.GetComponent<PlayerStats>().tailSlapDamage);
                coll.gameObject.GetComponent<PlayerStats>().hp -= gameObject.GetComponent<PlayerStats>().tailSlapDamage;
                available = false;
                lastUsed = Time.time;
=======
            if(coll.gameObject.tag == "player"){
                if (coll.gameObject.GetComponent<PlayerStats>().teamNumber  != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                {
                    if (available)
                    {
                        this.gameObject.GetComponentInParent<StateController>().attack(coll.gameObject, this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage);
                        available = false;
                        lastUsed = Time.time;
                    }
                }
>>>>>>> origin/master
            }
        }
    }
}
