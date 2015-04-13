using UnityEngine;
using System.Collections;

public class BuffScript : MonoBehaviour
{

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
        TestPlayerBehaviour tpb;
		public static bool TrapBuff;
		public static float orgBuffDur;
		public static float playerOneBuffDmg;
		public static float tailSlapDmg;
		public static float boomnanaDmg;

        private float secondsTimer = 0;
        private GameObject boost;
        private float boostTimer = 0;
        public float boostDuration = 10;
        private float boostAlpha = 0.2f;
        private float boostDecay;
        public Vector3 end;

        private void buffSetup()
        {
            //Create Particle System
            boost = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Boost"));
            //Make it "fire" upwards
            boost.transform.Rotate(Vector3.right, -90);
            //"Scale" according to size of user
            boost.GetComponent<ParticleSystem>().startLifetime += 0.2f * this.gameObject.transform.GetComponent<Collider>().bounds.size.y; //Works, makes the particles last longer and therefore moves further in y-axis (scaling it accoding to height of mesh)
            //boost.particleSystem.shape.radius = 2;
            boost.GetComponent<ParticleSystem>().transform.localScale.Scale(this.transform.lossyScale); //doesn't work 
            //TODO: Make at least the radius of particle system (shape) scale with size of user
            //Start and Deactivate (Initialize it without it being activated)
            boost.GetComponent<ParticleSystem>().Play();
            boost.SetActive(false);
        }

		// Use this for initialization
		void Start ()
		{
				ps = gameObject.GetComponent<PlayerStats> ();
				sc = gameObject.GetComponent<StateController> ();
                tpb = gameObject.GetComponent<TestPlayerBehaviour>();
                buffSetup();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (ps.teamNumber == 1) {
						if (BeeHive.playerOneIsBuffed == false) {
								if (ps.trapBeeHiveBuffed == true) {
										float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
										ps.tailSlapDamage = tailSlapDamageBuffed;
										Debug.Log ("" + ps.tailSlapDamage);
										float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
										ps.boomNanaDamage = boomnanaDamageBuffed;
										ps.trapBeeHiveBuffed = false;
								}
						}
				}
				if (ps.teamNumber == 2) {
						if (BeeHive.playerTwoIsBuffed == false) {
								if (ps.trapBeeHiveBuffed == true) {
										float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
										ps.tailSlapDamage = tailSlapDamageBuffed;
										Debug.Log ("" + ps.tailSlapDamage);
										float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
										ps.boomNanaDamage = boomnanaDamageBuffed;
										ps.trapBeeHiveBuffed = false;
								}
						}
				}

                if (boostTimer <= 0)
                {
                    boost.SetActive(false);
                }
				if (Input.GetKeyDown (tpb.buffKey) && available) {

			float currentHP = (ps.hp * ps.buffCostFactor); 
			gameObject.GetComponent<StateController>().takeBuffDamage(this.gameObject, currentHP);

                    // ADD VFX


                    boostTimer = ps.buffDuration;
                        boostAlpha = 0.2f;
                        boost.SetActive(true);
                    

                    // Calculate in seconds!

						Debug.Log ("BUFFING!!");
						//ps.hp -= currentHP;

						float tailSlapDamageBuffed = ps.tailSlapDamage * ps.buffDamageFactor;
						ps.tailSlapDamage = tailSlapDamageBuffed;

						float boomnanaDamageBuffed = ps.boomNanaDamage * ps.buffDamageFactor;
						ps.boomNanaDamage = boomnanaDamageBuffed;

						lastUsed = Time.time;

						available = false;
						sc.isBuffed = true;
				}

                if (boostTimer <= ps.buffDuration && boostTimer > 0)
                {
                    boost.transform.position = new Vector3(this.gameObject.transform.position.x, (this.gameObject.transform.position.y - (this.gameObject.transform.GetComponent<Collider>().bounds.size.y / 2)), this.gameObject.transform.position.z);
                    boost.GetComponent<ParticleSystem>().startColor = new Color(boost.GetComponent<ParticleSystem>().startColor.r, boost.GetComponent<ParticleSystem>().startColor.g, boost.GetComponent<ParticleSystem>().startColor.b, boostAlpha);
                    secondsTimer += Time.deltaTime;
                    if (secondsTimer > 1.0f)
                    {
                        buffDecay();
                        secondsTimer -= 1.0f;
                    }
                }

				//CHECK COOLDOWN
				if (Time.time - lastUsed >= ps.buffCooldown && !available) {         
						available = true;
				}
				//CHECK DURATION
				if (Time.time - lastUsed >= ps.buffDuration && sc.isBuffed) {
						sc.isBuffed = false;
						float tailSlapDamageBuffed = ps.tailSlapDamage / ps.buffDamageFactor;
						ps.tailSlapDamage = tailSlapDamageBuffed;

						float boomnanaDamageBuffed = ps.boomNanaDamage / ps.buffDamageFactor;
						ps.boomNanaDamage = boomnanaDamageBuffed;
				} 
		}
        private void buffDecay()
        {
            boostTimer -= 1;
            boostAlpha -= boostDecay;
        }
}
