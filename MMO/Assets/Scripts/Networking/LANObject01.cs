using UnityEngine;
using System.Collections;

public class LANObject01 : MonoBehaviour
{
		public NetworkPlayer theOwner;
//		public KeyCode moveUp;// = KeyCode.W;
//		public KeyCode moveDown;// = KeyCode.S;
//		public KeyCode moveRight;// = KeyCode.D;
//		public KeyCode moveLeft;// = KeyCode.A;
		public Vector3 position;
//		private int ms = 5;
//		private Vector3 gravity = new Vector3 (0.0f, 0.0f, 0.0f);
		private Quaternion rotation = new Quaternion ();
		float lastClientHInput = 0f;
		float lastClientVInput = 0f;
		float serverCurrentHInput = 0f;
		float serverCurrentVInput = 0f;
		//private IntergratedMovement intMove;

		void Awake ()
		{
				if (Network.isClient) {
						enabled = false;
				}
		}

		void Start ()
		{


		}

		[RPC]
		void SetPlayer (NetworkPlayer player)
		{
				theOwner = player;
				if (player == Network.player) {
						enabled = true;
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (theOwner != null && Network.player == theOwner) {

//						SendMovementInput ();
						//KeepSteady ();
						float HInput = Input.GetAxis ("Horizontal");
						float VInput = Input.GetAxis ("Vertical");
						if (lastClientHInput != HInput || lastClientVInput != VInput) {
								lastClientHInput = HInput;
								lastClientVInput = VInput;
						}
						if (Network.isServer) {
								//KeepSteady ();
								SendMovementInput (HInput, VInput);

						} else if (Network.isClient) {
								networkView.RPC ("SendMovementInput", RPCMode.Server, HInput, VInput);
				
						}
				}
				if (Network.isServer) {
						//	SendMovementInput ();
						//KeepSteady ();
						Vector3 moveDirection = new Vector3 (serverCurrentHInput, 0, serverCurrentVInput);
						float speed = 5;
						transform.Translate (speed * moveDirection * Time.deltaTime);
				}
		}

//		[RPC]
//		public void MovementControls ()
//		{
//				//position = transform.position; 
//				if (Input.GetKey (moveUp)) {
//						position.z += ms;
//				}
//				if (Input.GetKey (moveDown)) {
//						position.z -= ms;
//				}
//				if (Input.GetKey (moveRight)) {
//						position.x += ms;
//			
//				}
//				if (Input.GetKey (moveLeft)) {
//						position.x -= ms;
//				}
//		}

		[RPC]
		void SendMovementInput (float HInput, float VInput)
		{
//				position = transform.position; 
//				if (Input.GetKey (moveUp)) {
//						position.z += ms;
//				}
//				if (Input.GetKey (moveDown)) {
//						position.z -= ms;
//				}
//				if (Input.GetKey (moveRight)) {
//						position.x += ms;
//			
//				}
//				if (Input.GetKey (moveLeft)) {
//						position.x -= ms;
//				}
				//MovementControls ();	
				serverCurrentHInput = HInput;
				serverCurrentVInput = VInput;
		}

		
		void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
		{
				if (stream.isWriting) {
						Vector3 pos = transform.position;
						stream.Serialize (ref pos);
				} else {
						Vector3 posReceive = Vector3.zero;
						stream.Serialize (ref posReceive);//"Decode" it and receive it
						transform.position = posReceive;
				}
		}

		public void KeepSteady ()
		{
				rotation.y = 0;
				//rotation.x = 0;
				//rotation.z = 0;
				transform.rotation = rotation;	
		}

		
}
