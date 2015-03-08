using UnityEngine;
using System.Collections;

public class PlayerObject
{
		public BoltEntity character;
		public BoltConnection connection;
		public int teamId;

		public bool isServer {
				get { return connection == null;}
		}

		public bool isClient {
				get { return connection != null;}
		}

		public void Spawn ()
		{
				//character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
				if (!character) {
						
						character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
						if (BoltInit.hasPickedTeamOne) {
								character.renderer.material.color = Color.red;
								teamId = 1;
						} else if (BoltInit.hasPickedTeamTwo) {
								character.renderer.material.color = Color.green;
								teamId = 2;
						}
						if (isServer) {
								//character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
								character.TakeControl ();
								//Coconut.Instantiate ();
						} else if (isClient) {
								//	character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
								character.AssignControl (connection);
								GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.AssignControl (connection);
						}
				}
		
				// teleport entity to a random spawn position
				character.transform.position = RandomPosition ();
		}
	
		Vector3 RandomPosition ()
		{
				float x = Random.Range (-32f, +32f);
				float z = Random.Range (-32f, +32f);
				return new Vector3 (x + 1000f, 5f, z + 1000f);
		}

}
