using System;
using System.Collections.Generic;
using UnityEngine;


public class CoconutManager : BoltSingletonPrefab<CoconutManager>
{
		static Dictionary<int, CoconutObject> coconutObjects; 
		public static List<GameObject> coconutObjectsList = new List<GameObject> ();
		public string test ()
		{
				return "FUCK YOU!";
		}

//		public CoconutManager ()
//		{
//				foreach (GameObject go in GameObject.FindGameObjectsWithTag("nut")) {
//						// Add the tree to our tree "holder" and use the game object's InstanceID as key
//						//coconutObjects.Add (go.GetInstanceID (), new CoconutObject ());
//						coconutObjectsList.Add (go);
//				}
//		}
		void Start ()
		{
				// Initialise the tree "holder"
				coconutObjects = new Dictionary<int, CoconutObject> ();
		
				// Find all game objects with a "Tree" tag on them
				foreach (GameObject go in GameObject.FindGameObjectsWithTag("nut")) {
						// Add the tree to our tree "holder" and use the game object's InstanceID as key
						coconutObjects.Add (go.GetInstanceID (), new CoconutObject ());
						coconutObjectsList.Add (go);
				}
		
				// We also want to check the health of our trees now and then
				//InvokeRepeating( "CheckTrees", checkTreesInterval, checkTreesInterval );
		}
	
//	int CurrentEpoch () {
//		return (int)(( System.DateTime.UtcNow - new System.DateTime(1970, 1, 1) ).TotalSeconds);
//	}
	
//	void CheckTrees () {
//		// Iterate over each of our proxy tree objects and determine if it should be revived/filled up
//		foreach ( KeyValuePair<int, CoconutObject> coconutObject in coconutObjects ) {
//			if ( coconutObject.Value.health == 0 ) {
//				if ( (CurrentEpoch() - coconutObject.Value.epochWhenDied) > 360 ) { // Revive trees every 5th-ish minute
//					coconutObject.Value.health = 100;
//					coconutObject.Value.epochWhenDied = 0;
//				}
//			}
//		}
//	}
//	
		public void ApplyMovementToNut (int nutId, Vector3 position)
		{
				// Let's see if the tree in question, based on treeId, actually exists
//		CoconutObject coconutObject = coconutObjects.TryGetValue( ;
				//Debug.Log ("Size of coconutObjects: " + coconutObjects.Count);
				//Debug.Log ("List Size = " + coconutObjectsList.Count);

				Debug.Log ("SIZE OF THE DAMNED LIST!!! " + coconutObjects.Count);
				coconutObjectsList [0].transform.position = position;
//				CoconutObject coconutObject;
//				if (coconutObjects.TryGetValue (nutId, out coconutObject) && coconutObjects.Count > 0 && coconutObjectsList.Count > 0) {
//						coconutObjectsList [0].transform.position = coconutObject.cob.GetComponent<Coconut> ().getHolder ().transform.position;
//				}

//				if (coconutObjects.Count > 0 && coconutObjectsList.Count > 0) {
//						coconutObjectsList [0].transform.position = coconutObjects [nutId].cob.GetComponent<Coconut> ().getHolder ().transform.position;
//				}
//				if (coconutObjectsList.Count > 0) {
//						coconutObjectsList [0].transform.position = position;
//				}
			
//				if (coconutObjects.TryGetValue (nutId, out coconutObject)) {
				//Debug.Log ("POSITION FOUND: " + coconutObject.getCoconut ().transform.position);
				//	coconutObject.getCoconut ().transform.position = position;
				// We got a tree, and if it has a positive health value, we can damage it
//			if ( coconutObject.health > 0 ) {
//				coconutObject.health = (byte)Mathf.Clamp( coconutObject.health - damageAmount, 0, 100 );
//				
//				// Did this result in the tree "die"? If so, tell the proxy object when it died
//				if ( coconutObject.health == 0 ) {
//					coconutObject.epochWhenDied = CurrentEpoch();
//				}
				//}
//				}
		}
	
		// Proxy tree object
		public class CoconutObject
		{
				public GameObject cob;
				public Vector3 position;
		
				public CoconutObject ()
				{
						cob = GameObject.FindWithTag ("nut");
				}

				public GameObject getCoconut ()
				{
						return cob;
				}
		}
}

