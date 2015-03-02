using UnityEngine;
using System.Collections;

public class CoconutObject
{

		public BoltEntity coconut;
		public BoltConnection connection;
	
		public bool isServer {
				get { return connection == null;}
		}
	
		public bool isClient {
				get { return connection != null;}
		}
	
		public void Spawn ()
		{
				if (isServer && !coconut) {
						coconut = BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000f, 5f, 1000f), Quaternion.identity);
						coconut.TakeControl ();

				}

				
				// teleport entity to a random spawn position
				//coconut.transform.position = new Vector3 (1000f, 5f, 1000f);
		}
}
