using UnityEngine;
using System.Collections;

public class HidingGrassScript : MonoBehaviour
{
	Vector3 biggestScaledMonguin;
	[SerializeField]
	public float 
		hideReenabledTimer;
	public float hideUnenabledTime = 2;
	public float hideUnenabledAOETime = 6.5f;

	// Use this for initialization
	void Start ()
	{
		biggestScaledMonguin = new Vector3 (5, 5, 5);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerStay (Collider coll)
	{
		// should also consider unit size!
		if (coll.gameObject.tag == "player") {
			if (coll.transform.localScale != biggestScaledMonguin) {
				coll.GetComponent<TestPlayerBehaviour> ().isInsideHidingGrass = true;
				var meshChild = coll.transform.GetChild (2);
				var canvasChild = coll.transform.GetChild (8);
				var fishChild = coll.transform.GetChild (9);
				if (coll.GetComponent<StateController> ().gotHitInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						hideReenabledTimer = 0;
						coll.GetComponent<StateController> ().gotHitInHidingGrass = false;
					}
				} else if (coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						hideReenabledTimer = 0;
						coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass = false;
					}
				} else if (coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else if (coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else if (coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledAOETime) {
						coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass = false;
						hideReenabledTimer = 0;
						Debug.Log ("");
					}
				} else if (coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass == true) {
					meshChild.gameObject.SetActive (true);
					canvasChild.GetComponent<Canvas> ().enabled = true;
					fishChild.gameObject.SetActive (true);
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else {
					meshChild.gameObject.SetActive (false);
					canvasChild.GetComponent<Canvas> ().enabled = false;
					fishChild.gameObject.SetActive (false);
				}
			}
		}
	}
		
	void OnTriggerExit (Collider coll)
	{
		// should also consider unit size!
		if (coll.gameObject.tag == "player") {
			if (coll.transform.localScale != biggestScaledMonguin) {
				coll.GetComponent<TestPlayerBehaviour> ().isInsideHidingGrass = false;
				var meshChild = coll.transform.GetChild (2);
				var canvasChild = coll.transform.GetChild (8);
				var fishChild = coll.transform.GetChild (9);
				meshChild.gameObject.SetActive (true);
				canvasChild.GetComponent<Canvas> ().enabled = true;
				fishChild.gameObject.SetActive (true);
				if (coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass == true) {
					coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass = false;
				}
				if (coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass == true) {
					coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass = false;
				}
				if (coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass == true) {
					coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass = false;
				}
				if (coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass == true) {
					coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass = false;
				}
				if (coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass == true) {
					coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass = false;
				}
			}
		}
	}
}
