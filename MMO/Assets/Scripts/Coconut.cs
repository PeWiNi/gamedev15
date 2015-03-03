using UnityEngine;
using System.Collections;
using System;
public class Coconut : Bolt.EntityBehaviour<ICoconutState>
{

		public bool isHeld = false;
		private GameObject go = null;
		public Vector3 startPos;
		private int startup = 0;
		BoltEntity thisNut;
		int coconutId;
		BoltEntity owner;


		public override void Attached ()
		{
				state.CoconutTransform.SetTransforms (transform);
				
				//state.AddCallback("isHeld", )
		}

//		public override void SimulateController ()
//		{
//				Vector3 positionAtm = transform.position;
//				if (isHeld) {
//			
//						//Debug.Log("It is held!");
//						WASD wasd = go.GetComponent<WASD> ();
//						//int a = wasd.getFacing();
//						Vector3 position = 
//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//				             wasd.gameObject.transform.position.z);
//						transform.position = position;	
//						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//								evnt.isPickedUp = true;
//								evnt.CoconutPosition = transform.position;
//						}
//						//			//Debug.Log("It is held!");
//						//			WASD wasd = go.GetComponent<WASD> ();
//						//			//int a = wasd.getFacing();
//						//			Vector3 position = 
//						//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//						//				            wasd.gameObject.transform.position.z);
//						//			transform.position = position;
//				} else {
//						//Debug.Log("not held");
//						Vector3 newVec = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
//						transform.position = newVec;
//			
//				}
//				if (startup == 0) {
//						Start ();
//				}	
//
//				startup = 1;
//		}

		public void assignOwner (BoltEntity owner)
		{
				this.owner = owner;
		}


		// Use this for initialization
		void Start ()
		{
				isHeld = false;
				//state.CoconutIsHeld = false;
				startPos = transform.position;
				coconutId = gameObject.GetInstanceID ();
				
				//thisNut = BoltNetwork.FindEntity (state.CoconutNetworkId);
		}

		public bool isHeldAtm ()
		{
				return isHeld;
		}
		public GameObject getHolder ()
		{
				return go; 
		}

		public void updateNutPositionRemote (Vector3 newPos)
		{
				transform.position = newPos;
		}

		public void updateNutPositionLocal (Vector3 newPos)
		{
				transform.position = newPos;
		}

		// Update is called once per frame
		void Update ()
		{
			
				Vector3 positionAtm = transform.position;
				if (isHeld) {
						
						//Debug.Log("It is held!");
						WASD wasd = go.GetComponent<WASD> ();
						//int a = wasd.getFacing();
						Vector3 position = 
					new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
					             wasd.gameObject.transform.position.z);
						transform.position = position;	
						
//						CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = position;
//						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//								evnt.isPickedUp = true;
//								//state.CoconutTransform.Position = transform.position;
//								evnt.CoconutPosition = transform.position;
//						}
						//Debug.Log("It is held!");
//						WASD aa = go.GetComponent<WASD> ();
//						//int a = wasd.getFacing();
//						Vector3 pos = new Vector3 (aa.gameObject.transform.position.x, aa.gameObject.transform.position.y + 15,
//				            aa.gameObject.transform.position.z);
//						transform.position = position;
				} else {
						Vector3 newVec = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
						transform.position = newVec;	//state.CoconutTransform.Position;
						//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;

				}
		}

//		public void TakingCoconut (int coconutId, Vector3 position)
//		{
//				if (coconutId == GameObject.FindGameObjectWithTag ("nut").GetInstanceID ()) {
//
//				}
//				Vector3 positionAtm = transform.position;
//				if (isHeld) {
//						//Debug.Log("It is held!");
//						WASD wasd = go.GetComponent<WASD> ();
//						//int a = wasd.getFacing();
//						position = 
//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//				             wasd.gameObject.transform.position.z);
//						transform.position = position;	
//				} else {
//						//Debug.Log("not held");
//			
//						//Debug.Log (nut.gameObject.name);
//						position = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
//						transform.position = position;
//				}
//
//		}
//		public void Test ()
//		{
//				Vector3 positionAtm = transform.position;
//				if (isHeld) {
//			
//						//Debug.Log("It is held!");
//						WASD wasd = go.GetComponent<WASD> ();
//						//int a = wasd.getFacing();
//						Vector3 position = 
//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//				             wasd.gameObject.transform.position.z);
//						transform.position = position;	
//						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//								evnt.isPickedUp = true;
//								evnt.CoconutPosition = transform.position;
//						}
//						//			//Debug.Log("It is held!");
//						//			WASD wasd = go.GetComponent<WASD> ();
//						//			//int a = wasd.getFacing();
//						//			Vector3 position = 
//						//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//						//				            wasd.gameObject.transform.position.z);
//						//			transform.position = position;
//				} else {
//						//Debug.Log("not held");
//						Vector3 newVec = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
//						transform.position = newVec;		
//				}
//		}

		public void setCapture (GameObject go)
		{
				this.go = go;
				isHeld = true;
				//	state.CoconutIsHeld = true;
		}

//		public void test (BoltEntity entity)
//		{
//				isHeld = true;
//		}

		public void removeCapture (Vector3 dropPosition)
		{
				go.GetComponent<StateController> ().isHolding = false;
				this.go = null;
				//state.CoconutIsHeld = false;		
				isHeld = false;
				Vector3 dropPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				transform.position = dropPos;	
//				CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = dropPos;
//				CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = false;
//				using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//						evnt.isPickedUp = false;
//						evnt.CoconutPosition = state.CoconutTransform.Position;
//				}
		}
		void OnTriggerStay (Collider coll)
		{
				if (Input.GetKeyDown (KeyCode.Q)) {
						if (coll.gameObject.Equals (GameObject.FindWithTag ("player") as GameObject) && isHeld) {
								Debug.Log ("Q pressed");
								//GameObject.FindWithTag ("nut").GetComponent<Coconut> ().entity.TakeControl ();
//								coll.GetComponent<WASD> ();
								
								this.entity.ReleaseControl ();

								owner.GetComponent<TestPlayerBehaviour> ().entity.TakeControl ();
								removeCapture (new Vector3 ());
								//this.gameObject.GetComponent<WASD> ().entity.TakeControl ();
								//								foreach (BoltEntity b in BoltNetwork.entities) {
								//										GameObject bGo = b.gameObject;
								//										if (bGo.tag == "player") {
								//												if (bGo.FindComponent<WASD> ().isOwnerOfNut ()) {
								//														//nut = b.GetComponent<Coconut> ();
								//														Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
								//														b.gameObject.GetComponent<WASD> ().removeNutCapture (newPos);
								//														sc.isHolding = false;
								//												}
								//										}						
								//								}
						}
				}

		}
}
