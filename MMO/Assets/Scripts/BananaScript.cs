using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour
{
	[SerializeField]
	public bool
		isBananaUp;
	float madness;
	float healthRegained;
	
	// Use this for initialization
	void Start ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (isBananaUp == false) {
			while (entities.MoveNext()) {
				if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
					BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
					if (be.gameObject == this.gameObject) {
						using (var evnt = BananaAvailableEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.TargEnt = be;
							evnt.BananaIsUp = true;
						}
					}
				}
			}
		}
	}
	
	void OnTriggerEnter (Collider coll)
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (coll.gameObject.tag == "player") {
			if (isBananaUp == true) {
				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						if (be.gameObject == this.gameObject) {
							using (var evnt = BananaUnavailableEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
								evnt.BananaIsUp = false;
							}
						}
					}
				}
				madness = coll.GetComponent<PlayerStats> ().maxHealth;
				coll.GetComponent<PlayerStats> ().hp = MadnessReplenishment (madness);
			}
			StartCoroutine ("BananaSpawner");
		}
	}
	
	IEnumerator BananaSpawner ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (isBananaUp == false) {
			yield return new WaitForSeconds (60f);
			while (entities.MoveNext()) {
				if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
					BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
					if (be.gameObject == this.gameObject) {
						using (var evnt = BananaAvailableEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.TargEnt = be;
							evnt.BananaIsUp = true;
						}
					}
				}
			}
		}
	}
	
	float MadnessReplenishment (float health)
	{
		healthRegained = health;
		return healthRegained;
	}
}
