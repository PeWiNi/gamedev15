using UnityEngine;
using System.Collections;

public class AntNest : MonoBehaviour
{
	bool isActivatedByTeamOne = false;
	bool isActivatedByTeamTwo = false;
	bool setUpTimer = false;
	public static bool playerOneIsBuffed;
	public static bool playerTwoIsBuffed;
	public static float buffCcDuration;
	public static float playerBuffCcDuration = 1.35f;
		
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
			if (coll.GetComponent<PlayerStats> ().teamNumber == 1) {
				if (isActivatedByTeamTwo == false) {
					StartCoroutine ("SetupTimer");
					isActivatedByTeamOne = true;
					playerOneIsBuffed = true;
					buffCcDuration = coll.GetComponent<PlayerStats> ().ccDuration;
					if (playerOneIsBuffed == true && coll.GetComponent<PlayerStats> ().trapAntNestBuffed == false) {
						float buffedCcDuration = buffCcDuration * playerBuffCcDuration;
						coll.GetComponent<PlayerStats> ().ccDuration = buffedCcDuration;
						coll.GetComponent<PlayerStats> ().trapAntNestBuffed = true;
						StartCoroutine ("OneNoBuff");
					}
				} else if (isActivatedByTeamTwo == true) {
					if (setUpTimer == true) {
						ForestAreaScript.antNests01.Remove (this.gameObject);
						Destroy (this.gameObject);
						// if the damage isn't the same as 10 %, note; use a bool.
						using (var evnt = AntNestTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
							IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
							while (playerEntities.MoveNext()) {
								if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
									if (be.gameObject == coll.gameObject) {
										evnt.TargEnt = be;
									}
								}
							}
							evnt.TrapStunDuration = (coll.GetComponent<PlayerStats> ().ccDuration + 2);
						} 
					}
				}
			} else if (coll.GetComponent<PlayerStats> ().teamNumber == 2) {
				if (isActivatedByTeamOne == false) {
					StartCoroutine ("SetupTimer");
					isActivatedByTeamTwo = true;
					playerTwoIsBuffed = true;
					buffCcDuration = coll.GetComponent<PlayerStats> ().ccDuration;
					if (playerTwoIsBuffed == true && coll.GetComponent<PlayerStats> ().trapAntNestBuffed == false) {
						float buffedCcDuration = buffCcDuration * playerBuffCcDuration;
						coll.GetComponent<PlayerStats> ().ccDuration = buffedCcDuration;
						coll.GetComponent<PlayerStats> ().trapAntNestBuffed = true;
						StartCoroutine ("TwoNoBuff");
					}
				} else if (isActivatedByTeamOne == true) {
					if (setUpTimer == true) {
						ForestAreaScript.antNests01.Remove (this.gameObject);
						Destroy (this.gameObject);
						// if the damage isn't the same as 10 %, note; use a bool.
						using (var evnt = AntNestTrapEvent.Create(Bolt.GlobalTargets.Everyone)) {				
							IEnumerator playerEntities = BoltNetwork.entities.GetEnumerator ();
							while (playerEntities.MoveNext()) {
								if (playerEntities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)playerEntities.Current as BoltEntity;
									if (be.gameObject == coll.gameObject) {
										evnt.TargEnt = be;
									}
								}
							}
							evnt.TrapStunDuration = (coll.GetComponent<PlayerStats> ().ccDuration + 2);
						} 
					}
				}
			}	
		}
	}

	/// <summary>
	/// Setups the timer.
	/// </summary>
	/// <returns>The timer.</returns>
	IEnumerator SetupTimer ()
	{
		yield return new WaitForSeconds (3.5f);
		setUpTimer = true;
	}
	
	/// <summary>
	/// Deactivates the trap.
	/// </summary>
	/// <returns>The trap.</returns>
	IEnumerator DeactivateTrap ()
	{
		if (isActivatedByTeamOne == true || isActivatedByTeamTwo == true) {
			//should be 5 min wait time.
			yield return new WaitForSeconds (100);
			ForestAreaScript.antNests01.Remove (this.gameObject);
			Destroy (this.gameObject);
		}
	}

	/// <summary>
	/// Raises the no buff event.
	/// </summary>
	IEnumerator OneNoBuff ()
	{
		// couldn't have 2 mins, then the coroutine wouldn't work, but 100 or below works fine
		yield return new WaitForSeconds (90);
		playerOneIsBuffed = false;
	}

	/// <summary>
	/// Twos the no buff.
	/// </summary>
	/// <returns>The no buff.</returns>
	IEnumerator TwoNoBuff ()
	{
		// couldn't have 2 mins, then the coroutine wouldn't work, but 100 or below works fine
		yield return new WaitForSeconds (90);
		playerTwoIsBuffed = false;
	}

//		IEnumerator AvailableWaitTimer ()
//		{
//				//may have the timer to be the same time as for the expire timer which is 5 min.
//				yield return new WaitForSeconds (40f);
//				oneTimeBuffer = true;
//		}
}
