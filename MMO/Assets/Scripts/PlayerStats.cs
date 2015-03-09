using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

    //Get Max HP from server, and give each player a divided hp
    // Use StatSplitter.cs

    //Stats
    float playerHp;
    public int teamNumber;
    float movementSpeed;
    //Damages 
    public float tailSlapDamage;
    public float boomNanaDamage;
    //Ranges
    public float boomnanaRange;
    public float jumpHeight;
    //Timers
<<<<<<< HEAD
    float cc;
    public float buffDuration;
    public float hp;
    //Resouces
    public int cprBananas;
=======
    public float ccDuration;
    public float buffDuration;
    public float hp;
    //Resouces
    public int cprBananas; 
>>>>>>> origin/master
    //Cooldowns
    float globalCooldownCounter;
    public float tailSlapCooldown;
    public float boomNanaCooldown;
    public float cprCooldown;
<<<<<<< HEAD
    public float aoeCooldown;
=======
    public float aoeCooldown; 
>>>>>>> origin/master
    public float ccCooldown;
    public float buffCooldown;
    //BuffAttributes
    public float buffCostFactor = 0.05f;
    public float buffDamageFactor = 1.2f;
<<<<<<< HEAD
=======
    //BuffedStuff
    public bool buffed = false;
>>>>>>> origin/master


    // Use this for initialization
    void Start()
    {

    }

    void setSplitStats()
    {

    }

    void updateHp()
    {

    }

    void setMovementSpeed()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
