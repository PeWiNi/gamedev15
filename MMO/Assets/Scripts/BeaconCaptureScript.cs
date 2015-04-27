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
	public bool
		beaconIsNeutral;
	[SerializeField]
	bool
		pointIsIncreased;
	[SerializeField]
	public float
		maxCapturingVal = 100;
	[SerializeField]
	public float
		minCapturingVal = 0;
	[SerializeField]
	public float
		captureValue;
	[SerializeField]
	float
		captureTimer = 1;
	[SerializeField]
	float
		captureBeaconTimer;
	[SerializeField]
	public float
		beaconActivatedTime;
	[SerializeField]
	public float
		totalScoreTeamOne;
	public float totalScoreTeamTwo;
	[SerializeField]
	float
		teamOneScore;
	[SerializeField]
	float
		oneScore;
	public float teamOneScore01;
	public float teamOneScore02;
	public float teamOneScore03;
	public float teamOneScore04;
	[SerializeField]
	public float
		teamTwoScore;
	[SerializeField]
	float
		twoScore;
	public float teamTwoScore01;
	public float teamTwoScore02;
	public float teamTwoScore03;
	public float teamTwoScore04;
	[SerializeField]
	float
		increaseScoreValue;
	[SerializeField]
	float
		lastTimeOne;
	[SerializeField]
	float
		lastTimeTwo;
	[SerializeField]
	float
		scoreTimer = 2;
	[SerializeField]
	public bool
		bonusPointsGiven;
	[SerializeField]
	float
		bonusPoints = 100; 
	[SerializeField]
	float
		teamOneCapturePoints;
	[SerializeField]
	float
		teamTwoCapturePoints;
	GameObject beacon01;
	GameObject beacon02;
	GameObject beacon03;
	GameObject beacon04;
	bool checkOne;
	bool checkTwo;





	// Use this for initialization
	void Start ()
	{
		increaseScoreValue = 7;
		captureValue = 50;
		beacon01 = GameObject.Find ("BeaconZone01");
		beacon02 = GameObject.Find ("BeaconZone02");
		beacon03 = GameObject.Find ("BeaconZone03");
		beacon04 = GameObject.Find ("BeaconZone04");
	}
	
	// Update is called once per frame
	void Update ()
	{
		increasingCapturingValue ();
		capturingBeacon ();
		checkForBeaconActivation ();
		calculateTeamOneScore ();
		calculateTeamTwoScore ();
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="coll">Coll.</param>
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

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="coll">Coll.</param>
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

	/// <summary>
	/// Capturings the beacon.
	/// </summary>
	void capturingBeacon ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		while (entities.MoveNext()) {
			if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
				if (captureValue > 30 && captureValue < 70) {
					if (beaconIsNeutral == false) {
						if (be.gameObject == this.gameObject) {
							using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
								evnt.BeaconIsNeutral = true;
								evnt.BeaconUnderTeamOneControl = false;
								evnt.BeaconUnderTeamTwoControl = false;
								evnt.TeamOneIsCapturing = false;
								evnt.TeamTwoIsCapturing = false;
								evnt.BonusPointsGiven = false;
							}
						}
					}
				}
				if (captureValue <= 30 && captureValue > 0) {
					if (teamOneIsCapturing == false) {
						if (be.gameObject == this.gameObject) {
							using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
								evnt.BeaconIsNeutral = false;
								evnt.BeaconUnderTeamOneControl = false;
								evnt.BeaconUnderTeamTwoControl = false;
								evnt.TeamOneIsCapturing = true;
								evnt.BonusPointsGiven = false;
							}
						}
					}
				}
				if (captureValue <= minCapturingVal) {
					// Create Event and use the be, if it is the one that is colliding.
					if (beaconUnderTeamOneControl == false) {
						if (be.gameObject == this.gameObject) {
							using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {							
								evnt.TargEnt = be;
								evnt.BeaconIsNeutral = false;
								evnt.BeaconUnderTeamOneControl = true;
								evnt.BeaconUnderTeamTwoControl = false;
								evnt.TeamOneIsCapturing = false;
							}
						}
					}
				}
				if (captureValue >= 70 && captureValue < 100) {
					if (teamTwoIsCapturing == false) {
						if (be.gameObject == this.gameObject) {
							using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {
								evnt.TargEnt = be;
								evnt.BeaconIsNeutral = false;
								evnt.TeamTwoIsCapturing = true;
								evnt.BeaconUnderTeamOneControl = false;
								evnt.BeaconUnderTeamTwoControl = false;
								evnt.BonusPointsGiven = false;
							}
						}
					}
				}
				if (captureValue >= maxCapturingVal) {
					// Create Event and use the be, if it is the one that is colliding.
					if (beaconUnderTeamTwoControl == false) {
						if (be.gameObject == this.gameObject) {
							using (var evnt = BeaconUnderControlEvent.Create(Bolt.GlobalTargets.Everyone)) {							
								evnt.TargEnt = be;
								evnt.BeaconIsNeutral = false;
								evnt.BeaconUnderTeamTwoControl = true;
								evnt.BeaconUnderTeamOneControl = false;
								evnt.TeamTwoIsCapturing = false;
							}
						}
					}
				}
			}
		}
	}


	/// <summary>
	/// Increasings the capturing value.
	/// </summary>
	void increasingCapturingValue ()
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (teamOneCaptureList.Count > teamTwoCaptureList.Count) {
			if (captureValue > minCapturingVal) {
				pointIsIncreased = true;
				if (pointIsIncreased == true) {
					captureBeaconTimer += Time.deltaTime;
					if (captureBeaconTimer >= captureTimer) {
						while (entities.MoveNext()) {
							if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
								BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
								if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 1) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue - 1;
										}
									}
								} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 2) {
							
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue - 2;
										}
									}
								} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) == 3) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue - 4;
										}
									}
								} else if ((teamOneCaptureList.Count - teamTwoCaptureList.Count) >= 4) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue - 8;
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
		if (teamOneCaptureList.Count < teamTwoCaptureList.Count) {
			if (captureValue < maxCapturingVal) {
				pointIsIncreased = true;
				if (pointIsIncreased == true) {
					captureBeaconTimer += Time.deltaTime;
					if (captureBeaconTimer >= captureTimer) {
						while (entities.MoveNext()) {
							if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
								BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
								if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 1) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue + 1;
										}
									}
								} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 2) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue + 2;
										}
									}
								} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) == 3) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue + 4;
										}
									}
								} else if ((teamTwoCaptureList.Count - teamOneCaptureList.Count) >= 4) {
									// Create Event and use the be, if it is the one that is colliding.
									if (be.gameObject == this.gameObject) {
										using (var evnt = BeaconCapturingEvent.Create(Bolt.GlobalTargets.Everyone)) {							
											evnt.TargEnt = be;
											evnt.CaptureValue = captureValue + 8;
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
	

	/// <summary>
	/// Checks for beacon activation.
	/// </summary>
	void checkForBeaconActivation ()
	{
		if (beaconUnderTeamOneControl == true) {
			if (bonusPointsGiven == false) {
				bool haveCalculated = false;
				if (this.gameObject.name == "BeaconZone01") {
					teamOneScore01 = oneScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone02") {
					teamOneScore02 = oneScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone03") {
					teamOneScore03 = oneScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone04") {
					teamOneScore04 = oneScore + bonusPoints;
					haveCalculated = true;
				}
				if (haveCalculated == true) {
					using (var evnt = TeamOneScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TeamOneTotalScore = beacon01.GetComponent<BeaconCaptureScript> ().teamOneScore01 + beacon02.GetComponent<BeaconCaptureScript> ().teamOneScore02 + beacon03.GetComponent<BeaconCaptureScript> ().teamOneScore03 + beacon04.GetComponent<BeaconCaptureScript> ().teamOneScore04;
					}
					bonusPointsGiven = true;
				}
			}
			if (this.gameObject.name == "BeaconZone01") {
				StartCoroutine ("addScoreTeamOne", teamOneScore01);
			}
			if (this.gameObject.name == "BeaconZone02") {
				StartCoroutine ("addScoreTeamOne", teamOneScore02);
			}
			if (this.gameObject.name == "BeaconZone03") {
				StartCoroutine ("addScoreTeamOne", teamOneScore03);
			} 
			if (this.gameObject.name == "BeaconZone04") {
				StartCoroutine ("addScoreTeamOne", teamOneScore04);
			}
		}
		if (beaconUnderTeamTwoControl == true) {
			if (bonusPointsGiven == false) {
				bool haveCalculated = false;
				if (this.gameObject.name == "BeaconZone01") {
					teamTwoScore01 = twoScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone02") {
					teamTwoScore02 = twoScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone03") {
					teamTwoScore03 = twoScore + bonusPoints;
					haveCalculated = true;
				}
				if (this.gameObject.name == "BeaconZone04") {
					teamTwoScore04 = twoScore + bonusPoints;
					haveCalculated = true;
				}
				if (haveCalculated == true) {
					using (var evnt = TeamTwoScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
						evnt.TeamTwoTotalScore = beacon01.GetComponent<BeaconCaptureScript> ().teamTwoScore01 + beacon02.GetComponent<BeaconCaptureScript> ().teamTwoScore02 + beacon03.GetComponent<BeaconCaptureScript> ().teamTwoScore03 + beacon04.GetComponent<BeaconCaptureScript> ().teamTwoScore04;
					}
					bonusPointsGiven = true;
				}
			}
			if (this.gameObject.name == "BeaconZone01") {
				StartCoroutine ("addScoreTeamTwo", teamTwoScore01);
			}
			if (this.gameObject.name == "BeaconZone02") {
				StartCoroutine ("addScoreTeamTwo", teamTwoScore02);
			}
			if (this.gameObject.name == "BeaconZone03") {
				StartCoroutine ("addScoreTeamTwo", teamTwoScore03);
			}
			if (this.gameObject.name == "BeaconZone04") {
				StartCoroutine ("addScoreTeamTwo", teamTwoScore04);
			}
		}
	}

	/// <summary>
	/// Adds the score team two.
	/// </summary>
	/// <returns>The score team two.</returns>
	/// <param name="teamTwoScore">Team two score.</param>
	public IEnumerator addScoreTeamOne (float teamOneScore)
	{
		yield return new WaitForSeconds (10f);
		if (GameTimeManager.time >= 1) {
			oneScore = teamOneScore + increaseScoreValue;
			if (this.gameObject.name == "BeaconZone01") {
				teamOneScore01 = oneScore;
				checkOne = true;
			}
			if (this.gameObject.name == "BeaconZone02") {
				teamOneScore02 = oneScore;
				checkOne = true;
			}
			if (this.gameObject.name == "BeaconZone03") {
				teamOneScore03 = oneScore; 
				checkOne = true;
			}
			if (this.gameObject.name == "BeaconZone04") {
				teamOneScore04 = oneScore;
				checkOne = true;
			}
		}
	}

	/// <summary>
	/// Adds the score team two.
	/// </summary>
	/// <returns>The score team two.</returns>
	/// <param name="teamTwoScore">Team two score.</param>
	public IEnumerator addScoreTeamTwo (float teamTwoScore)
	{
		yield return new WaitForSeconds (10f);
		if (GameTimeManager.time >= 1) {
			twoScore = teamTwoScore + increaseScoreValue;
			if (this.gameObject.name == "BeaconZone01") {
				teamTwoScore01 = twoScore;
				checkTwo = true;
			}
			if (this.gameObject.name == "BeaconZone02") {
				teamTwoScore02 = twoScore;
				checkTwo = true;
			}
			if (this.gameObject.name == "BeaconZone03") {
				teamTwoScore03 = twoScore;
				checkTwo = true;
			}
			if (this.gameObject.name == "BeaconZone04") {
				teamOneScore04 = twoScore;
				checkTwo = true;
			}
		}
	}

	/// <summary>
	/// Calculates the team one score.
	/// </summary>
	public void calculateTeamOneScore ()
	{
		if (beaconUnderTeamOneControl == true) {
			if (bonusPointsGiven == true) {
				lastTimeOne += Time.deltaTime;
				if (lastTimeOne >= scoreTimer) {
					if (checkOne == true) {
						totalScoreTeamOne = beacon01.GetComponent<BeaconCaptureScript> ().teamOneScore01 + beacon02.GetComponent<BeaconCaptureScript> ().teamOneScore02 + beacon03.GetComponent<BeaconCaptureScript> ().teamOneScore03 + beacon04.GetComponent<BeaconCaptureScript> ().teamOneScore04;
						using (var evnt = TeamOneScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.TeamOneTotalScore = totalScoreTeamOne;
						}
						checkOne = false;
					}
					lastTimeOne = 0;
				}
			}
		}
	}
	
	/// <summary>
	/// Calculates the team two score.
	/// </summary>
	public void calculateTeamTwoScore ()
	{
		if (beaconUnderTeamTwoControl == true) {
			if (bonusPointsGiven == true) {
				lastTimeTwo += Time.deltaTime;
				if (lastTimeTwo >= scoreTimer) {
					if (checkTwo == true) {
						totalScoreTeamTwo = beacon01.GetComponent<BeaconCaptureScript> ().teamTwoScore01 + beacon02.GetComponent<BeaconCaptureScript> ().teamTwoScore02 + beacon03.GetComponent<BeaconCaptureScript> ().teamTwoScore03 + beacon04.GetComponent<BeaconCaptureScript> ().teamTwoScore04;
						using (var evnt = TeamTwoScoreEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.TeamTwoTotalScore = totalScoreTeamTwo;
						}
						checkTwo = false;
					}
					lastTimeTwo = 0;
				}
			}
		}
	}

}
