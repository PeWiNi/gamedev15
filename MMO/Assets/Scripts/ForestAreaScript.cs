using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestAreaScript : MonoBehaviour
{
		GameObject forestObject01;
		GameObject forestObject02;
		GameObject forestObject03;
		GameObject beeHiveTrap;
		GameObject antNestTrap;
		public static List<GameObject> beeHives01 = new List<GameObject> ();
		public static List<GameObject> antNests01 = new List<GameObject> ();
		public static List<GameObject> beeHives02 = new List<GameObject> ();
		public static List<GameObject> antNests02 = new List<GameObject> ();
		public static List<GameObject> beeHives03 = new List<GameObject> ();
		public static List<GameObject> antNests03 = new List<GameObject> ();
		public static bool isThreeBeeHiveUpListOne;
		public static bool isThreeBeeHiveUpListTwo;
		public static bool isThreeBeeHiveUpListThree;
		public static bool isTwoAntNestUpListOne;
		public static bool isTwoAntNestUpListTwo;
		public static bool isTwoAntNestUpListThree;
		

		// Use this for initialization
		void Start ()
		{
				
				forestObject01 = GameObject.Find ("ForestArea01");
				forestObject02 = GameObject.Find ("ForestArea02");
				forestObject03 = GameObject.Find ("ForestArea03");
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
				if (beeHives01.Count != 3) {
						GameObject beeHive = (GameObject)Instantiate (beeHiveTrap, new Vector3 (Random.Range (-20, +20) + forestObject01.transform.position.x, forestObject01.transform.position.y, Random.Range (-20, +20) + forestObject01.transform.position.z), Quaternion.identity);
						beeHive.name = "BeeHiveTrap";			
						beeHives01.Add (beeHive);
						//Debug.Log ("the object: " + beeHiveTrap.name);
						//Debug.Log ("beeHive list " + beeHives.Count);
						if (beeHives01.Count == 3) {
								isThreeBeeHiveUpListOne = true;
						}
						yield return new WaitForSeconds (1f);	
				}
				if (beeHives02.Count != 3) {
						GameObject beeHive = (GameObject)Instantiate (beeHiveTrap, new Vector3 (Random.Range (-20, +20) + forestObject02.transform.position.x, forestObject02.transform.position.y, Random.Range (-20, +20) + forestObject02.transform.position.z), Quaternion.identity);
						beeHive.name = "BeeHiveTrap";			
						beeHives02.Add (beeHive);
						//Debug.Log ("the object: " + beeHiveTrap.name);
						//Debug.Log ("beeHive list " + beeHives.Count);
						if (beeHives02.Count == 3) {
								isThreeBeeHiveUpListTwo = true;
						}
						yield return new WaitForSeconds (1f);	
				}
				if (beeHives03.Count != 3) {
						GameObject beeHive = (GameObject)Instantiate (beeHiveTrap, new Vector3 (Random.Range (-20, +20) + forestObject03.transform.position.x, forestObject03.transform.position.y, Random.Range (-20, +20) + forestObject03.transform.position.z), Quaternion.identity);
						beeHive.name = "BeeHiveTrap";			
						beeHives03.Add (beeHive);
						//Debug.Log ("the object: " + beeHiveTrap.name);
						//Debug.Log ("beeHive list " + beeHives.Count);
						if (beeHives03.Count == 3) {
								isThreeBeeHiveUpListThree = true;
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
				if (antNests01.Count != 2) {
						GameObject antNest = (GameObject)Instantiate (antNestTrap, new Vector3 (Random.Range (-20, +20) + forestObject01.transform.position.x, forestObject01.transform.position.y, Random.Range (-20, +20) + forestObject01.transform.position.z), Quaternion.identity);
						antNest.name = "AntNestTrap";
						antNests01.Add (antNest);
						//Debug.Log ("the object: " + antNestTrap.name);
						//Debug.Log ("antNest list " + antNests.Count);
						if (antNests01.Count == 2) {
								isTwoAntNestUpListOne = true; 
						}
						yield return new WaitForSeconds (1f);
				}
				if (antNests02.Count != 2) {
						GameObject antNest = (GameObject)Instantiate (antNestTrap, new Vector3 (Random.Range (-20, +20) + forestObject02.transform.position.x, forestObject02.transform.position.y, Random.Range (-20, +20) + forestObject02.transform.position.z), Quaternion.identity);
						antNest.name = "AntNestTrap";
						antNests02.Add (antNest);
						//Debug.Log ("the object: " + antNestTrap.name);
						//Debug.Log ("antNest list " + antNests.Count);
						if (antNests02.Count == 2) {
								isTwoAntNestUpListTwo = true; 
						}
						yield return new WaitForSeconds (1f);
				}
				if (antNests03.Count != 2) {
						GameObject antNest = (GameObject)Instantiate (antNestTrap, new Vector3 (Random.Range (-20, +20) + forestObject03.transform.position.x, forestObject03.transform.position.y, Random.Range (-20, +20) + forestObject03.transform.position.z), Quaternion.identity);
						antNest.name = "AntNestTrap";
						antNests03.Add (antNest);
						//Debug.Log ("the object: " + antNestTrap.name);
						//Debug.Log ("antNest list " + antNests.Count);
						if (antNests03.Count == 2) {
								isTwoAntNestUpListThree = true; 
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
				return new Vector3 (Random.Range (-20, +20) + forestObject01.transform.position.x, forestObject01.transform.position.y, Random.Range (-150, +150) + forestObject01.transform.position.z);
		}

		/// <summary>
		/// Spawns the ant nest position.
		/// </summary>
		/// <returns>The ant nest position.</returns>
		Vector3 SpawnAntNestPosition ()
		{
				return new Vector3 (Random.Range (-20, +20) + forestObject01.transform.position.x, forestObject01.transform.position.y, Random.Range (-150, +150) + forestObject01.transform.position.z);
		}
}
