using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

		void OnGUI ()
		{
			
				GUILayout.BeginArea (new Rect (10, 10, Screen.width - 20, Screen.height - 20));

				if (GUILayout.Button ("Start server", GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
						BoltLauncher.StartServer (UdpKit.UdpEndPoint.Parse ("10.25.177.231:27000"));
						//BoltNetwork.LoadScene ("NetworkScene01");
						BoltNetwork.LoadScene ("Scene1Test");
						//BoltLauncher.StartClient ();
						//BoltNetwork.Connect (UdpKit.UdpEndPoint.Parse ("127.0.0.1:27000"));
				}

				if (GUILayout.Button ("Start Client", GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
						BoltLauncher.StartClient ();
						BoltNetwork.Connect (UdpKit.UdpEndPoint.Parse ("10.25.177.231:27000"));
				}

				GUILayout.EndArea ();
		}

//	public bool SelectTeamOne()
//	{
//
//	}
//
//	public bool SelectTeamTwo()
//	{
//
//	}
}
