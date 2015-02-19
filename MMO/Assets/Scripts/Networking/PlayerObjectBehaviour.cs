using UnityEngine;
using System.Collections;

public class PlayerObjectBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
		public override void Attached ()
		{
				state.PlayerTransform.SetTransforms (transform);

				if (entity.isOwner) {
						state.PlayerColor = new Color (Random.value, Random.value, Random.value);
				}

				state.AddCallback ("PlayerColor", ColorChanged);
		}

		public override void SimulateOwner ()
		{
				var ms = 4f;
				var mpos = Vector3.zero;
				if (Input.GetKey (KeyCode.W)) {
						mpos.z += 1;
				}
				if (Input.GetKey (KeyCode.S)) {
						mpos.z -= 1;
				}
				if (Input.GetKey (KeyCode.A)) {
						mpos.x -= 1;
						
				}
				if (Input.GetKey (KeyCode.D)) {
						mpos.x += 1;
				}
				if (mpos != Vector3.zero) {
						transform.position = transform.position + (mpos.normalized * ms * BoltNetwork.frameDeltaTime);
				}

		}

		public void ColorChanged ()
		{
				renderer.material.color = state.PlayerColor;
		}

		void OnGUI ()
		{
				if (entity.isOwner) {
						GUI.color = state.PlayerColor;
						GUILayout.Label ("@@@");
						GUI.color = Color.white;
				}
		}

}
