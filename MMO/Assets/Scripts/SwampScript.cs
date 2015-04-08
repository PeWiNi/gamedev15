using UnityEngine;
using System.Collections;

public class SwampScript : MonoBehaviour
{
		float speed;
		
		// Use this for initialization
		void Start ()
		{
		}

		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerEnter (Collider coll)
		{				
				if (coll.gameObject.tag == "player" && this.gameObject.name == "Swamp") {
						speed = (coll.GetComponent<StateController> ().movementspeed * 40f) / 100f;
						Debug.Log (speed);
						coll.GetComponent<StateController> ().movementspeed = speed;
				}
		}
	
		/// <summary>
		/// Raises the trigger exit event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerExit (Collider coll)
		{
				if (coll.gameObject.tag == "player" && this.gameObject.name == "Swamp") {
						speed = (coll.GetComponent<StateController> ().movementspeed * 100f) / 40f;
						coll.GetComponent<StateController> ().movementspeed = speed;
				}
		}
}
