using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{

		BoltEntity nut;
		public override void SceneLoadLocalDone (string map)
		{
				CoconutManager.Instantiate ();
				//Debug.Log (CoconutManager.instance == null);

		}
		
}
