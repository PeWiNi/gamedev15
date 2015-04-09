using UnityEngine;
using System.Collections;

public class BananaTreeNorthScript : MonoBehaviour
{

		float bananaSpawnTimer;
		GameObject bananaNorth;
		GameObject bananaTreeNorth;
		GameObject banana;
		bool isNorthBananaUp = true;
		float madness;
		float healthRegained;
		float x;
		float z;
	
		// Use this for initialization
		void Start ()
		{
				x = 10f; //Random.Range (-20f, +20f);
				z = -5f; //Random.Range (-20f, +20f); 
				bananaNorth = Resources.Load ("Prefabs/ScaledGO/Banana_Scaled") as GameObject;
				//bananaTreeNorth = GameObject.Find ("Banana_Tree_N_Scaled");
				banana = (GameObject)Instantiate (bananaNorth, new Vector3 (x + this.gameObject.transform.position.x, this.gameObject.transform.position.y, z + this.gameObject.transform.position.z), Quaternion.identity);
				banana.name = "bananaNorth";
		}
	
		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "Banana_Tree_N_Scaled") {
						if (isNorthBananaUp == true) {
								isNorthBananaUp = false; 
								madness = coll.GetComponent<PlayerStats> ().maxHealth;
								coll.GetComponent<PlayerStats> ().hp = MadnessReplenishment (madness);
								Destroy (GameObject.Find ("bananaNorth"));
								StartCoroutine ("BananaNorthSpawner");
						}
				}
		}
	
		IEnumerator BananaNorthSpawner ()
		{
				if (isNorthBananaUp == false) {
						yield return new WaitForSeconds (90f);
						banana = (GameObject)Instantiate (bananaNorth, new Vector3 (x + this.gameObject.transform.position.x, this.gameObject.transform.position.y, z + this.gameObject.transform.position.z), Quaternion.identity);
						banana.name = "bananaNorth";
						isNorthBananaUp = true;
				}
		}
	
		float MadnessReplenishment (float health)
		{
				healthRegained = health;
				return healthRegained;
		}
	
		Vector3 SpawnBananaNorthPosition ()
		{
				return new Vector3 (x + this.gameObject.transform.position.x, this.gameObject.transform.position.y, z + this.gameObject.transform.position.z);
		}
}
