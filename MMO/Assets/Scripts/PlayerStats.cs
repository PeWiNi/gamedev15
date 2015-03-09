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
    float cc;
    public float buffDuration;
    public float hp;
    //Resouces
    public int cprBananas;
    //Cooldowns
    float globalCooldownCounter;
    public float tailSlapCooldown;
    public float boomNanaCooldown;
    public float cprCooldown;
    public float aoeCooldown;
    public float ccCooldown;
    public float buffCooldown;
    //BuffAttributes
    public float buffCostFactor = 0.05f;
    public float buffDamageFactor = 1.2f;


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
