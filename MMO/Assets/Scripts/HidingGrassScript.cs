using UnityEngine;
using System.Collections;

public class HidingGrassScript : MonoBehaviour
{
	Vector3 biggestScaledMonguin;
	[SerializeField]
	public float 
		hideReenabledTimer;
	float hideUnenabledTime = 2;
	float hideUnenabledAOETime = 6.5f;

	// Use this for initialization
	void Start ()
	{
		biggestScaledMonguin = new Vector3 (3, 3, 3);
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
				var child = coll.transform.GetChild (5);
				if (coll.GetComponent<StateController> ().gotHitInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						hideReenabledTimer = 0;
						coll.GetComponent<StateController> ().gotHitInHidingGrass = false;
					}
				} else if (coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						hideReenabledTimer = 0;
						coll.GetComponentInChildren<CCScript> ().CCUsedInHidingGrass = false;
					}
				} else if (coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInChildren<TailSlap> ().TailSlapUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else if (coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInChildren<CprScript> ().CprUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else if (coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledAOETime) {
						coll.GetComponentInChildren<AOE> ().AOEUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else if (coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass == true) {
					coll.GetComponent<Renderer> ().enabled = true;
					child.GetComponent<Canvas> ().enabled = true;
					hideReenabledTimer += Time.deltaTime;
					if (hideReenabledTimer >= hideUnenabledTime) {
						coll.GetComponentInParent<TestPlayerBehaviour> ().BoomNanaUsedInHidingGrass = false;
						hideReenabledTimer = 0;
					}
				} else {
					coll.GetComponent<Renderer> ().enabled = false;
					child.GetComponent<Canvas> ().enabled = false;
				}
			}
		}
	}
		
	void OnTriggerExit (Collider coll)
	{
		// should also consider unit size!
		if (coll.gameObject.tag == "player") {
			if (coll.transform.localScale != biggestScaledMonguin) {
				var child = coll.transform.GetChild (5);
				coll.GetComponent<Renderer> ().enabled = true;
				child.GetComponent<Canvas> ().enabled = true;
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
