using UnityEngine;
using System.Collections;

public class ThornyVineScript : MonoBehaviour
{
		float speed;
		float decreaseMovementSpeed;
		float increaseMovementSpeed;
		GameObject player;
		float health;
		float healthRemain;
		float maxHealth;

		bool isDealingDamage;
			
		// Use this for initialization
		void Start ()
		{
				decreaseMovementSpeed = 2f;
				increaseMovementSpeed = 2f;
				maxHealth = 100;
		}
	
		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerStay (Collider coll)
		{				
				if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath01" && !isDealingDamage) {
						health = coll.GetComponent<PlayerStats> ().hp;
						StartCoroutine ("VineDamage", health);
						coll.GetComponent<PlayerStats> ().hp = healthRemain;
						speed = ((2f * 20f) / 100f) + increaseMovementSpeed;
						coll.GetComponent<StateController> ().movementspeed = speed;
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath02") {

				}
		}

		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerExit (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath01") {
						coll.GetComponent<StateController> ().movementspeed = decreaseMovementSpeed;
				}
				if (coll.gameObject.tag == "player" && this.gameObject.name == "ThornyVinePath02") {

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
