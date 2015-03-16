using UnityEngine;
using System.Collections;

public class Boomnana : MonoBehaviour {

	public GameObject owner;
	//private GameObject thisObj;
	//private float spawnTime;
//	float lateralspeed; 
	bool movingBack = false; 
	Vector3 endpoint;
	// Use this for initialization
	void Start () {
	
	}

	public void spawn(GameObject owner, GameObject boomnana, Vector3 start, Vector3 direction){
		this.owner = owner;
		//this.thisObj = boomnana;
		transform.position = start;
        endpoint = new Vector3(start.x + direction.x, start.y + direction.y, start.z + direction.z);
        movingBack = false; 
		//spawnTime = Time.time * 1000;
//		Vector2 v2 = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
//		lateralspeed = v2.magnitude;
	}

	// Update is called once per frame
	void Update () { 
		if (transform.position == endpoint) {
			movingBack = true;
		}
		if (movingBack){
			rigidbody.velocity = new Vector3 (0, 0, 0);
			transform.position = Vector3.MoveTowards (transform.position, owner.gameObject.transform.position, 4.0f);
		} else {
			transform.position = Vector3.MoveTowards(transform.position, endpoint, 4.0f);	
		}
	}
    	void OnTriggerEnter(Collider coll)
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
                        // CHECKS IF IT HIT ITSELF
                        if (coll.gameObject == owner)
                        {// STUN THE OWNER
                            using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
                            {
                                evnt.TargEnt = be;
                                evnt.Duration = owner.GetComponent<PlayerStats>().ccDuration;
                            }
                        }
                        else // CHECK IF FRIENDLY OR FOE 
                        {
                            if (coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
                            {
                                // deal full damage!!!
                                using (var evnt = BoomEvent.Create(Bolt.GlobalTargets.Everyone))
                                {
                                    evnt.TargEnt = be;
                                    evnt.Damage = this.owner.GetComponent<PlayerStats>().boomNanaDamage;
                                }
                            }
                            else // check for friendly player, deal 50% dmg.
                            {
                                // deal half damage!!!
                                using (var evnt = BoomEvent.Create(Bolt.GlobalTargets.Everyone))
                                {
                                    evnt.TargEnt = be;
                                    evnt.Damage = this.owner.GetComponent<PlayerStats>().boomNanaDamage / 2;
                                }
                            }
                        }
                        //  Debug.Log("BoltEntity.gameObject matches coll.gameObject");
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }

}
