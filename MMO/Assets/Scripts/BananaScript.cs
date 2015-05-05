using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour
{
	[SerializeField]
	public bool
		bananaIsNotUp = false;
	float madness;
	float healthRegained;
		
	void OnTriggerEnter (Collider coll)
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (coll.gameObject.tag == "player") {
			if (bananaIsNotUp == false) {
				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						if (be.gameObject == this.gameObject) {
							this.gameObject.GetComponentInChildren<ParticleSystem> ().enableEmission = false;
                            var evnt = BananaUnavailableEvent.Create(Bolt.GlobalTargets.Everyone);
							evnt.TargEnt = be;
							evnt.BananaIsNotUp = true;
                            evnt.Send();
							
						}
					}
					StartCoroutine ("BananaSpawner");
				}
				madness = coll.GetComponent<PlayerStats> ().maxHealth;
				coll.GetComponent<PlayerStats> ().hp = MadnessReplenishment (madness);
			}
		}
	}
	
	IEnumerator BananaSpawner ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		yield return new WaitForSeconds (60f);
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				if (be.gameObject == this.gameObject) {
					this.gameObject.GetComponentInChildren<ParticleSystem> ().enableEmission = true;
					var evnt = BananaAvailableEvent.Create(Bolt.GlobalTargets.Everyone);
					evnt.TargEnt = be;
					evnt.BananaIsNotUp = false;
                    evnt.Send();
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
