using UnityEngine;
using System;
using System.Collections;

public class SpawnPlayerObject02 : MonoBehaviour
{

		public Transform LANPlayer;
		public ArrayList playerScripts = new ArrayList ();

		void OnServerInitialized ()
		{
				SpawnPlayer (Network.player);
		}

		void OnPlayerConnected (NetworkPlayer player)
		{
				SpawnPlayer (player);
		}

		void SpawnPlayer (NetworkPlayer player)
		{
				string tempPlayerString = player.ToString ();
				int playerNumber = Convert.ToInt32 (tempPlayerString);
				Transform newPlayerTransform = (Transform)Network.Instantiate (LANPlayer, transform.position, transform.rotation, playerNumber);
				playerScripts.Add (newPlayerTransform.GetComponent ("LANObject01"));
				NetworkView theNetworkView = newPlayerTransform.networkView;
				theNetworkView.RPC ("SetPlayer", RPCMode.AllBuffered, player);
		}
}
