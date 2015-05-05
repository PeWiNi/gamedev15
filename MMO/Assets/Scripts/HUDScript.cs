using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
	private float secondsTimer = 0;
	private GameObject Menu;
	private bool menu = false;

	// Damage Effect
	public RawImage takingDamageHUD;
	private float HUDTimer = 0;
	public float HUDEffectIntensity = 0.3f;
	public float HUDEffectDecay = 0.004f;
	float FadeFactor = 0.75f;

    #region Action Bar
    public GameObject Action1;
	private Image a1Over;
	public static KeyCode a1Key = KeyCode.Mouse0;
	public float a1Time = 3f;
	private bool a1Cooldown;
    public GameObject Action2;
	private Image a2Over;
    public static KeyCode a2Key = KeyCode.Mouse1;
	public float a2Time = 3f;
	private bool a2Cooldown;
    public GameObject Action3;
	private Image a3Over;
    public static KeyCode a3Key = KeyCode.Alpha1;
	public float a3Time = 3f;
	private bool a3Cooldown;
    public GameObject Action4;
	private Image a4Over;
    public static KeyCode a4Key = KeyCode.Alpha2;
	public float a4Time = 3f;
	private bool a4Cooldown;
    public GameObject Action6;
	private Image a6Over;
    public static KeyCode a6Key = KeyCode.R;
	public float a6Time = 10f;
	private bool a6Cooldown;
    #endregion

	private Slider castBar;
	public Text announcementText;
    private KeyCode currentCast;

	private Slider captureSliderNorth;
	private Slider captureSliderWest;
	private Slider captureSliderEast;
	private Slider captureSliderSouth;

	// Use this for initialization
	void Start ()
	{
		SetupActionBar ();
		castBar = GameObject.Find ("CastBar").GetComponent<Slider> ();
		castBar.gameObject.SetActive (false);

		captureSliderNorth = GameObject.Find ("CaptureSliderNorth").GetComponent<Slider> ();
		captureSliderWest = GameObject.Find ("CaptureSliderWest").GetComponent<Slider> ();
		captureSliderEast = GameObject.Find ("CaptureSliderEast").GetComponent<Slider> ();
		captureSliderSouth = GameObject.Find ("CaptureSliderSouth").GetComponent<Slider> ();

		Menu = GameObject.Find ("InGameMenu");
		Menu.SetActive (false);
	}

	public void damageEff ()
	{
		HUDTimer = HUDEffectIntensity;
		takingDamageHUD.enabled = true;
		takingDamageHUD.color = new Color (246f / 255f, 49f / 255f, 1f / 255f);
	}

	public void healEff ()
	{
		HUDTimer = HUDEffectIntensity;
		takingDamageHUD.enabled = true;
		takingDamageHUD.color = new Color (136f / 255f, 204f / 255f, 68f / 255f);
	}

	// Update is called once per frame
	void Update ()
    {
		announcementText.color = new Color (announcementText.color.r, announcementText.color.g, announcementText.color.b, announcementText.color.a - (1.0f / FadeFactor * Time.deltaTime));
		
		#region Damage Effect
		if (HUDTimer <= 0) // Disable when the effect has ended
			takingDamageHUD.enabled = false;
		takingDamageHUD.color = new Color (takingDamageHUD.color.r, takingDamageHUD.color.g, takingDamageHUD.color.b, HUDTimer);
        HUDTimer -= HUDEffectDecay;
        #endregion
        try {
            GameObject monguin = GameObject.Find("PlayerObject3dWithColliders(Clone)");
            StateController sc = monguin.GetComponent<StateController>();
            DeathSpawner ds = monguin.GetComponent<DeathSpawner>();
            if (ds.remain > 0 && sc.ressStarted) {
                HUDTimer = 1.0f;
                takingDamageHUD.enabled = true;
                announcementText.text = "You are dead.. Respawning in " + ds.remain + " seconds";
                announcementText.color = new Color(announcementText.color.r, announcementText.color.g, announcementText.color.b, HUDTimer);
                HUDTimer = HUDEffectIntensity;
            }
        } catch { }

        #region ActionBar
        UpdateActionBarText();
        ActionBarOnPress (ref a1Over, a1Key, ref a1Cooldown, a1Time); // Tail
		ActionBarOnRelease (ref a2Over, a2Key, ref a2Cooldown, a2Time); // Boom
		ActionBarOnPress (ref a3Over, a3Key, ref a3Cooldown, a3Time, 3, true); // AoE
		ActionBarOnPress (ref a4Over, a4Key, ref a4Cooldown, a4Time); // CC
		ActionBarOnPress (ref a6Over, a6Key, ref a6Cooldown, a6Time); // CPR
		#endregion

		#region Capture
		try {
            captureSliderNorth.value = GameObject.Find("BeaconNorth").GetComponent<BeaconCaptureScript>().captureValue;
		} catch {
			captureSliderNorth.gameObject.SetActive (false);
        } 
        try {
            captureSliderWest.value = GameObject.Find("BeaconWest").GetComponent<BeaconCaptureScript>().captureValue;
        }
        catch {
			captureSliderWest.gameObject.SetActive (false);
        }
        try {
            captureSliderEast.value = GameObject.Find("BeaconEast").GetComponent<BeaconCaptureScript>().captureValue;
        }
        catch {
			captureSliderEast.gameObject.SetActive (false);
        }
        try {
            captureSliderSouth.value = GameObject.Find("BeaconSouth").GetComponent<BeaconCaptureScript>().captureValue;
        }
        catch {
			captureSliderSouth.gameObject.SetActive (false);
		}
		#endregion

		if (Input.GetKeyUp (KeyCode.Escape))
            ShowMenu(menu);
	}

	public void ShowMenu (bool isActive)
	{
		Menu.SetActive (!isActive);
		menu = !isActive;
		Cursor.visible = !isActive;
	}

	private void SetupActionBar ()
	{
		a1Over = Action1.transform.GetChild (0).GetComponent<Image> ();
		Action1.transform.GetChild (1).GetComponent<Text> ().text = toUpper (a1Key);
		a2Over = Action2.transform.GetChild (0).GetComponent<Image> ();
		Action2.transform.GetChild (1).GetComponent<Text> ().text = toUpper (a2Key);
		a3Over = Action3.transform.GetChild (0).GetComponent<Image> ();
		Action3.transform.GetChild (1).GetComponent<Text> ().text = toUpper (a3Key);
		a4Over = Action4.transform.GetChild (0).GetComponent<Image> ();
		Action4.transform.GetChild (1).GetComponent<Text> ().text = toUpper (a4Key);
		a6Over = Action6.transform.GetChild (0).GetComponent<Image> ();
		Action6.transform.GetChild (1).GetComponent<Text> ().text = toUpper (a6Key);
	}

    private void UpdateActionBarText ()
    {
        try
        {
            a1Key = MenuScript.KeyBindings[0];
            a2Key = MenuScript.KeyBindings[1];
            a3Key = MenuScript.KeyBindings[2];
            a4Key = MenuScript.KeyBindings[3];
            a6Key = MenuScript.KeyBindings[4];
            Action1.transform.GetChild(1).GetComponent<Text>().text = toUpper(a1Key);
            Action2.transform.GetChild(1).GetComponent<Text>().text = toUpper(a2Key);
            Action3.transform.GetChild(1).GetComponent<Text>().text = toUpper(a3Key);
            Action4.transform.GetChild(1).GetComponent<Text>().text = toUpper(a4Key);
            Action6.transform.GetChild(1).GetComponent<Text>().text = toUpper(a6Key);
        }
        catch { }
    }

	private static string toUpper (KeyCode kc)
	{
        if (kc == KeyCode.Mouse0)
			return "LMB";
        if (kc == KeyCode.Mouse1)
			return "RMB";
        if (kc == KeyCode.Mouse2)
            return "MMB";
        if (kc == KeyCode.Alpha0)
            return "0";
        if (kc == KeyCode.Alpha1)
            return "1";
        if (kc == KeyCode.Alpha2)
            return "2";
        if (kc == KeyCode.Alpha3)
            return "3";
        if (kc == KeyCode.Alpha4)
            return "4";
        if (kc == KeyCode.Alpha5)
            return "5";
        return kc.ToString().ToUpper();
	}

	public void ActionBarOnRelease (ref Image overlayImage, KeyCode key, ref bool onCooldown, float cooldownTimer)
	{
		if (!menu) {
			if (Input.GetKeyUp (key) && !onCooldown) {
				onCooldown = true;
				overlayImage.fillAmount = 0.0f;
				announcementText.color = new Color (announcementText.color.r, announcementText.color.g, announcementText.color.b, 1);

				announcementText.text = "Throwing BoomNana!";

			}
		}
		if (onCooldown == true) {
			overlayImage.fillAmount += (1.0f / cooldownTimer * Time.deltaTime);
			if (overlayImage.fillAmount == 1.0f)
				onCooldown = false;
		}
	}

	public void ActionBarOnPress (ref Image overlayImage, KeyCode key, ref bool onCooldown, float cooldownTimer, float castTime = 0, bool channeled = false)
	{
		if (!menu) {
			if (Input.GetKeyDown (key) && !onCooldown) {
				onCooldown = true;
				overlayImage.fillAmount = 0.0f;
				announcementText.color = new Color (announcementText.color.r, announcementText.color.g, announcementText.color.b, 1);
				if (castTime != 0)
					ActivateCastBar (castTime, channeled, key);
				if (key == a1Key) {
					announcementText.text = "Miss!";
				}
				if (key == a3Key) {
					announcementText.text = "Channeling AOE!";
					Text a = castBar.GetComponentInChildren<Text> ();
					a.color = Color.red;
					a.text = "Puking All Over The Place";
				}
				if (key == a4Key) {
					announcementText.text = "Stunning!";
				}
				if (key == a6Key) {
					announcementText.text = "Resurrecting!";
					Text a = castBar.GetComponentInChildren<Text> ();
					a.color = Color.red;
					a.text = "Monguin CPR";
				}

			}
		}
		if (onCooldown == true) {
			overlayImage.fillAmount += (1.0f / cooldownTimer * Time.deltaTime);
			
			if (overlayImage.fillAmount == 1.0f)
				onCooldown = false;
		}
		if (castTime != 0 && castBar.IsActive () && currentCast == key)
			UpdateCastBar (castTime, channeled);
	}

    private void ActivateCastBar(float duration, bool channeled, KeyCode synchronize)
	{
		castBar.gameObject.SetActive (true);
		if (channeled)
			castBar.value = 1;
		else
			castBar.value = 0;
		currentCast = synchronize;
	}

	private void UpdateCastBar (float duration, bool channeled)
	{
		if (channeled) {
			castBar.value -= 1.0f / duration * Time.deltaTime;
			if (castBar.value == 0)
				castBar.gameObject.SetActive (false);
		} else {
			castBar.value += 1.0f / duration * Time.deltaTime;
			if (castBar.value == 1)
				castBar.gameObject.SetActive (false);
		}
	}
}
