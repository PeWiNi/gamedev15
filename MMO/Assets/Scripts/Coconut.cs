using UnityEngine;
using System.Collections;
using System;
public class Coconut : Bolt.EntityBehaviour<ICoconutState>
{

		public bool isHeld = false;
		public GameObject go = null;
		public Vector3 startPos;
		private int startup = 0;
		BoltEntity thisNut;
		public int coconutId;
		BoltEntity owner;
		BoltConnection connection;
		//Bolt.NetworkId nutNetworkId;



		public override void Attached ()
		{
				state.CoconutTransform.SetTransforms (transform);
				
				
				//state.AddCallback("isHeld", )
		}

//		public override void SimulateController()
//	{
//		Vector3 positionAtm = transform.position;
//		
//		if (isHeld) {
//			using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//				evnt.isPickedUp = true;
//				state.CoconutNetworkId = thisNut.networkId;
//				Debug.Log (nutNetworkId.ToString ());
//				if (go == null) {
//					evnt.CoconutPosition = this.gameObject.transform.position;
//					removeCapture (new Vector3 (0, 0, 0));
//				} else {
//                    evnt.CoconutPosition = go.transform.position; //set y
//                }
//            }
//		} else {
//			isHeld = false;
//        }
//	}
        
        
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
				//CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = new Vector3 (1000, 5, 1000);
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
		void LateUpdate ()
		{
			
				Vector3 positionAtm = transform.position;
            
				if (isHeld) {
						
						//    //Debug.Log("It is held!");
						//    WASD wasd = go.GetComponent<WASD> ();
						//    //int a = wasd.getFacing();
						//    Vector3 position = 
						//new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
						//             wasd.gameObject.transform.position.z);
						//    //transform.position = position;	
						
//						CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = position;
						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.isPickedUp = true;
								//	state.CoconutNetworkId = thisNut.networkId;
								//	Debug.Log (nutNetworkId.ToString ());
								//state.CoconutTransform.Position = transform.position;
								if (go == null) {
										evnt.CoconutPosition = this.gameObject.transform.position;
										removeCapture (new Vector3 (0, 0, 0));
								} else {
										//Debug.Log ("Owner Pos: " + go.transform.position);
										evnt.CoconutPosition = go.transform.position; //set y
								}
						}
						//Debug.Log("It is held!");
//						WASD aa = go.GetComponent<WASD> ();
//						//int a = wasd.getFacing();
//						Vector3 pos = new Vector3 (aa.gameObject.transform.position.x, aa.gameObject.transform.position.y + 15,
//				            aa.gameObject.transform.position.z);
//						transform.position = position;
				} else {
						//state.CoconutIsHeld = false;
						isHeld = false;
//						Vector3 newVec = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
//						transform.position = newVec;	//state.CoconutTransform.Position;
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

		public void setCapture (GameObject go)
		{
				if (!isHeld) {
						this.go = go;
						isHeld = true;
				}
				
				//	state.CoconutIsHeld = true;
		}

		public void removeCapture (Vector3 dropPosition)
		{
				go.GetComponent<StateController> ().isHolding = false;
				this.go = null;
				//state.CoconutIsHeld = false;		
				isHeld = false;
//				Vector3 dropPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
//				transform.position = dropPos;	
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
								
								//this.entity.ReleaseControl ();

								//owner.GetComponent<TestPlayerBehaviour> ().entity.TakeControl ();
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


//		public void coconutOwner ()
//		{
//				Vector3 positionAtm = transform.position;
//		
//				if (isHeld) {
//						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//								evnt.isPickedUp = true;
//								if (go == null) {
//										evnt.CoconutPosition = this.gameObject.transform.position;
//										removeCapture (new Vector3 (0, 0, 0));
//								} else {
//										if (connection == null) {				
//												this.entity.TakeControl ();	
//												//evnt.CoconutPosition = go.transform.position; //set y
//										}
//								}
//						}
//				} else {
//						isHeld = false;				
//				}
//				//	go.GetComponent<TestPlayerBehaviour>().entity.
//		}
//	void setNutNetworkId()
//	{
//		setNutNetworkId = state.CoconutNetworkId;
//	}
}
