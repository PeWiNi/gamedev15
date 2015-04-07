using UnityEngine;
using System.Collections;

public class HidingGrassScript : MonoBehaviour
{

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
				// should also consider unit size!
				if (coll.gameObject.tag == "player") {
						var child = coll.transform.GetChild (5);
						coll.renderer.enabled = false;
						child.GetComponent<Canvas> ().enabled = false;
				}
		}
		
		void OnTriggerExit (Collider coll)
		{
				// should also consider unit size!
				if (coll.gameObject.tag == "player") {
						var child = coll.transform.GetChild (5);
						coll.renderer.enabled = true;
						child.GetComponent<Canvas> ().enabled = true;
				}
		}
}
