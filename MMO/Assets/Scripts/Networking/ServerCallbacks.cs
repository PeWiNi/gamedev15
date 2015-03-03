using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{
		BoltEntity nut;
		public override void SceneLoadLocalDone (string map)
		{
				//Coconut.Instantiate ();
				//BoltEntity nut = BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000, 5, 1000), Quaternion.identity);
//				nut.ReleaseControl ();
				
		}
	
		public override void ControlOfEntityGained (BoltEntity arg)
		{
				//nut.GetComponent<Coconut> ().assignOwner (arg);
//				nut.ReleaseControl ();
				//	Coconut.instance.test (arg);
		}
		
		
}
