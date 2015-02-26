using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Scene1Test")]
public class ServerCallBacks : Bolt.GlobalEventListener
{
		BoltConnection connection;

		void Awake ()
		{
				PlayerObjectReg.createServerPlayerObject ();
				
		}

		public override void Connected (BoltConnection connection)
		{
				Debug.Log ("connected");
				PlayerObjectReg.createClientPlayerObject (connection);
				this.connection = connection;
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
//				if (connection == null) {
//						//BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000, 5, 1000), Quaternion.identity);
				PlayerObjectReg.serverPlayerObject.Spawn ();
//				} 
		}
	
		public override void SceneLoadRemoteDone (BoltConnection connection)
		{
				Debug.Log ("Spawning");
				PlayerObjectReg.getPlayerObject (connection).Spawn ();
		}

}
