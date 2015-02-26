using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour
{
		public BoltEntity character;
		public BoltConnection connection;

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
						
						character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d, new Vector3 (1050, 5, 1050), Quaternion.identity);
			
						if (isServer) {
								character.TakeControl ();
						} else {
								character.AssignControl (connection);
						}
				}
		
				// teleport entity to a random spawn position
//				character.transform.position = RandomPosition ();
		}
	
		Vector3 RandomPosition ()
		{
				float x = Random.Range (-32f, +32f);
				float z = Random.Range (-32f, +32f);
				return new Vector3 (x + 1000f, 5f, z + 1000f);
		}

}
