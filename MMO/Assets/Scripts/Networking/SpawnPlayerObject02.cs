using UnityEngine;
using System;
using System.Collections;

public class SpawnPlayerObject02 : MonoBehaviour
{

		public Transform LANPlayer;
		public GameObject LanPlayer;
		public Hashtable playerScripts = new Hashtable ();
		public ArrayList playerscriptsArr = new ArrayList ();

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
				//Transform newPlayerTransform = (Transform)Network.Instantiate (LANPlayer, transform.position, transform.rotation, playerNumber);			
				//playerscriptsArr.Add (newPlayerTransform.GetComponent ("LANObject01"));
				GameObject go = Network.Instantiate (LanPlayer, Vector3.up * 2, Quaternion.identity, playerNumber) as GameObject;
				NetworkView theNetworkView = go.transform.networkView;
				//GameObject go = newPlayerTransform.Find ("LANPlayer");
				//go.GetComponent("LANPlayer");
				playerScripts [player] = go; //newPlayerTransform.GetComponent ("LanObject01");
				//NetworkView theNetworkView = newPlayerTransform.networkView;
				foreach (DictionaryEntry entry in playerScripts) {
						Debug.Log (entry.Key + " " + entry.Value);
				}
				theNetworkView.RPC ("SetPlayer", RPCMode.AllBuffered, player);
		}

		void OnDisconnectedFromServer (NetworkDisconnection info)
		{
				GameObject[] gos = GameObject.FindGameObjectsWithTag ("LanPlayer");
				foreach (GameObject go in gos) {
						Destroy (go);
				}
		}

		void OnPlayerDisconnected (NetworkPlayer player)
		{
				GameObject playerObj = playerScripts [player] as GameObject;
				Debug.Log ("Clean up after player " + player);
				Network.RemoveRPCs (playerObj.networkView.viewID);
				Network.Destroy (playerObj);
				playerScripts.Remove (player);
		}
}
