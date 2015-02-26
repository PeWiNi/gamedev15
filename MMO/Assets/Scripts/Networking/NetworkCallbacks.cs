using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
		IList<string> logMessages = new List<string> ();
		Bolt.EntityEventListener<ICoconutState> state;
		Vector3 pos;
		BoltEntity nut;
		BoltConnection connection;
	  
		public bool isServer {
				get { return connection == null; }
		}
	
		public bool isClient {
				get { return connection != null; }
		}
	
//		public void Spawn ()
//		{
//				if (!nut) {
//						nut = BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000, 5, 1000), Quaternion.identity);
//			
//						if (isServer) {
////								nut.TakeControl ();
//						}
//				}
//		}

	    
	    
//		public override void SceneLoadLocalDone (string map)
//		{
//			
//				Spawn ();
//				//GameObject player = GameObject.Find ("PlayerObject3d");
//				//Destroy (player);
//				//GameObject player = Instantiate(Resources.Load("Prefabs/PlayerObject3d", typeof(GameObject)) as GameObject,
//				// new Vector3(1000, 5, 1000), Quaternion.identity) as GameObject;
//				var pos = new Vector3 (Random.Range (-100, 100) + 1000, 5, Random.Range (-100, 100) + 1000);
//				//var coconutPos = new Vector3 (Random.Range (-100, 100) + 1000, 5, Random.Range (-100, 100) + 1000);
//				//BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d, pos, Quaternion.identity);
//				//BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, coconutPos, Quaternion.identity);
//				//var pos = new Vector3 (Random.Range (-4,4), 0, Random.Range (-4, 4));
//				//BoltNetwork.Instantiate (BoltPrefabs.PlayerLANObject, pos, Quaternion.identity);
//				//	Destroy (player);
//		}
//
//		public override void SceneLoadRemoteDone (BoltConnection connection)
//		{
//				var coconutPos = new Vector3 (Random.Range (-100, 100) + 1000, 5, Random.Range (-100, 100) + 1000);
//				BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, coconutPos, Quaternion.identity);
//		}

		public override void OnEvent (LogEvent evnt)
		{
				logMessages.Insert (0, evnt.Message);
		}

		public override void OnEvent (CoconutEvent evnt)
		{
			
				if (evnt.isPickedUp == true) {
						logMessages.Insert (0, evnt.CoconutPosition.ToString ());
						
						//gameObject.GetComponent ("Coconut 1").transform.position = evnt.CoconutPosition; 
						//state.transform.position = evnt.CoconutPosition;
				}
		}

//		void OnGUI ()
//		{
//				int maxMessages = Mathf.Min (5, logMessages.Count);
//
//				GUILayout.BeginArea (new Rect (Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
//
//				for (int i = 0; i < maxMessages; ++i) {
//						GUILayout.Label (logMessages [i]);
//				}
//
//				GUILayout.EndArea ();
//		}
}
