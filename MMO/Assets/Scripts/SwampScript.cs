using UnityEngine;
using System.Collections;

public class SwampScript : MonoBehaviour
{
		float speed;
		public float movementSpeed;
		
		// Use this for initialization
		void Start ()
		{
				movementSpeed = 2f;
		}

		/// <summary>
		/// Raises the trigger stay event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerStay (Collider coll)
		{				
				if (coll.gameObject.tag == "player" && this.gameObject.name == "Swamp") {
						speed = movementSpeed - ((2f * 50f) / 100f);
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
						coll.GetComponent<StateController> ().movementspeed = movementSpeed;
				}
		}
}
