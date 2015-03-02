using UnityEngine;
using System.Collections;
using System;
public class Coconut : Bolt.EntityBehaviour<ICoconutState>
{

		private bool isHeld = false;
		private GameObject go = null;
		public Vector3 startPos;
		private int startup = 0;
		BoltEntity thisNut;


		public override void Attached ()
		{
				state.CoconutTransform.SetTransforms (transform);
				Debug.Log ("NUT NETWORK ID: " + state.CoconutNetworkId.ToString ());
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



		// Use this for initialization
		void Start ()
		{
				isHeld = false;
				startPos = transform.position;
				thisNut = BoltNetwork.FindEntity (state.CoconutNetworkId);
		}

		public bool isHeldAtm ()
		{
				return isHeld;
		}
		public GameObject getHolder ()
		{
				return go; 
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
						CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = position;
//						using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//								evnt.isPickedUp = true;
//								//state.CoconutTransform.Position = transform.position;
//								evnt.CoconutPosition = transform.position;
//						}
//			//Debug.Log("It is held!");
//			WASD wasd = go.GetComponent<WASD> ();
//			//int a = wasd.getFacing();
//			Vector3 position = 
//				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
//				            wasd.gameObject.transform.position.z);
//			transform.position = position;
				} else {
						//Debug.Log("not held");
						
						//Debug.Log (nut.gameObject.name);
						Vector3 newVec = new Vector3 (positionAtm.x, positionAtm.y, positionAtm.z);
						transform.position = state.CoconutTransform.Position;
						CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = transform.position;

				}
		}

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
		}

//		public void test (BoltEntity entity)
//		{
//				isHeld = true;
//		}

		public void removeCapture ()
		{
				go.GetComponent<StateController> ().isHolding = false;
				this.go = null;
				isHeld = false;
				Vector3 dropPos = new Vector3 (transform.position.x, transform.position.y - 5, transform.position.z);
				transform.position = dropPos;
				CoconutEvent.Create (Bolt.GlobalTargets.Everyone).CoconutPosition = dropPos;
				CoconutEvent.Create (Bolt.GlobalTargets.Everyone).isPickedUp = false;
//				using (var evnt = CoconutEvent.Create(Bolt.GlobalTargets.Everyone)) {
//						evnt.isPickedUp = false;
//						evnt.CoconutPosition = state.CoconutTransform.Position;
//				}
		}
}
