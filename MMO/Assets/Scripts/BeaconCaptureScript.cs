using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeaconCaptureScript : MonoBehaviour
{
	[SerializeField]
	public List<GameObject>
		teamOneCaptureList = new List<GameObject> ();
	[SerializeField]
	public List<GameObject>
		teamTwoCaptureList = new List<GameObject> ();
	[SerializeField]
	public bool
		beaconUnderTeamOneControl;
	[SerializeField]
	public bool
		beaconUnderTeamTwoControl;
	[SerializeField]
	public bool
		teamOneIsCapturing;
	[SerializeField]
	public bool
		teamTwoIsCapturing;
	[SerializeField]
	bool
		pointIsIncreased;
	[SerializeField]
	public float
		capturingVal = 10;
	[SerializeField]
	public float
		teamOneCaptureValue;
	[SerializeField]
	public float
		teamTwoCaptureValue;
	[SerializeField]
	float
		capturePointTimer = 2;
	[SerializeField]
	float
		captureBeaconTimer;
	[SerializeField]
	float
		activeTime = 200f;
	[SerializeField]
	public float
		beaconActivatedTime;
	[SerializeField]
	public float
		beaconOneHealth;
	[SerializeField]
	public float
		beaconTwoHealth;



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		increasingCapturingValue ();
		capturingBeacon ();
		if (beaconUnderTeamOneControl == true || beaconUnderTeamTwoControl == true) {
			beaconActivatedTime += Time.deltaTime;
			Debug.Log ("" + beaconActivatedTime);
			if (beaconActivatedTime >= activeTime) {
				activatedBeaconExpire ();
			}
		}
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone01") {
			teamOneCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone01") {
			teamTwoCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone02") {
			teamOneCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone02") {
			teamTwoCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone03") {
			teamOneCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone03") {
			teamTwoCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone04") {
			teamOneCaptureList.Add (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone04") {
			teamTwoCaptureList.Add (coll.gameObject);
		}
	}

	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone01") {
			teamOneCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone01") {
			teamTwoCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone02") {
			teamOneCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone02") {
			teamTwoCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone03") {
			teamOneCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone03") {
			teamTwoCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 1 && this.gameObject.name == "BeaconZone04") {
			teamOneCaptureList.Remove (coll.gameObject);
		}
		if (coll.gameObject.tag == "player" && coll.gameObject.GetComponent<PlayerStats> ().teamNumber == 2 && this.gameObject.name == "BeaconZone04") {
			teamTwoCaptureList.Remove (coll.gameObject);
		}
	}
	
	void capturingBeacon ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (teamOneCaptureValue == capturingVal) {
			while (entities.MoveNext()) {
				if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
					BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
					// Create Event and use the be, if it is the one that is colliding.
					if (be.gameObject == this.gameObject) {
						using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {							
							evnt.TargEnt = be;
							evnt.BeaconUnderTeamOneControl = true;
							evnt.BeaconTeamOneHealth = 2000;
						}
					}
				}
			}
		}
		if (teamTwoCaptureValue == capturingVal) {
			while (entities.MoveNext()) {
				if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
					BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
					// Create Event and use the be, if it is the one that is colliding.
					if (be.gameObject == this.gameObject) {
						using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {							
							evnt.TargEnt = be;
							evnt.BeaconUnderTeamTwoControl = true;
							evnt.BeaconTeamTwoHealth = 2000;
						}
					}
				}
			}
		}
	}

	void activatedBeaconExpire ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				// Create Event and use the be, if it is the one that is colliding.
				if (be.gameObject == this.gameObject) {
					using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {							
						evnt.TargEnt = be;
						evnt.BeaconUnderTeamOneControl = false;
						evnt.BeaconUnderTeamTwoControl = false;
						evnt.BeaconTeamOneHealth = 0;
						evnt.BeaconTeamTwoHealth = 0;
					}
					if (be.gameObject == this.gameObject) {
						using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
							evnt.TargEnt = be;
							evnt.TeamOneCaptureValue = 0;
							evnt.TeamTwoCaptureValue = 0;
						}
					}
				}
			}
		}
	}

	void increasingCapturingValue ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (beaconUnderTeamOneControl == false && beaconUnderTeamTwoControl == false) {
			if (teamOneCaptureList.Count > teamTwoCaptureList.Count) {
				pointIsIncreased = true;
				if (pointIsIncreased == true) {
					captureBeaconTimer += Time.deltaTime;
					if (captureBeaconTimer >= capturePointTimer) {
						if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 1) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamOneCaptureValue = teamOneCaptureValue + 1;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue;
										}
									}
								}
							}
						} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 2) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamOneCaptureValue = teamOneCaptureValue + 2;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue;
										}
									}
								}
							}
						} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 3) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamOneCaptureValue = teamOneCaptureValue + 3;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue;
										}
									}
								}
							}
						} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 4) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamOneCaptureValue = teamOneCaptureValue + 4;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue;
										}
									}
								}
							}
						} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) >= 5) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamOneCaptureValue = teamOneCaptureValue + 5;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue;
										}
									}
								}
							}
						}
						captureBeaconTimer = 0;
						pointIsIncreased = false;
					}
				}
			}
			if (teamOneCaptureList.Count < teamTwoCaptureList.Count) {
				pointIsIncreased = true;
				if (pointIsIncreased == true) {
					captureBeaconTimer += Time.deltaTime;
					if (captureBeaconTimer >= capturePointTimer) {
						if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 1) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue + 1;
											evnt.TeamOneCaptureValue = teamOneCaptureValue;
										}
									}
								}
							}
						} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 2) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue + 2;
											evnt.TeamOneCaptureValue = teamOneCaptureValue;
										}
									}
								}
							}
						} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 3) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue + 3;
											evnt.TeamOneCaptureValue = teamOneCaptureValue;
										}
									}
								}
							}
						} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 4) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue + 4;
											evnt.TeamOneCaptureValue = teamOneCaptureValue;
										}
									}
								}
							}
						} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) >= 5) {
							while (entities.MoveNext()) {
								if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
									BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.TeamTwoCaptureValue = teamTwoCaptureValue + 5;
											evnt.TeamOneCaptureValue = teamOneCaptureValue;
										}
									}
								}
							}
						}
					}
					captureBeaconTimer = 0;
					pointIsIncreased = false;
				}
			}
		}
	}
}


