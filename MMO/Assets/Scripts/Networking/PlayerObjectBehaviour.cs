using UnityEngine;
using System.Collections;

public class PlayerObjectBehaviour : Bolt.EntityEventListener<IPlayerState>
{
	public GameObject[] AbilityObjects;
	float resetColorTime;
	GameObject owner;
	GameObject thisObj;
	Vector3 start;
	Vector3 direction;
	GameObject boomnana;
	Boomnana boomscript;
		
	void Start ()
	{
//				boomnana = Instantiate (Resources.Load ("Prefabs/Boomnana", typeof(GameObject)) as GameObject,
//		                        new Vector3 (transform.position.x + 20, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
//				boomnana.GetComponent<Boomnana> ();
	}

	void Update ()
	{
		if (resetColorTime < Time.time) {
			GetComponent<Renderer> ().material.color = state.PlayerColor;
		}
	}

	public override void Attached ()
	{
		state.PlayerTransform.SetTransforms (transform);

		if (entity.isOwner) {
			state.PlayerColor = new Color (Random.value, Random.value, Random.value);

			for (int i = 0; i < state.AbilityArray.Length; ++i) {
				state.AbilityArray [i].AbilityId = Random.Range (0, AbilityObjects.Length);
			}
			state.AbilityActiveIndex = 0;
		}

		state.AddCallback ("PlayerColor", ColorChanged);

		state.AddCallback ("AbilityActiveIndex", AbilityActiveIndexChanged);
	}

	public override void SimulateOwner ()
	{

				
//				if (Input.GetKey (KeyCode.E)) {
//						
//						boomscript.spawn (owner, thisObj, start, direction);
//				}

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

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			state.AbilityActiveIndex = 0;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			state.AbilityActiveIndex = 1;
		}
				

		if (Input.GetKeyDown (KeyCode.F)) {
			var flash = FlashColorEvent.Create (entity);
			flash.FlashColor = Color.red;
			flash.Send ();
		}

	}

	public void ColorChanged ()
	{
		GetComponent<Renderer> ().material.color = state.PlayerColor;
	}

	public override void OnEvent (FlashColorEvent evnt)
	{
		resetColorTime = Time.time + 0.25f;
		GetComponent<Renderer> ().material.color = evnt.FlashColor;
	}

	void AbilityActiveIndexChanged ()
	{
		for (int i = 0; i < AbilityObjects.Length; ++i) {
			AbilityObjects [i].SetActive (false);
		}
		
		if (state.AbilityActiveIndex >= 0) {
			int objectId = state.AbilityArray [state.AbilityActiveIndex].AbilityId;
			AbilityObjects [objectId].SetActive (true);
		}
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
