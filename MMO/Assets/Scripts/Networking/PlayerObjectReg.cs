using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PlayerObjectReg
{
		public static List<PlayerObject> playerObjects = new List<PlayerObject> (); 
//		public static CoconutObject co;

		static PlayerObject createPlayer (BoltConnection connection)
		{
				PlayerObject po;

				po = new PlayerObject ();
				po.connection = connection;

				if (po.connection != null) {
						po.connection.UserData = po;
				}

				playerObjects.Add (po);

				return po;
		}

		public static IEnumerable<PlayerObject> allPlayerObjects {
				get { return playerObjects; }
		}

		public static PlayerObject serverPlayerObject {
				get { return playerObjects.First (x => x.isServer); }
		}

		public static PlayerObject createServerPlayerObject ()
		{
				return createPlayer (null);
		}

		public static PlayerObject createClientPlayerObject (BoltConnection connection)
		{
				return createPlayer (connection);
		}

		public static PlayerObject getPlayerObject (BoltConnection connection)
		{
				if (connection == null) {
						return serverPlayerObject;
				}

				return (PlayerObject)connection.UserData;
		}

		public static void DestoryOnDisconnection (BoltConnection connection)
		{
			
				foreach (PlayerObject p in playerObjects.ToArray()) {
						if (p.connection == connection) {
								//BoltNetwork.Destroy (p.gameObject);
								playerObjects.Remove (p);

						}
				}
		}

//		public static CoconutObject createCoconutObject ()
//		{
//				co = new CoconutObject ();
//				return co;
//		}c
}
