using UnityEngine;
using System.Collections;

public class DeathSpawner : MonoBehaviour
{

	/*
     * Init when dead -> StateController.checkIfDead()
     * If Ress(), cancel respawn, from CprScript.
     * 
     * Init-> Get ToD 
     * Update-> check respawnTimer against ToD
     *          if(Time.time - ToD >= respawnTimer){
     *              ress at Start Location!
     *          }
     * 
     */

	float ToD = -1;
	float respawnTimer;


	// Use this for initialization
	void Start ()
	{
		respawnTimer = this.gameObject.GetComponent<StateController> ().respawnTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ToD != -1) {
			if (Time.time - ToD >= respawnTimer) {
				PlayerStats ps = this.gameObject.GetComponent<PlayerStats> ();
				ps.hp = ps.maxHealth;
				this.gameObject.transform.position = ps.respawnPosition;
				this.gameObject.GetComponent<StateController> ().isDead = false;
				this.gameObject.GetComponent<StateController> ().ressStarted = false;
				ToD = -1;
			}
		}
	}

	public void startRespawn ()
	{
		ToD = Time.time;
	}

	public void cancelRespawn ()
	{
		this.gameObject.GetComponent<StateController> ().isDead = false;
		this.gameObject.GetComponent<StateController> ().ressStarted = false;
		ToD = -1;
	}
}
