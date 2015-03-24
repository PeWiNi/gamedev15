using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeeHive : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{	

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="coll">Coll.</param>
		void OnTriggerEnter (Collider coll)
		{
				if (coll.gameObject.tag == "player") {
						ForestAreaScript.beeHives.Remove (this.gameObject);
						this.gameObject.SetActive (false);
						//Destroy (this.gameObject);	
						ForestAreaScript.isThreeBeeHiveUp = false;
						Debug.Log ("something something " + ForestAreaScript.beeHives.Count);
				}
		}

		
}
