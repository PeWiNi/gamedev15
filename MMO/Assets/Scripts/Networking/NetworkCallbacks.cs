using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
		//IList<string> logMessages = new List<string> ();
		BoltConnection connection;
		Vector3 position;
		Bolt.NetworkId id;
		

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
//				if (tpb.state.TeamMemberId == 1) {
//						PlayerObjectReg.DestoryTeamOnePlayerOnDisconnection (connection);
//				} else if (tpb.state.TeamMemberId == 2) {
//						PlayerObjectReg.DestoryTeamTwoPlayerOnDisconnection (connection);
//				}
//				var log = LogEvent.Create ();
//				log.Message = string.Format ("{0} disconnected", connection.RemoteEndPoint);
//				log.Send ();
		}

		public override void SceneLoadLocalDone (string map)
		{
//				BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000f, 5f, 1000f), Quaternion.identity);
//				if (BoltInit.hasPickedTeamOne && connection == null) {
//						PlayerObjectReg.serverTeamOnePlayerObject.Spawn ();
//		
//				} else if (BoltInit.hasPickedTeamTwo && connection == null) {
//						PlayerObjectReg.serverTeamTwoPlayerObject.Spawn ();
//				}
				//	PlayerObjectReg.serverPlayerObject.Spawn ();
				PlayerObjectReg.getPlayerObject (connection).Spawn ();
				//PlayerObjectReg.createCoconutObject ().Spawn ();
				Debug.Log ("objects" + PlayerObjectReg.playerObjects.Count);
		}

		public override void OnEvent (CoconutEvent evnt)
		{
				//id = WASD.WASDNetworkId; 
				//Debug.Log ("AFSADADASD");
				//Debug.Log ("I FOUND THE NAME!! READ ME!!!!:" + GameObject.FindGameObjectWithTag ("nut").name);
				//Debug.Log ("TESTING..  She says: " + GameObject.FindGameObjectWithTag ("nut").GetComponent<CoconutManager> ().test ());
				//GameObject.FindGameObjectWithTag ("nut").GetComponent<CoconutManager> ().ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);	
				BoltSingletonPrefab<CoconutManager> cm = CoconutManager.instance;
				cm.GetComponent<CoconutManager> ().ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);//.instance.ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);	
		}
		public override void OnEvent (TailSlapEvent evnt)
		{
				BoltEntity target = evnt.TargEnt;
				target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.Damage);
		}

		public override void OnEvent (AoeEvent evnt)
		{
				BoltEntity target = evnt.TargEnt;
				target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.TickDamage);
		}

		public override void OnEvent (BoomEvent evnt)
		{
				BoltEntity target = evnt.TargEnt;
				target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.Damage);
		}


		public override void OnEvent (CCEvent evnt)
		{
				BoltEntity target = evnt.TargEnt;
				target.gameObject.GetComponent<StateController> ().stun (target.gameObject, evnt.Duration);
		}

		public override void OnEvent (CprEvent evnt)
		{
				BoltEntity target = evnt.TargEnt;
				target.gameObject.GetComponentInChildren<CprScript> ().ress ();
		}
    
		public void DealDamage (GameObject reciever, float damage)
		{
				IEnumerator<BoltEntity> enumer = BoltNetwork.entities.GetEnumerator ();
				BoltEntity current = enumer.Current;
				while (current!=null) {
						if (current.gameObject == reciever) {
								Debug.Log ("FOUND TROLOLOLOL");
						} else {
								enumer.MoveNext ();
						}
				}
				reciever.gameObject.GetComponent<StateController> ().attack (reciever.gameObject, 50);
		}

		public override void OnEvent (GameTimerEvent evnt)
		{
				GameTimeManager.time = evnt.GameTime;
				GameTimeManager.setGameTimer (GameTimeManager.time);
		}

		public override void OnEvent (TeamOneScoreEvent evnt)
		{
				ScoreOneManager.totalOneScore = evnt.TeamOneTotalScore;
				ScoreOneManager.setTeamOneTotalScore (ScoreOneManager.totalOneScore);
		}

		public override void OnEvent (TeamTwoScoreEvent evnt)
		{
				ScoreTwoManager.totalTwoScore = evnt.TeamTwoTotalScore;
				ScoreTwoManager.setTeamTwoTotalScore (ScoreTwoManager.totalTwoScore);
		}

//		public override void SceneLoadRemoteDone (BoltConnection connection)
//		{
//				//	Debug.Log ("Spawning");
//				//	PlayerObjectReg.getPlayerObject (connection).Spawn ();
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
