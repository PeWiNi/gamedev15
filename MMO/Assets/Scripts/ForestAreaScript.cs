using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestAreaScript : MonoBehaviour
{
		GameObject forestObject;
		GameObject beeHiveTrap;
		GameObject antNestTrap;
		public static List<GameObject> beeHives = new List<GameObject> ();
		public static List<GameObject> antNests = new List<GameObject> ();
		public static bool isThreeBeeHiveUp;
		public static bool isTwoAntNestUp;

		// Use this for initialization
		void Start ()
		{
				forestObject = GameObject.FindWithTag ("forest");
				beeHiveTrap = Resources.Load ("Prefabs/BeeHiveTrap") as GameObject;
				antNestTrap = Resources.Load ("Prefabs/AntNestTrap") as GameObject;
		}
    
		// Update is called once per frame
		void Update ()
		{
				StartCoroutine ("BeeHiveSpawner");
				StartCoroutine ("AntNestSpawner");
		}

		/// <summary>
		/// Bees the hive spawner.
		/// </summary>
		/// <returns>The hive spawner.</returns>
		IEnumerator BeeHiveSpawner ()
		{
				//Debug.Log ("beeHive list " + beeHives.Count);
				if (beeHives.Count != 3) {
						GameObject beeHive = (GameObject)Instantiate (beeHiveTrap, new Vector3 (Random.Range (-20, +20) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-20, +20) + forestObject.transform.position.z), Quaternion.identity);
						beeHive.name = "BeeHiveTrap";			
						beeHives.Add (beeHive);
						//Debug.Log ("the object: " + beeHiveTrap.name);
						//Debug.Log ("beeHive list " + beeHives.Count);
						if (beeHives.Count == 3) {
								isThreeBeeHiveUp = true;
						}
						yield return new WaitForSeconds (1f);	
				}
		}

		/// <summary>
		/// Ants the nest spawner.
		/// </summary>
		/// <returns>The nest spawner.</returns>
		IEnumerator AntNestSpawner ()
		{
				//Debug.Log ("antNest list " + antNests.Count);
				yield return new WaitForSeconds (1f);
				if (antNests.Count != 2) {
						GameObject antNest = (GameObject)Instantiate (antNestTrap, new Vector3 (Random.Range (-20, +20) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-20, +20) + forestObject.transform.position.z), Quaternion.identity);
						antNest.name = "AntNestTrap";
						antNests.Add (antNest);
						//Debug.Log ("the object: " + antNestTrap.name);
						//Debug.Log ("antNest list " + antNests.Count);
						if (antNests.Count == 2) {
								isTwoAntNestUp = true; 
						}
						yield return new WaitForSeconds (1f);
				}
		}
	
		/// <summary>
		/// Spawns the bee hive position.
		/// </summary>
		/// <returns>The bee hive position.</returns>
		Vector3 SpawnBeeHivePosition ()
		{
				return new Vector3 (Random.Range (-20, +20) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-150, +150) + forestObject.transform.position.z);
		}

		/// <summary>
		/// Spawns the ant nest position.
		/// </summary>
		/// <returns>The ant nest position.</returns>
		Vector3 SpawnAntNestPosition ()
		{
				return new Vector3 (Random.Range (-20, +20) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-150, +150) + forestObject.transform.position.z);
		}
}
