using UnityEngine;
using System.Collections;
using System;

public class WASD : MonoBehaviour
{

	/*TODO: Make state controller, so you don't connect/split during combat.
	 * 
	 * */
	//Bolt.EntityEventListener<ICoconutState> state;
	BoltEntity entity;
	BoltEntity me;
	public float stunnedStart;
	GameObject coconut;
	Coconut nut;
	StateController sc;
	PlayerStats ps;
	int nutId;
	// Use this for initialization
	void Start ()
	{
				
		//coconut = BoltNetwork.Attach (BoltPrefabs.Coconut_1) as GameObject;
		//GameObject.Find ("Coconut 1(Clone)");
		//coconut = GameObject.Find ("Coconut 1(Clone)");
		//nut = coconut.GetComponent<Coconut> ();
		//Debug.Log ("Coconut: " + coconut + ", nutScript: " + nut);
		sc = this.gameObject.GetComponent<StateController> ();
		ps = this.gameObject.GetComponent<PlayerStats> ();
		nutId = (GameObject.FindWithTag ("nut")as GameObject).GetInstanceID ();
		coconut = GameObject.FindWithTag ("nut") as GameObject;
				
				

	}

	// Update is called once per frame
	void Update ()
	{
//				if (nut == null) {
//						//nutId = (GameObject.FindWithTag ("nut")as GameObject).GetInstanceID ();
//						nut = (GameObject.FindWithTag ("nut") as GameObject).GetComponent<Coconut> ();
//				}
//				if (coconut == null || nut == null) {
//						try {
//								coconut = GameObject.FindWithTag ("nut") as GameObject;
//
//								nut = coconut.GetComponent<Coconut> ();
//
//						} catch {
//						}
//				}
//				if (sc.isHolding) {
//						foreach (BoltEntity b in BoltNetwork.entities) {
//								GameObject bGo = b.gameObject;
//								if (bGo.tag == "player") {
//										if (bGo.FindComponent<WASD> ().isOwnerOfNut ()) {
//												//nut = b.gameObject.GetComponent<Coconut> ();
//												Vector3 newPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
//												bGo.gameObject.GetComponent<WASD> ().updateNutPosWhenHeld (newPos);
//												//b.gameObject.GetComponent<WASD> ().setCapture (this.gameObject);
//												//sc.isHolding = true;
//										}
//								}
//				
//						}
//				}
	}

	void OnCollisionEnter (Collision coll)
	{ // Working!!
		if (coll.gameObject.tag.Equals ("Terrain") && sc != null) { 
			sc.isJumping = false;
		}

//				if (Input.GetKey (KeyCode.E)) {
//						if (coll.gameObject.Equals (GameObject.FindWithTag ("nut") as GameObject)) {
//								//Debug.Log (coll.gameObject.name); 
//								nut = coll.gameObject.GetComponent<Coconut> ();
//								if (!nut.isHeldAtm ()) {
//										nut.setCapture (this.gameObject);
//										sc.isHolding = true;
//										//Apply grabbing to nutId
//										//CoconutEvent.Create(
//										//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = true;
//										//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;
////										using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
////												if (evnt.CoconutId == nutId) {
////														evnt.isPickedUp = true;
////														evnt.CoconutPosition = transform.position;
////												}
////										}
//								}
//						}
//				}

		if (coll.gameObject.name.Equals ("Boomnana(Clone)")) {
			if (coll.gameObject.GetComponent<Boomnana> ().owner == this.gameObject) {
				sc.isStunned = true;
				stunnedStart = Time.time;
				Destroy (coll.gameObject); 
			}
		}

//				if (Input.GetKey (KeyCode.Q)) {
//						if (nut.getHolder () != null) {
//								if (nut.getHolder ().Equals (this.gameObject)) {
//										nut.removeCapture ();
//										sc.isHolding = false;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = false;
////										CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;
////										using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
////												evnt.isPickedUp = false;
////												evnt.CoconutPosition = transform.position;
////										}
//								}
//						}
//				}
	}

	public void updateNutPosWhenHeld (Vector3 pos)
	{
		this.nut.updateNutPositionRemote (pos);
	}

	public void updateNutPositionRemote (Vector3 newPos, GameObject go)
	{
		nut.updateNutPositionRemote (newPos);
		nut.setCapture (go);
	}

	public void removeNutCapture (Vector3 pos)
	{
		nut.removeCapture (pos);
	}

	public bool isOwnerOfNut ()
	{
		return (GameObject.FindWithTag ("nut") as GameObject).GetComponent<Coconut> ().entity.isOwner;
	}
}