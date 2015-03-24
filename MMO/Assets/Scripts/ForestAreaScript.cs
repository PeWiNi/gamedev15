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
				yield return new WaitForSeconds (10f);
				if (isThreeBeeHiveUp == false) {
						if (beeHives.Count != 3) {
								Instantiate (beeHiveTrap);
								beeHiveTrap.transform.position = SpawnBeeHivePosition ();
								beeHives.Add (beeHiveTrap);
								Debug.Log ("beeHive list " + beeHives.Count);
								if (beeHives.Count == 3) {
										isThreeBeeHiveUp = true;
								}
								yield return new WaitForSeconds (10f);	
						}
				}
		}

		/// <summary>
		/// Ants the nest spawner.
		/// </summary>
		/// <returns>The nest spawner.</returns>
		IEnumerator AntNestSpawner ()
		{
				yield return new WaitForSeconds (10f);
				if (isTwoAntNestUp == false) {
						if (antNests.Count != 2) {
								Instantiate (antNestTrap);
								antNestTrap.transform.position = SpawnAntNestPosition ();
								antNests.Add (antNestTrap);
								Debug.Log ("antNest list " + antNests.Count);
								if (antNests.Count == 2) {
										isTwoAntNestUp = true; 
								}
								yield return new WaitForSeconds (10f);
						}
				}
		}
	
		/// <summary>
		/// Spawns the bee hive position.
		/// </summary>
		/// <returns>The bee hive position.</returns>
		Vector3 SpawnBeeHivePosition ()
		{
				return new Vector3 (Random.Range (-150, +150) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-150, +150) + forestObject.transform.position.z);
		}

		/// <summary>
		/// Spawns the ant nest position.
		/// </summary>
		/// <returns>The ant nest position.</returns>
		Vector3 SpawnAntNestPosition ()
		{
				return new Vector3 (Random.Range (-150, +150) + forestObject.transform.position.x, forestObject.transform.position.y, Random.Range (-150, +150) + forestObject.transform.position.z);
		}
}
