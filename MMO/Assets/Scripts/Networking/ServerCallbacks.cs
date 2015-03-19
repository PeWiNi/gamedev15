using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{

		public override void SceneLoadLocalDone (string map)
		{
				CoconutManager.Instantiate ();
		}
      
}
