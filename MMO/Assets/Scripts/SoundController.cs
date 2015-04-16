using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{

	private AudioSource soundPlayer;
	private AudioSource movementPlayer;
	private AudioSource jumpPlayer;
	private AudioSource stunnedPlayer;
	// Audioclips
	public AudioClip boomnanathrowclip;
	public AudioClip jumpclip;
	public AudioClip movementclip;
	public AudioClip stunnedclip; 

	// Use this for initialization 
	void Start ()
	{
		GameObject jP = Instantiate (Resources.Load ("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                            new Vector3 (-1000, 0, -1000), Quaternion.identity) as GameObject;
		jumpPlayer = jP.GetComponent<AudioSource> ();

		GameObject bP = Instantiate (Resources.Load ("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                            new Vector3 (-1000, 0, -1000), Quaternion.identity) as GameObject;
		movementPlayer = bP.GetComponent<AudioSource> ();

		GameObject sP = Instantiate (Resources.Load ("Prefabs/SoundMgr", typeof(GameObject)) as GameObject,
		                            new Vector3 (-1000, 0, -1000), Quaternion.identity) as GameObject;
		stunnedPlayer = sP.GetComponent<AudioSource> ();

		soundPlayer = gameObject.GetComponent<AudioSource> ();
		//Debug.Log (nut.name + "  " + coconut.name);
	}
	// Update is called once per frame
	public AudioSource getSoundPlayer ()
	{
		return soundPlayer;
	}

	public AudioSource getMovementPlayer ()
	{
		return movementPlayer;
	}

	public AudioSource getJumpPlayer ()
	{
		return jumpPlayer;
	}

	public AudioSource getStunnedPlayer ()
	{
		return stunnedPlayer;
	}
}
