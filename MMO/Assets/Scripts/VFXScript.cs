using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VFXScript : MonoBehaviour
{
	private float secondsTimer = 0;

	// Damage Effect
	public KeyCode redKey = KeyCode.Space; // Change to taking damage event
	public RawImage takingDamageHUD;
	public Camera mainCamera;
	private float HUDTimer = 0;
	public float HUDEffectIntensity = 0.3f;
	public float HUDEffectDecay = 0.004f;

    #region Action Bar
	public GameObject Action1;
	private Image a1Over;
	public string a1Key = "e";
	public float a1Time = 3f;
	private bool a1Cooldown;
	public GameObject Action2;
	private Image a2Over;
	public string a2Key = "q";
	public float a2Time = 3f;
	private bool a2Cooldown;
	public GameObject Action3;
	private Image a3Over;
	public string a3Key = "1";
	public float a3Time = 3f;
	private bool a3Cooldown;
	public GameObject Action4;
	private Image a4Over;
	public string a4Key = "2";
	public float a4Time = 3f;
	private bool a4Cooldown;
	public GameObject Action5;
	private Image a5Over;
	public string a5Key = "3";
	public float a5Time = 3f;
	private bool a5Cooldown;
	public GameObject Action6;
	private Image a6Over;
	public string a6Key = "r";
	public float a6Time = 3f;
	private bool a6Cooldown;
    #endregion

	// Coconut Effect
	public KeyCode burdenKey = KeyCode.E; // Change to Coconut-Held event
	private bool holdingCoconut;
	private GameObject burdenEffect;

	// Boost Effect
	public KeyCode boostKey = KeyCode.Q; // Change to Boost-key
	private GameObject boost;
	private float boostTimer = 0;
	public float boostDuration = 10;
	private float boostAlpha = 0.2f;
	private float boostDecay;
	public Vector3 end;

	// Aim
	public KeyCode aimKey = KeyCode.Z; // Change to Boost-key
	private Transform aim;
	public float range = 25;
	public GameObject GOuser;
	public Boomnana boomscript;
	private Transform user;
	private Vector3 userSize;

	// Use this for initialization
	void Start ()
	{
		// Assumes that the camera is a child of the playerObj. 
		user = mainCamera.transform.parent;
		userSize = user.GetComponent<Collider> ().bounds.size;

		boostDecay = boostAlpha / boostDuration;

		burdenEffect = (GameObject)Instantiate (Resources.Load ("Prefabs/VFX_Burdened"));
		burdenEffect.GetComponent<ParticleSystem> ().Stop ();

		aim = user.GetChild (1);
		aim.GetComponent<Renderer> ().enabled = false;

		buffSetup ();
        
		SetupActionBar ();
	}

	// Update is called once per frame
	void Update ()
	{
		#region Damage Effect
		if (HUDTimer <= 0) // Disable when the effect has ended
			takingDamageHUD.enabled = false;
		if (Input.GetKeyDown (redKey)) { // Change to taking damage event
			//Creates HUDeffect when damaged
			HUDTimer = HUDEffectIntensity;
			takingDamageHUD.enabled = true;

			if (boostTimer > 0)
				damageTakenEffect ("Fish"); //Fish Slam
            else
				damageTakenEffect ("Banana"); //Tail Slap
		}
		takingDamageHUD.color = new Color (takingDamageHUD.color.r, takingDamageHUD.color.g, takingDamageHUD.color.b, HUDTimer);
		HUDTimer -= HUDEffectDecay;
		#endregion

		#region Burden
		if (Input.GetKeyDown (burdenKey)) {
			holdingCoconut = !holdingCoconut;
			if (holdingCoconut)
				burdenEffect.GetComponent<ParticleSystem> ().Play ();
			else
				burdenEffect.GetComponent<ParticleSystem> ().Stop ();
			//burdenEffect.SetActive(holdingCoconut = !holdingCoconut);
		}
		if (holdingCoconut)
			burdenEffect.transform.position = new Vector3 (user.position.x, (user.position.y + (userSize.y / 2)), user.position.z);
		#endregion

		#region Boost
		if (boostTimer <= 0)
			boost.SetActive (false);
		if (Input.GetKeyDown (boostKey)) {
			boostTimer = boostDuration;
			boostAlpha = 0.2f;
			boost.SetActive (true);
		}

		// Calculate in seconds!
		if (boostTimer <= boostDuration && boostTimer > 0) {
			boost.transform.position = new Vector3 (user.position.x, (user.position.y - (userSize.y / 2)), user.position.z);
			boost.GetComponent<ParticleSystem> ().startColor = new Color (boost.GetComponent<ParticleSystem> ().startColor.r, boost.GetComponent<ParticleSystem> ().startColor.g, boost.GetComponent<ParticleSystem> ().startColor.b, boostAlpha);
			secondsTimer += Time.deltaTime;
			if (secondsTimer > 1.0f) {
				buffDecay ();
				secondsTimer -= 1.0f;
			}
		}
		#endregion

		#region Aim / Boomnana
		if (Input.GetKeyDown (aimKey)) {
			//aim = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Burdened"));
			aim.GetComponent<Renderer> ().enabled = true;
			aimOverlay (1, range, 0.5f);
		}
		if (Input.GetKeyUp (aimKey)) {

			Vector3 shot = new Vector3 (user.position.x, user.position.y, gameObject.GetComponentInParent<PlayerStats> ().boomnanaRange + user.position.z);
			Vector3 offset = user.position - shot;
			end = (user.forward * gameObject.GetComponentInParent<PlayerStats> ().boomnanaRange);
			float desiredAngle = user.eulerAngles.y;

			Debug.Log ("Angle = " + desiredAngle);

			Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
			//Vector3 retry = desiredAngle;
            
			Vector3 endPos = (user.position) - (rotation * offset);
			Debug.Log ("EndPos = " + endPos.x + "," + endPos.y + "," + endPos.z);
			Debug.Log ("FIRING BOOMNANA FROM VFX"); 
			boomscript.spawn (GOuser, boomscript.owner, user.position, /*user.forward * range, */endPos); 
			aim.GetComponent<Renderer> ().enabled = false;
		}
		#endregion


		#region ActionBar
		ActionBar (ref a1Over, a1Key, ref a1Cooldown, a1Time);
		ActionBar (ref a2Over, a2Key, ref a2Cooldown, a2Time);
		ActionBar (ref a3Over, a3Key, ref a3Cooldown, a3Time);
		ActionBar (ref a4Over, a4Key, ref a4Cooldown, a4Time);
		ActionBar (ref a5Over, a5Key, ref a5Cooldown, a5Time);
		ActionBar (ref a6Over, a6Key, ref a6Cooldown, a6Time);
		#endregion
	}

	/// <summary>
	/// Method for managing the Alpha-decay of the Boost Particle system.
	/// </summary>
	private void buffDecay ()
	{
		print ("Buff left: " + (boostTimer - 1));
		boostTimer -= 1;
		boostAlpha -= boostDecay;
	}

	/// <summary>
	/// Method for setting up the Particle System for when activating AMP ability.
	/// </summary>
	private void buffSetup ()
	{
		//Create Particle System
		boost = (GameObject)Instantiate (Resources.Load ("Prefabs/VFX_Boost"));
		//Make it "fire" upwards
		boost.transform.Rotate (Vector3.right, -90);
		//"Scale" according to size of user
		boost.GetComponent<ParticleSystem> ().startLifetime += 0.2f * userSize.y; //Works, makes the particles last longer and therefore moves further in y-axis (scaling it accoding to height of mesh)
		//boost.particleSystem.shape.radius = 2;
		boost.GetComponent<ParticleSystem> ().transform.localScale.Scale (user.transform.lossyScale); //doesn't work 
		//TODO: Make at least the radius of particle system (shape) scale with size of user
		//Start and Deactivate (Initialize it without it being activated)
		boost.GetComponent<ParticleSystem> ().Play ();
		boost.SetActive (false);
	}

	/// <summary>
	/// Method for Particle System effect when taking damage.
	/// </summary>
	/// <param name="type">Name of material from prefab. Currently Supports "Banana" and "Fish".</param>
	private void damageTakenEffect (string type)
	{
		//Create Particle System
		GameObject instance = (GameObject)Instantiate (Resources.Load ("Prefabs/VFX_DmgTaken"));
		//Assign Material
		instance.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().material = (Material)Instantiate (Resources.Load ("Materials/VFX_" + type));
		//Activate it at the user position (will be center of object)
		instance.transform.position = user.position;
		instance.GetComponent<ParticleSystem> ().Play ();
		//Cleaning up
		Destroy (instance, 1.5f);
	}

	public void aimOverlay (float radius, float rangeMax, float rangeMin = 0)
	{
		float range = rangeMax - rangeMin;
		aim.localScale = new Vector3 (radius, userSize.x / 2, range);
		//aim.localScale = new Vector3(range, radius, userSize.x / 2);
		aim.localPosition = new Vector3 (0, 0, (userSize.z / 2) + rangeMin + (range / 2));
	}

	private void SetupActionBar ()
	{
		a1Over = Action1.transform.GetChild (0).GetComponent<Image> ();
		Action1.transform.GetChild (1).GetComponent<Text> ().text = a1Key.ToUpper ();
		a2Over = Action2.transform.GetChild (0).GetComponent<Image> ();
		Action2.transform.GetChild (1).GetComponent<Text> ().text = a2Key.ToUpper ();
		a3Over = Action3.transform.GetChild (0).GetComponent<Image> ();
		Action3.transform.GetChild (1).GetComponent<Text> ().text = a3Key.ToUpper ();
		a4Over = Action4.transform.GetChild (0).GetComponent<Image> ();
		Action4.transform.GetChild (1).GetComponent<Text> ().text = a4Key.ToUpper ();
		a5Over = Action5.transform.GetChild (0).GetComponent<Image> ();
		Action5.transform.GetChild (1).GetComponent<Text> ().text = a5Key.ToUpper ();
		a6Over = Action6.transform.GetChild (0).GetComponent<Image> ();
		Action6.transform.GetChild (1).GetComponent<Text> ().text = a6Key.ToUpper ();
	}

	public void ActionBar (ref Image overlayImage, string key, ref bool onCooldown, float cooldownTimer)
	{
		if (Input.GetKeyDown (key) && !onCooldown) {
			onCooldown = true;
			overlayImage.fillAmount = 0.0f;
		}
		if (onCooldown == true) {
			overlayImage.fillAmount += (1.0f / cooldownTimer * Time.deltaTime);
			if (overlayImage.fillAmount == 1.0f)
				onCooldown = false;
		}
	}
}