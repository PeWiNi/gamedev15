using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
		//IList<string> logMessages = new List<string> ();
		BoltConnection connection;
		

		void Awake ()
		{
				//PlayerObjectReg.createCoconutObject ();//.Spawn ();
				PlayerObjectReg.createServerPlayerObject ();
				//Coconut.Instantiate ();
				//PlayerObjectReg.co.Spawn ();
				//	PlayerObjectReg.createCoconutObject ().Spawn ();
		}

		public override void Connected (BoltConnection connection)
		{
				
				Debug.Log ("connected");
				PlayerObjectReg.createClientPlayerObject (connection);
				//this.connection = connection;
//				var log = LogEvent.Create ();
//				log.Message = string.Format ("{0} connected", connection.RemoteEndPoint);
//				log.Send ();
		}
	
		public override void Disconnected (BoltConnection connection)
		{
			     
				PlayerObjectReg.DestoryOnDisconnection (connection);
//				var log = LogEvent.Create ();
//				log.Message = string.Format ("{0} disconnected", connection.RemoteEndPoint);
//				log.Send ();
		}

		public override void SceneLoadLocalDone (string map)
		{
//				BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000f, 5f, 1000f), Quaternion.identity);
				PlayerObjectReg.serverPlayerObject.Spawn ();
				
				PlayerObjectReg.getPlayerObject (connection).Spawn ();
				//PlayerObjectReg.createCoconutObject ().Spawn ();
		}
    
		//public override void SceneLoadRemoteDone (BoltConnection connection)
		//		{
		//				Debug.Log ("Spawning");
		//				PlayerObjectReg.getPlayerObject (connection).Spawn ();
		//		}

//		public override void OnEvent (CoconutEvent evnt)
//		{
//					
//				if (evnt.isPickedUp == true) {
//						//logMessages.Insert (0, evnt.CoconutPosition.ToString ());
//						
//						//transform.gameObject.GetComponent<Coconut> ().Test ();
//						transform.position = evnt.CoconutPosition;
//						//evnt.		
//						//gameObject.GetComponent ("Coconut 1").transform.position = evnt.CoconutPosition; 
//						//state.transform.position = evnt.CoconutPosition;
//				}
//		}


		//void OnGUI ()
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


		//		public override void OnEvent (LogEvent evnt)
		//		{
		//				logMessages.Insert (0, evnt.Message);
		//		}
}
