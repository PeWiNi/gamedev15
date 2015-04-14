using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerObjectReg
{
	public  List<PlayerObject> playerObjects = new List<PlayerObject> (); 
//		public static List<PlayerObject> teamOnePlayerObjects = new List<PlayerObject> ();
//		public static List<PlayerObject> teamTwoPlayerObjects = new List<PlayerObject> ();
		
	PlayerObject createPlayer (BoltConnection connection)
	{
		PlayerObject po;

		po = new PlayerObject ();
		po.connection = connection;

		if (po.connection != null) {
			po.connection.UserData = po;
		}
		playerObjects.Add (po);
//				if (BoltInit.hasPickedTeamOne) {
//						teamOnePlayerObjects.Add (po);
//				} else if (BoltInit.hasPickedTeamTwo) {
//						teamTwoPlayerObjects.Add (po);
//				}
		return po;
	}

//		public static IEnumerable<PlayerObject> allTeamOnePlayerObjects {
//				get { return teamOnePlayerObjects; }
//		}
//
//		public static IEnumerable<PlayerObject> allTeamTwoPlayerObjects {
//				get { return teamTwoPlayerObjects; }
//		}

	public  IEnumerable<PlayerObject> allPlayerObjects {
		get { return playerObjects; }
	}

//		public static PlayerObject serverTeamOnePlayerObject {
//				get {
//						return teamOnePlayerObjects.First (x => x.isServer);
//				}
//		}
//
//		public static PlayerObject serverTeamTwoPlayerObject {
//				get { return teamTwoPlayerObjects.First (x => x.isServer); }
//		}

	public  PlayerObject serverPlayerObject {
		get { return playerObjects.First (x => x.isServer); }
	}

	public  PlayerObject createServerPlayerObject ()
	{
		return createPlayer (null);
	}

	public  PlayerObject createClientPlayerObject (BoltConnection connection)
	{
		return createPlayer (connection);
	}

	public  PlayerObject getPlayerObject (BoltConnection connection)
	{
		if (connection == null) {
			return serverPlayerObject;
		}
		return (PlayerObject)connection.UserData;
	}

//		public static void DestoryTeamOnePlayerOnDisconnection (BoltConnection connection)
//		{
//				foreach (PlayerObject p in teamOnePlayerObjects.ToArray()) {
//						if (p.connection == connection) {
//								//BoltNetwork.Destroy (p.gameObject);
//								teamOnePlayerObjects.Remove (p);
//						}
//				}
//		}
//
//
//		public static void DestoryTeamTwoPlayerOnDisconnection (BoltConnection connection)
//		{
//				foreach (PlayerObject p in teamTwoPlayerObjects.ToArray()) {
//						if (p.connection == connection) {
//								//BoltNetwork.Destroy (p.gameObject);
//								teamTwoPlayerObjects.Remove (p);
//						}
//				}
//		}


	public  void DestoryOnDisconnection (BoltConnection connection)
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
