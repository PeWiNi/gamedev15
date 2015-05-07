using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour("TutorialLevel", "Scene1TestNew")]
public class PlayerCallbacks : Bolt.GlobalEventListener
{
	public override void SceneLoadLocalDone (string map)
	{
		PlayerCam.Instantiate ();
	}

	public override void ControlOfEntityGained (BoltEntity arg)
	{
		PlayerCam.instance.SetTarget (arg);
	}
	 

}
