using UnityEngine;
using System.Collections;

public class CoconutEffect : MonoBehaviour
{
		public bool isCoconutConsumed = false; 
	

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
		}
		
		void OnTriggerStay (Collider coll)
		{
				if (coll.gameObject.tag == "player") {
						coll.GetComponent<PlayerStats> ().IsInCoconutArea = true;
						if (isCoconutConsumed == true) {
								coll.GetComponent<PlayerStats> ().IsInCoconutArea = false;
								this.gameObject.transform.renderer.enabled = false;
						}
				}
		}

		void OnTriggerExit (Collider coll)
		{
				if (coll.gameObject.tag == "player") {
						coll.GetComponent<PlayerStats> ().IsInCoconutArea = false;
				}

		
		}
}