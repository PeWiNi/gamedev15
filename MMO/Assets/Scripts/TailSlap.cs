using UnityEngine;
using System.Collections;

public class TailSlap : MonoBehaviour
{

		bool available = true;
		float lastUsed;

		void start ()
		{
		}

		void Update ()
		{
				if ((Time.time - lastUsed) >= gameObject.GetComponentInParent<PlayerStats> ().tailSlapCooldown) {
						available = true;
				}
		}

        void OnTriggerStay(Collider coll)
        {
            IEnumerator entities = BoltNetwork.entities.GetEnumerator();
            if (coll.gameObject.tag == "player")
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    while (entities.MoveNext())
                    {
                        if (entities.Current.GetType().IsInstanceOfType(new BoltEntity()))
                        {
                            BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
                            // Create Event and use the be, if it is the one that is colliding.

                            if (be.gameObject == coll.gameObject)
                            { // Check for enemy, deal full damage
                                if (available)
                                {
                                    Debug.Log("SLAPPING DA TAIL");
                                    if (coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                                    {
                                        // deal full damage!!!
                                        using (var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone))
                                        {
                                            evnt.TargEnt = be; 
                                            evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage;
                                        }
                                    }
                                    else // check for friendly player, deal 50% dmg.
                                    {
                                        // deal half damage!!!
                                        using (var evnt = TailSlapEvent.Create(Bolt.GlobalTargets.Everyone))
                                        {
                                            evnt.TargEnt = be;
                                            evnt.Damage = this.gameObject.GetComponentInParent<PlayerStats>().tailSlapDamage / 2;
                                        }
                                    }

                                    available = false;
                                    lastUsed = Time.time;
                                }

                                //  Debug.Log("BoltEntity.gameObject matches coll.gameObject");

                            }
                        }
                    }
                }
            }
		}
}
