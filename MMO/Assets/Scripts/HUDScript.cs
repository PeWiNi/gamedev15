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
	public float FadeFactor = 0.75f;

    #region Action Bar
	public GameObject Action1;
	private Image a1Over;
	public string a1Key = "mouse 0";
	public float a1Time = 3f;
	private bool a1Cooldown;
	public GameObject Action2;
	private Image a2Over;
	public string a2Key = "mouse 1";
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
	public GameObject Action6;
	private Image a6Over;
	public string a6Key = "r";
	public float a6Time = 10f;
	private bool a6Cooldown;
    #endregion

    private Slider castBar;
	public Text dmgDealt;
    private string currentCast;

    public GameObject bcs1;
    public GameObject bcs2;
    public GameObject bcs3;
    public GameObject bcs4;
    private Slider captureSlider1;
    private Slider captureSlider2;
    private Slider captureSlider3;
    private Slider captureSlider4;

	// Use this for initialization
	void Start ()
	{
		SetupActionBar ();
		castBar = GameObject.Find("CastBar").GetComponent<Slider>();
        castBar.gameObject.SetActive(false);

        captureSlider1 = GameObject.Find("CaptureSlider1").GetComponent<Slider>();
        captureSlider2 = GameObject.Find("CaptureSlider2").GetComponent<Slider>();
        captureSlider3 = GameObject.Find("CaptureSlider3").GetComponent<Slider>();
        captureSlider4 = GameObject.Find("CaptureSlider4").GetComponent<Slider>();

        Menu = GameObject.Find("InGameMenu");
        Menu.SetActive(false);
	}

	public void damageEff ()
	{
		HUDTimer = HUDEffectIntensity;
		takingDamageHUD.enabled = true;
        takingDamageHUD.color = new Color(246f/255f, 49f/255f, 1f/255f);
	}

    public void healEff()
    {
        HUDTimer = HUDEffectIntensity;
        takingDamageHUD.enabled = true;
        takingDamageHUD.color = new Color(136f/255f, 204f/255f, 68f/255f);
    }

	// Update is called once per frame
	void Update ()
	{
		dmgDealt.color = new Color (dmgDealt.color.r, dmgDealt.color.g, dmgDealt.color.b, dmgDealt.color.a - (1.0f / FadeFactor * Time.deltaTime));
		
		#region Damage Effect
		if (HUDTimer <= 0) // Disable when the effect has ended
			takingDamageHUD.enabled = false;
		takingDamageHUD.color = new Color (takingDamageHUD.color.r, takingDamageHUD.color.g, takingDamageHUD.color.b, HUDTimer);
		HUDTimer -= HUDEffectDecay;
		#endregion

        #region ActionBar
            ActionBarOnPress(ref a1Over, a1Key, ref a1Cooldown, a1Time); // Tail
            ActionBarOnRelease(ref a2Over, a2Key, ref a2Cooldown, a2Time); // Boom
            ActionBarOnPress(ref a3Over, a3Key, ref a3Cooldown, a3Time, 3, true); // AoE
            ActionBarOnPress(ref a4Over, a4Key, ref a4Cooldown, a4Time); // CC
            ActionBarOnPress(ref a6Over, a6Key, ref a6Cooldown, a6Time); // CPR
        #endregion

        #region Capture
        try {
            captureSlider1.value = bcs1.GetComponent<BeaconCaptureScript>().captureValue;
            captureSlider2.value = bcs2.GetComponent<BeaconCaptureScript>().captureValue;
            captureSlider3.value = bcs3.GetComponent<BeaconCaptureScript>().captureValue;
            captureSlider4.value = bcs4.GetComponent<BeaconCaptureScript>().captureValue;
        } catch {
            captureSlider1.gameObject.SetActive(false);
            captureSlider2.gameObject.SetActive(false);
            captureSlider3.gameObject.SetActive(false);
            captureSlider4.gameObject.SetActive(false);
        }
        #endregion

        if (Input.GetKeyUp(KeyCode.Escape)) 
            ShowMenu(menu);
	}

    public void ShowMenu(bool isActive)
    {
        Menu.SetActive(!isActive);
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

	private string toUpper (string s)
	{
		if (s.Equals ("mouse 0"))
			return "LMB";
		if (s.Equals ("mouse 1"))
			return "RMB";
		if (s.Equals ("mouse 2"))
			return "MMB";
		return s.ToUpper ();
	}

	public void ActionBarOnRelease (ref Image overlayImage, string key, ref bool onCooldown, float cooldownTimer)
	{
        if (!menu)
        {
            if (Input.GetKeyUp(key) && !onCooldown)
            {
                onCooldown = true;
                overlayImage.fillAmount = 0.0f;
                dmgDealt.color = new Color(dmgDealt.color.r, dmgDealt.color.g, dmgDealt.color.b, 1);

                dmgDealt.text = "Throwing BoomNana!";

            }
        }
		if (onCooldown == true) {
			overlayImage.fillAmount += (1.0f / cooldownTimer * Time.deltaTime);
			if (overlayImage.fillAmount == 1.0f)
				onCooldown = false;
		}
	}

	public void ActionBarOnPress (ref Image overlayImage, string key, ref bool onCooldown, float cooldownTimer, float castTime = 0, bool channeled = false)
	{
        if (!menu)
        {
            if (Input.GetKeyDown(key) && !onCooldown)
            {
                onCooldown = true;
                overlayImage.fillAmount = 0.0f;
                dmgDealt.color = new Color(dmgDealt.color.r, dmgDealt.color.g, dmgDealt.color.b, 1);
                if (castTime != 0)
                    ActivateCastBar(castTime, channeled, key);
                if (key == a1Key)
                {
                    dmgDealt.text = "Miss!";
                }
                if (key == a3Key)
                {
                    dmgDealt.text = "Channeling AOE!";
                    Text a = castBar.GetComponentInChildren<Text>();
                    a.color = Color.red;
                    a.text = "Puking All Over The Place";
                }
                if (key == a4Key)
                {
                    dmgDealt.text = "Stunning!";
                }
                if (key == a6Key)
                {
                    dmgDealt.text = "Resurrecting!";
                    Text a = castBar.GetComponentInChildren<Text>();
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
		if (castTime != 0 && castBar.IsActive () && currentCast.Equals (key))
			UpdateCastBar (castTime, channeled);
	}

	private void ActivateCastBar (float duration, bool channeled, string synchronize)
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
