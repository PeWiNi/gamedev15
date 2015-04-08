using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    private float secondsTimer = 0;

    // Damage Effect
    public KeyCode redKey = KeyCode.Space; // Change to taking damage event
    public RawImage takingDamageHUD;
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

    // Use this for initialization
    void Start()
    {
        SetupActionBar();
    }

    // Update is called once per frame
    void Update()
    {
        #region Damage Effect
        if (HUDTimer <= 0) // Disable when the effect has ended
            takingDamageHUD.enabled = false;
        if (Input.GetKeyDown(redKey)) // Change to taking damage event 
        {
            //Creates HUDeffect when damaged
            HUDTimer = HUDEffectIntensity;
            takingDamageHUD.enabled = true;
        }
        takingDamageHUD.color = new Color(takingDamageHUD.color.r, takingDamageHUD.color.g, takingDamageHUD.color.b, HUDTimer);
        HUDTimer -= HUDEffectDecay;
        #endregion

        #region ActionBar
        ActionBar(ref a1Over, a1Key, ref a1Cooldown, a1Time);
        ActionBar(ref a2Over, a2Key, ref a2Cooldown, a2Time);
        ActionBar(ref a3Over, a3Key, ref a3Cooldown, a3Time);
        ActionBar(ref a4Over, a4Key, ref a4Cooldown, a4Time);
        ActionBar(ref a5Over, a5Key, ref a5Cooldown, a5Time);
        ActionBar(ref a6Over, a6Key, ref a6Cooldown, a6Time);
        #endregion
    }

    private void SetupActionBar()
    {
        a1Over = Action1.transform.GetChild(0).GetComponent<Image>();
        Action1.transform.GetChild(1).GetComponent<Text>().text = toUpper(a1Key);
        a2Over = Action2.transform.GetChild(0).GetComponent<Image>();
        Action2.transform.GetChild(1).GetComponent<Text>().text = toUpper(a2Key);
        a3Over = Action3.transform.GetChild(0).GetComponent<Image>();
        Action3.transform.GetChild(1).GetComponent<Text>().text = toUpper(a3Key);
        a4Over = Action4.transform.GetChild(0).GetComponent<Image>();
        Action4.transform.GetChild(1).GetComponent<Text>().text = toUpper(a4Key);
        a5Over = Action5.transform.GetChild(0).GetComponent<Image>();
        Action5.transform.GetChild(1).GetComponent<Text>().text = toUpper(a5Key);
        a6Over = Action6.transform.GetChild(0).GetComponent<Image>();
        Action6.transform.GetChild(1).GetComponent<Text>().text = toUpper(a6Key);
    }

    private string toUpper(string s)
    {
        if(s.Equals("mouse 0"))
            return "LMB";
        if (s.Equals("mouse 1"))
            return "RMB";
        if (s.Equals("mouse 2"))
            return "MMB";
        return s.ToUpper();
    }

    public void ActionBar(ref Image overlayImage, string key, ref bool onCooldown, float cooldownTimer)
    {
        if (Input.GetKeyDown(key) && !onCooldown)
        {
            onCooldown = true;
            overlayImage.fillAmount = 0.0f;
        }
        if (onCooldown == true)
        {
            overlayImage.fillAmount += (1.0f / cooldownTimer * Time.deltaTime);
            if (overlayImage.fillAmount == 1.0f)
                onCooldown = false;
        }
    }
}
