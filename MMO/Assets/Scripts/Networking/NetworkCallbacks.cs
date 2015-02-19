using UnityEngine;
using System.Collections;

[BoltGlobalBehaviourAttribute]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
		public override void SceneLoadLocalDone (string map)
		{
				var pos = new Vector3 (Random.Range (-4, 4), 0, Random.Range (-4, 4));

				BoltNetwork.Instantiate (BoltPrefabs.PlayerLANObject, pos, Quaternion.identity);
		}
}
