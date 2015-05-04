using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
	//IList<string> logMessages = new List<string> ();
	BoltConnection connection;
	Vector3 position;
	Bolt.NetworkId id;
	PlayerObjectReg por;
	bool playerDC = false;
	// BoltEntity lastSplitter;

	public void updateStats ()
	{
		int teamOneMembers = 0;
		int teamTwoMembers = 0;
		// statspliter.splitStats();
		// playerobjectreg -> get all characters.

		// FOR SOME REASON, THE PLAYEROBJECTREG ONLY CONTAINS 1 OBJECT .. IT IS NOT COMMON OVER THE SERVER!!!!
		IEnumerator boltEnum = BoltNetwork.entities.GetEnumerator ();
		int a = 0;
		IEnumerator theEnum = null;
		while (boltEnum.MoveNext()) {
			if (boltEnum.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity boltEnt = (BoltEntity)boltEnum.Current as BoltEntity;
				if (boltEnt.gameObject.tag == "player") {
					if (boltEnt.gameObject.GetComponent<PlayerStats> ().teamNumber == 1) {
						teamOneMembers++;
					} else {
						teamTwoMembers++;
					}
					theEnum = (boltEnt.gameObject.GetComponent<PlayerStats> ().getEntities ());
					//break;
				}
			}
			a++; 
			Debug.Log ("Entities found FROM STATSUPDATER = " + a);
		}

		/*if (theEnum != null) {
			int e = 0;
			while (theEnum.MoveNext()) {
				//Debug.Log("Enum from player object: "+ e);
				//e++; 
				if (theEnum.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
					e++;
					BoltEntity be = (BoltEntity)theEnum.Current as BoltEntity;
					//Debug.Log(be.gameObject.tag);
					if (be.gameObject.tag == "player") {
						if (be.gameObject.GetComponent<PlayerStats> ().teamNumber == 1) {
							teamOneMembers++;
						} else {
							teamTwoMembers++;
						}
					}
				}
			}
			Debug.Log ("Number of PLAYERS found: " + e + ", T1 = "+teamOneMembers+", T2 = "+ teamTwoMembers);
		}*/
		/* IEnumerator enumer = por.allPlayerObjects.GetEnumerator();
            // go through each entity, and check for teamnumber.
            while (enumer.MoveNext())
            {
                Debug.Log("INSIDE ENUMER"); 
                if (enumer.Current.GetType().IsInstanceOfType(new PlayerObject()))
                {
                    Debug.Log("IT IS A PlayerObject!!");

                    PlayerObject po = (PlayerObject) enumer.Current as PlayerObject;

                    if (po.teamId == 1)
                    {
                        Debug.Log("TEAM 1");
                        teamOneMembers++;
                    }
                    else if (po.teamId == 2)
                    {
                        Debug.Log("TEAM 2");
                        teamTwoMembers++; 
                    }
                }
            }*/
		Debug.Log (teamTwoMembers + " at team 2, " + teamOneMembers + " at team1");

		StatSplitter sp = new StatSplitter ();
		sp.splitStats (teamOneMembers);
		sp.splitScale (teamOneMembers);
		IEnumerator players = por.allPlayerObjects.GetEnumerator ();
		int currentPlayerIndex = 0;

		IEnumerator boltEnumTwo = BoltNetwork.entities.GetEnumerator ();
		while (boltEnumTwo.MoveNext()) {
			if (boltEnumTwo.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity boltEnt = (BoltEntity)boltEnumTwo.Current as BoltEntity;
				if (boltEnt.gameObject.tag == "player") {
					if (boltEnt.gameObject.GetComponent<PlayerStats> ().teamNumber == 1) {

						using (var evnt = StatUpdateEvent.Create(Bolt.GlobalTargets.Everyone)) {
							// High Hp = low Tail
							// High Boom = low Aoe
							evnt.MaxHp = (float)sp.hpValues [currentPlayerIndex];
							evnt.TailDamage = (float)sp.tailValues [(sp.tailValues.Count - 1) - currentPlayerIndex];
							evnt.BoomDamage = (float)sp.boomValues [currentPlayerIndex];
							evnt.AoeDamage = (float)sp.aoeValues [(sp.aoeValues.Count - 1) - currentPlayerIndex];
							evnt.TargEnt = boltEnt;
						}
						using (var evnt = ScaleEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.Scale = (float)sp.scaleFactor;
							evnt.TargEnt = boltEnt;
						}
						currentPlayerIndex++;

					}
					//theEnum = (boltEnt.gameObject.GetComponent<PlayerStats> ().getEntities ());
					//break;
				}
			}
		}



		/*while (players.MoveNext()) {
			PlayerObject player = (PlayerObject)players.Current as PlayerObject;
			if (player.teamId == 1) {
				using (var evnt = StatUpdateEvent.Create(Bolt.GlobalTargets.Everyone)) {
					// High Hp = low Tail
					// High Boom = low Aoe
					evnt.MaxHp = (float)sp.hpValues [currentPlayerIndex];
					evnt.TailDamage = (float)sp.tailValues [(sp.tailValues.Count - 1) - currentPlayerIndex];
					evnt.BoomDamage = (float)sp.boomValues [currentPlayerIndex];
					evnt.AoeDamage = (float)sp.aoeValues [(sp.aoeValues.Count - 1) - currentPlayerIndex];
					evnt.TargEnt = player.character;
				}
				using (var evnt = ScaleEvent.Create(Bolt.GlobalTargets.Everyone)) {
					evnt.Scale = (float)sp.scaleFactor;
					evnt.TargEnt = player.character;
				}
			}
			currentPlayerIndex++;
		}*/

		sp = new StatSplitter ();
		sp.splitStats (teamTwoMembers);
		sp.splitScale (teamTwoMembers);
		currentPlayerIndex = 0;
		players = por.allPlayerObjects.GetEnumerator ();
		IEnumerator boltEnumThree = BoltNetwork.entities.GetEnumerator ();
		while (boltEnumThree.MoveNext()) {
			if (boltEnumThree.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				BoltEntity boltEnt = (BoltEntity)boltEnumThree.Current as BoltEntity;
				if (boltEnt.gameObject.tag == "player") {
					if (boltEnt.gameObject.GetComponent<PlayerStats> ().teamNumber == 2) {
						
						using (var evnt = StatUpdateEvent.Create(Bolt.GlobalTargets.Everyone)) {
							// High Hp = low Tail
							// High Boom = low Aoe
							evnt.MaxHp = (float)sp.hpValues [currentPlayerIndex];
							evnt.TailDamage = (float)sp.tailValues [(sp.tailValues.Count - 1) - currentPlayerIndex];
							evnt.BoomDamage = (float)sp.boomValues [currentPlayerIndex];
							evnt.AoeDamage = (float)sp.aoeValues [(sp.aoeValues.Count - 1) - currentPlayerIndex];
							evnt.TargEnt = boltEnt;
						}
						using (var evnt = ScaleEvent.Create(Bolt.GlobalTargets.Everyone)) {
							evnt.Scale = (float)sp.scaleFactor;
							evnt.TargEnt = boltEnt;
						}
						currentPlayerIndex++;
						
					}
					//theEnum = (boltEnt.gameObject.GetComponent<PlayerStats> ().getEntities ());
					//break;
				}
			}
		}/*
		while (players.MoveNext()) {
			PlayerObject player = (PlayerObject)players.Current as PlayerObject;
			if (player.teamId == 2) {
				using (var evnt = StatUpdateEvent.Create(Bolt.GlobalTargets.Everyone)) {
					// High Hp = low Tail
					// High Boom = low Aoe
					evnt.MaxHp = (float)sp.hpValues [currentPlayerIndex];
					evnt.TailDamage = (float)sp.tailValues [(sp.tailValues.Count - 1) - currentPlayerIndex];
					evnt.BoomDamage = (float)sp.boomValues [currentPlayerIndex];
					evnt.AoeDamage = (float)sp.aoeValues [(sp.aoeValues.Count - 1) - currentPlayerIndex];
					evnt.TargEnt = player.character;
				}
				using (var evnt = ScaleEvent.Create(Bolt.GlobalTargets.Everyone)) {
					evnt.Scale = (float)sp.scaleFactor;
					evnt.TargEnt = player.character;
				}
			}
			currentPlayerIndex++;
		}*/

	}

	void Awake ()
	{
		por = new PlayerObjectReg ();
            
		//PlayerObjectReg.createCoconutObject ();//.Spawn ();
		por.createServerPlayerObject ();
		//updateStats();
		//Coconut.Instantiate ();
		//PlayerObjectReg.co.Spawn ();
		//	PlayerObjectReg.createCoconutObject ().Spawn ();
	}

	public override void Connected (BoltConnection connection)
	{
		#region Announcement
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();

		hs.announcementText.text = "Player connected!";
		hs.announcementText.color = new Color (hs.announcementText.color.r, hs.announcementText.color.g, hs.announcementText.color.b, 1.0f);
		#endregion

		Debug.Log ("connected");
		por.createClientPlayerObject (connection);
		//updateStats();




		// boltnetwork.entities -> assign stats for all.
		//this.connection = connection;
//				var log = LogEvent.Create ();
//				log.Message = string.Format ("{0} connected", connection.RemoteEndPoint);
//				log.Send ();
	}
	
	public override void Disconnected (BoltConnection connection)
	{
		#region Announcement
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();

		hs.announcementText.text = "Player disconnected!";
		hs.announcementText.color = new Color (hs.announcementText.color.r, hs.announcementText.color.g, hs.announcementText.color.b, 1.0f);
		#endregion

		Debug.Log ("DISCONNECTED PLAYER");
		por.DestoryOnDisconnection (connection);
		playerDC = true;
//				if (tpb.state.TeamMemberId == 1) {
//						PlayerObjectReg.DestoryTeamOnePlayerOnDisconnection (connection);
//				} else if (tpb.state.TeamMemberId == 2) {
//						PlayerObjectReg.DestoryTeamTwoPlayerOnDisconnection (connection);
//				}
//				var log = LogEvent.Create ();
//				log.Message = string.Format ("{0} disconnected", connection.RemoteEndPoint);
//				log.Send ();
		//updateStats ();
	}

	public override void SceneLoadLocalDone (string map)
	{
//				BoltNetwork.Instantiate (BoltPrefabs.Coconut_1, new Vector3 (1000f, 5f, 1000f), Quaternion.identity);
//				if (BoltInit.hasPickedTeamOne && connection == null) {
//						PlayerObjectReg.serverTeamOnePlayerObject.Spawn ();
//		
//				} else if (BoltInit.hasPickedTeamTwo && connection == null) {
//						PlayerObjectReg.serverTeamTwoPlayerObject.Spawn ();
//				}
		//	PlayerObjectReg.serverPlayerObject.Spawn ();
		por.getPlayerObject (connection).Spawn ();
		//updateStats(); 
		//PlayerObjectReg.createCoconutObject ().Spawn ();
		Debug.Log ("objects" + por.playerObjects.Count);
	}

	public override void OnEvent (ScaleEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.transform.localScale = new Vector3 (evnt.Scale, evnt.Scale, evnt.Scale);
	}

	public override void OnEvent (StatStartEvent evnt)
	{
		//lastSplitter = evnt.TargEnt;
		updateStats (); 
	}

	public override void OnEvent (CoconutEvent evnt)
	{
		//id = WASD.WASDNetworkId; 
		//Debug.Log ("AFSADADASD");
		//Debug.Log ("I FOUND THE NAME!! READ ME!!!!:" + GameObject.FindGameObjectWithTag ("nut").name);
		//Debug.Log ("TESTING..  She says: " + GameObject.FindGameObjectWithTag ("nut").GetComponent<CoconutManager> ().test ());
		//GameObject.FindGameObjectWithTag ("nut").GetComponent<CoconutManager> ().ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);	
		BoltSingletonPrefab<CoconutManager> cm = CoconutManager.instance;
		cm.GetComponent<CoconutManager> ().ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);//.instance.ApplyMovementToNut (evnt.CoconutId, evnt.CoconutPosition);	
	}

	public override void OnEvent (StatUpdateEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<PlayerStats> ().setSplitStats (evnt.MaxHp, evnt.BoomDamage, evnt.TailDamage, evnt.AoeDamage);
	}

	public override void OnEvent (TailSlapEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.Damage);
	}

	public override void OnEvent (CoconutEffectEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<PlayerStats> ().hasCoconutEffect = evnt.isAffectedByCoconut;
		target.gameObject.GetComponent<PlayerStats> ().coconutEffectDuration = evnt.CoconutEffectDuration;
		target.gameObject.GetComponent<PlayerStats> ().ccDuration = evnt.CoconutCCBuffDuration;
		target.gameObject.GetComponent<PlayerStats> ().tailSlapDamage = evnt.CoconutTailSlapDmgBuff;
		target.gameObject.GetComponent<PlayerStats> ().boomNanaDamage = evnt.CoconutBoomnanaDmgBuff;
		target.gameObject.GetComponent<PlayerStats> ().aoeTickDamageFactor = evnt.CoconutAOEDmgBuff;
		target.gameObject.GetComponent<PlayerStats> ().stoppedInCoconutConsume = evnt.StoppedInCoconutConsume;

	}

	public override void OnEvent (CoconutAvailableEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.transform.GetComponent<Renderer> ().enabled = true;
		target.gameObject.GetComponent<CoconutEffect> ().isCoconutNotConsumed = evnt.isCoconutNotConsumed;
	}

	public override void OnEvent (CoconutUnavailableEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.transform.GetComponent<Renderer> ().enabled = false;
		target.gameObject.GetComponent<CoconutEffect> ().isCoconutNotConsumed = evnt.isCoconutNotConsumed;
	}

	public override void OnEvent (UnhideInGrassEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.transform.GetChild (2).gameObject.SetActive (true);
		target.transform.GetChild (8).GetComponent<Canvas> ().enabled = true;
		target.transform.GetChild (9).gameObject.SetActive (true);
	}

	public override void OnEvent (HidingInGrassEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.transform.GetChild (2).gameObject.SetActive (false);
		target.transform.GetChild (8).GetComponent<Canvas> ().enabled = false;
		target.transform.GetChild (9).gameObject.SetActive (false);
	}

	public override void OnEvent (PlayerBeingBuffedEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<PlayerStats> ().canPickUpCoconut = evnt.CanPickUpCoconut;
	}
		
	public override void OnEvent (BeaconCapturingEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<BeaconCaptureScript> ().captureValue = evnt.CaptureValue;
	}

	public override void OnEvent (BeaconUnderControlEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<BeaconCaptureScript> ().beaconUnderTeamOneControl = evnt.BeaconUnderTeamOneControl;
		target.gameObject.GetComponent<BeaconCaptureScript> ().beaconUnderTeamTwoControl = evnt.BeaconUnderTeamTwoControl;
		target.gameObject.GetComponent<BeaconCaptureScript> ().teamOneIsCapturing = evnt.TeamOneIsCapturing;
		target.gameObject.GetComponent<BeaconCaptureScript> ().teamTwoIsCapturing = evnt.TeamTwoIsCapturing;
		target.gameObject.GetComponent<BeaconCaptureScript> ().beaconIsNeutral = evnt.BeaconIsNeutral;
		target.gameObject.GetComponent<BeaconCaptureScript> ().bonusPointsGiven = evnt.BonusPointsGiven;
	}

	public override void OnEvent (BananaAvailableEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.transform.GetComponent<Renderer> ().enabled = true;
		target.gameObject.GetComponent<BananaScript> ().bananaIsNotUp = evnt.BananaIsNotUp;
	}

	public override void OnEvent (BananaUnavailableEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.transform.GetComponent<Renderer> ().enabled = false;
		target.gameObject.GetComponent<BananaScript> ().bananaIsNotUp = evnt.BananaIsNotUp;
	}
		
	public override void OnEvent (AoeEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.TickDamage);
	}

	public override void OnEvent (BoomEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.Damage);
	}

	public override void OnEvent (CCEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().stun (target.gameObject, evnt.Duration);
	}

	public override void OnEvent (CprEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponentInChildren<CprScript> ().ress ();
	}
    
	public void DealDamage (GameObject reciever, float damage)
	{
		IEnumerator<BoltEntity> enumer = BoltNetwork.entities.GetEnumerator ();
		BoltEntity current = enumer.Current;
		while (current!=null) {
			if (current.gameObject == reciever) {
				Debug.Log ("FOUND TROLOLOLOL");
			} else {
				enumer.MoveNext ();
			}
		}
		reciever.gameObject.GetComponent<StateController> ().attack (reciever.gameObject, 50);
	}

	public override void OnEvent (BeeHiveTrapEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().attack (target.gameObject, evnt.TrapDamage);
	}

	public override void OnEvent (AntNestTrapEvent evnt)
	{
		BoltEntity target = evnt.TargEnt;
		target.gameObject.GetComponent<StateController> ().stun (target.gameObject, evnt.TrapStunDuration);
	}

	public override void OnEvent (GameTimerEvent evnt)
	{
		if (playerDC) {
			updateStats ();
			playerDC = false;
		}
		GameTimeManager.time = evnt.GameTime;
		GameTimeManager.setGameTimer (GameTimeManager.time);
	}

	public override void OnEvent (TeamOneScoreEvent evnt)
	{
		ScoreOneManager.totalOneScore = evnt.TeamOneTotalScore;
		ScoreOneManager.setTeamOneTotalScore (ScoreOneManager.totalOneScore);
	}

	public override void OnEvent (TeamTwoScoreEvent evnt)
	{
		ScoreTwoManager.totalTwoScore = evnt.TeamTwoTotalScore;
		ScoreTwoManager.setTeamTwoTotalScore (ScoreTwoManager.totalTwoScore);
	}
	public override void OnEvent (KillScoreOneEvent evnt)
	{
		ScoreOneManager.addDeathPoints (evnt.Score);
	}
	public override void OnEvent (KillScoreTwoEvent evnt)
	{
		ScoreTwoManager.addDeathPoints (evnt.Score);
	}
		
//		public override void SceneLoadRemoteDone (BoltConnection connection)
//		{
//				//	Debug.Log ("Spawning");
//				//	PlayerObjectReg.getPlayerObject (connection).Spawn ();
//		}

//		public override void OnEvent (CoconutEvent evnt)
//		{
//					
//				if (evnt.isPickedUp == true) {
//						//logMessages.Insert (0, evnt.CoconutPosition.ToString ());
//						
//						//transform.gameObject.GetComponent<Coconut> ().Test ();
//						transform.position = evnt.CoconutPosition;
//						//evnt.		
//						//gameObject.GetComponent ("Coconut 1").transform.position = evnt.CoconutPosition; 
//						//state.transform.position = evnt.CoconutPosition;
//				}
//		}


	//void OnGUI ()
	//		{
	//				int maxMessages = Mathf.Min (5, logMessages.Count);
	//
	//				GUILayout.BeginArea (new Rect (Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
	//
	//				for (int i = 0; i < maxMessages; ++i) {
	//						GUILayout.Label (logMessages [i]);
	//				}
	//
	//				GUILayout.EndArea ();
	//		}


	//		public override void OnEvent (LogEvent evnt)
	//		{
	//				logMessages.Insert (0, evnt.Message);
	//		}
}
