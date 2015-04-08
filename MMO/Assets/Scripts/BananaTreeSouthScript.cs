using UnityEngine;
using System.Collections;

public class BananaTreeSouthScript : MonoBehaviour
{
		float bananaSpawnTimer;
		GameObject bananaSouth;
		GameObject bananaTreeSouth;
		GameObject banana;
		bool isSouthBananaUp = true;
		float madness;
		float healthRegained;
		float x;
		float z;

		// Use this for initialization
		void Start ()
		{
				x = 10f; //Random.Range (-20f, +20f);
				z = -5f; //Random.Range (-20f, +20f);
				bananaSouth = Resources.Load ("Prefabs/ScaledGO/Banana_Scaled") as GameObject; 
				bananaTreeSouth = GameObject.Find ("Banana_Tree_S_Scaled");
				banana = (GameObject)Instantiate (bananaSouth, new Vector3 (x + bananaTreeSouth.transform.position.x, bananaTreeSouth.transform.position.y, z + bananaTreeSouth.transform.position.z), Quaternion.identity);
				banana.name = "bananaSouth";
				//bananaSouth.transform.position = new Vector3 (x + bananaTreeSouth.transform.position.x, bananaTreeSouth.transform.position.y, z + bananaTreeSouth.transform.position.z);
		}
		
		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "Banana_Tree_S_Scaled") {
						if (isSouthBananaUp == true) {
								isSouthBananaUp = false; 
								madness = coll.GetComponent<PlayerStats> ().maxHealth;
								coll.GetComponent<PlayerStats> ().hp = MadnessReplenishment (madness);
								Destroy (GameObject.Find ("bananaSouth"));
								StartCoroutine ("BananaSouthSpawner");
						}
				}
		}

		IEnumerator BananaSouthSpawner ()
		{
				if (isSouthBananaUp == false) {
						yield return new WaitForSeconds (5f);
						banana = (GameObject)Instantiate (bananaSouth, new Vector3 (x + bananaTreeSouth.transform.position.x, bananaTreeSouth.transform.position.y, z + bananaTreeSouth.transform.position.z), Quaternion.identity);
						banana.name = "bananaSouth";
						//Instantiate (bananaSouth);
						//bananaSouth.transform.position = new Vector3 (x + bananaTreeSouth.transform.position.x, bananaTreeSouth.transform.position.y, z + bananaTreeSouth.transform.position.z);
						isSouthBananaUp = true;
				}
		}

		float MadnessReplenishment (float health)
		{
				healthRegained = health;
				return healthRegained;
		}

		Vector3 SpawnBananaSouthPosition ()
		{
				return new Vector3 (x + bananaTreeSouth.transform.position.x, bananaTreeSouth.transform.position.y, z + bananaTreeSouth.transform.position.z);
		}
}
