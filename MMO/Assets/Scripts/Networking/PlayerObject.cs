using UnityEngine;
using System.Collections;

public class PlayerObject
{
	public BoltEntity character;
	public BoltConnection connection;
	public int teamId;

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
						
			character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3dWithColliders);
			if (MenuScript.hasPickedTeamOne) {
				//character.renderer.material.color = Color.red;
                teamId = 1;

                foreach (SkinnedMeshRenderer smr in character.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    smr.material.mainTexture = Resources.Load<Texture>("Textures/Layer_lambert1_u1_v2_Diffuse_merged_wNoise_Fish");
                    smr.material.SetTexture(1, Resources.Load<Texture>("Textures/Layer_lambert1_u1_v2_Diffuse_merged_wNoise_Fish_normal"));
                }
			} else if (MenuScript.hasPickedTeamTwo) {
				// character.renderer.material.color = Color.green;
                teamId = 2;

                foreach (SkinnedMeshRenderer smr in character.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    smr.material.mainTexture = Resources.Load<Texture>("Textures/Layer_lambert1_u1_v2_Diffuse_merged_wNoise_Banana");
                    smr.material.SetTexture(1, Resources.Load<Texture>("Textures/Layer_lambert1_u1_v2_Diffuse_merged_wNoise_Banana_normal"));
                }
			}
			if (isServer) {
				//character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
				character.TakeControl ();
				//Coconut.Instantiate ();
			} else if (isClient) {
				//	character = BoltNetwork.Instantiate (BoltPrefabs.PlayerObject3d);
				character.AssignControl (connection);
				GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.AssignControl (connection);
			}
		}
				
		// teleport entity to a random spawn position
		if (MenuScript.hasPickedTeamOne == true) {
			character.transform.position = SpawnRandomPositionTeamOne ();
		} else if (MenuScript.hasPickedTeamTwo == true) {
            character.transform.position = SpawnRandomPositionTeamTwo();
		}
	}
	
	Vector3 SpawnRandomPositionTeamOne ()
	{
		float x = Random.Range (-10f, +10f);
		float z = Random.Range (-10f, +10f);
		character.gameObject.GetComponent<PlayerStats> ().respawnPosition = new Vector3 (x + 580, 15f, z + 200);
		return new Vector3 (x + 580, 5f, z + 200);
	}
		
	Vector3 SpawnRandomPositionTeamTwo ()
	{
		float x = Random.Range (-10f, +10f);
		float z = Random.Range (-10f, +10f);
		character.gameObject.GetComponent<PlayerStats> ().respawnPosition = new Vector3 (x - 600, 15f, z - 200);
		return new Vector3 (x + (-600), 5f, z + (-200));
	}
}
