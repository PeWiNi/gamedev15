using UnityEngine;
using System.Collections;

public class BananaTreeScript : MonoBehaviour
{
		float bananaSpawnTimer;
		GameObject banana;
		GameObject bananaTree;
		bool isBananaUp = true;
		float madness;
		float healthRegained;
		float x;
		float z;

		// Use this for initialization
		void Start ()
		{
				x = 30f; //Random.Range (-20f, +20f);
				z = -5f; //Random.Range (-20f, +20f);
				banana = Resources.Load ("Prefabs/banana") as GameObject; 
				Instantiate (banana);
				banana.transform.position = SpawnBananaPosition ();
		}

		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player") {
						if (isBananaUp == true) {
								isBananaUp = false; 
								madness = coll.GetComponent<PlayerStats> ().maxHealth;
								coll.GetComponent<PlayerStats> ().hp = MadnessReplenishment (madness);
								Destroy (GameObject.Find ("banana(Clone)"));
								StartCoroutine ("BananaSpawner");
						}
				}
		}

		IEnumerator BananaSpawner ()
		{
				if (isBananaUp == false) {
						yield return new WaitForSeconds (10f);
						Instantiate (banana);
						banana.transform.position = SpawnBananaPosition ();
						isBananaUp = true;
				}
		}

		float MadnessReplenishment (float health)
		{
				healthRegained = health;
				return healthRegained;
		}

		Vector3 SpawnBananaPosition ()
		{
				return new Vector3 (x + this.gameObject.transform.position.x, this.gameObject.transform.position.y, z + this.gameObject.transform.position.z);
		}
}
