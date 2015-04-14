using UnityEngine;
using System.Collections;

public class ThornyVineScript : MonoBehaviour
{
	float speed;
	float originalMovementSpeed;
	float increaseMovementSpeed;
	GameObject player;
	float health;
	float healthRemain;
	float maxHealth;
	bool isDealingDamage;
			
	// Use this for initialization
	void Start ()
	{
		originalMovementSpeed = 2f;
		increaseMovementSpeed = 2f;
	}
	
	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerStay (Collider coll)
	{				
		if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath01" && !isDealingDamage) {
			health = coll.GetComponent<PlayerStats> ().hp;
			maxHealth = coll.GetComponent<PlayerStats> ().maxHealth;
			StartCoroutine ("VineDamage", health);
			coll.GetComponent<PlayerStats> ().hp = healthRemain;
			originalMovementSpeed = coll.GetComponent<StateController> ().movementspeed;
			speed = originalMovementSpeed + increaseMovementSpeed;
			coll.GetComponent<StateController> ().movementspeed = speed;
		}
		if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath02" && !isDealingDamage) {
			health = coll.GetComponent<PlayerStats> ().hp;
			maxHealth = coll.GetComponent<PlayerStats> ().maxHealth;
			StartCoroutine ("VineDamage", health);
			coll.GetComponent<PlayerStats> ().hp = healthRemain;
			originalMovementSpeed = coll.GetComponent<StateController> ().movementspeed;
			speed = originalMovementSpeed + increaseMovementSpeed;
			coll.GetComponent<StateController> ().movementspeed = speed;
		}
	}

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath01") {
			coll.GetComponent<StateController> ().movementspeed = originalMovementSpeed;
		}
		if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath02") {
			coll.GetComponent<StateController> ().movementspeed = originalMovementSpeed;
		}
	}

	/// <summary>
	/// Vines the damage.
	/// </summary>
	/// <returns>The damage.</returns>
	/// <param name="health">Health.</param>
	public IEnumerator VineDamage (float health)
	{
		isDealingDamage = true;
		float dmg = (maxHealth * 1) / 100;
		healthRemain = health - dmg;
		yield return new WaitForSeconds (1f);
		isDealingDamage = false;
	}

}
