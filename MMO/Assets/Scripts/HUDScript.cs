using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    private float secondsTimer = 0;

    // Damage Effect
    public KeyCode redKey = KeyCode.Space; // Change to taking damage event
    public RawImage takingDamageHUD;
    public Camera mainCamera;
    private float HUDTimer = 0;
    public float HUDEffectIntensity = 0.3f;
    public float HUDEffectDecay = 0.004f;

    // Boost Effect
    public KeyCode boostKey = KeyCode.Q; // Change to Boost-key
    private GameObject boost;
    private float boostTimer = 0;
    public float boostDuration = 10;
    private float boostAlpha = 0.2f;

    private Transform user;
    private Vector3 userSize;

	// Use this for initialization
    void Start()
    {
        // Assumes that the camera is a child of the playerObj. 
        user = mainCamera.transform.parent;
        userSize = user.collider.bounds.size;

        buffSetup();
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

            if (boostTimer > 0)
                damageTakenEffect("Fish"); //Fish Slam
            else
                damageTakenEffect("Banana"); //Tail Slap
        }
        takingDamageHUD.color = new Color(takingDamageHUD.color.r, takingDamageHUD.color.g, takingDamageHUD.color.b, HUDTimer);
        HUDTimer -= HUDEffectDecay;
        #endregion

        #region Boost
        if (boostTimer <= 0)
            boost.SetActive(false);
        if (Input.GetKeyDown(boostKey))
        {
            boostTimer = boostDuration;
            boostAlpha = 0.2f;
            boost.transform.position = new Vector3(user.position.x, (user.position.y - (userSize.y / 2)), user.position.z);
            boost.SetActive(true);
        }

        // To seconds!
        if (boostTimer <= boostDuration && boostTimer > 0)
        {
            boost.particleSystem.startColor = new Color(boost.particleSystem.startColor.r, boost.particleSystem.startColor.g, boost.particleSystem.startColor.b, boostAlpha);
            secondsTimer += Time.deltaTime;
            if (secondsTimer > 1.0f)
            {
                buffDecay();
                secondsTimer -= 1.0f;
            }
        }
        #endregion
    }

    private void buffDecay()
    {
        print("Buff left: " + (boostTimer - 1));
        boostTimer -= 1;
        boostAlpha -= 0.02f;

    }

    private void buffSetup()
    {
        boost = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Boost"));
        boost.transform.Rotate(Vector3.right, -90);
        boost.particleSystem.startLifetime += 0.2f * userSize.y;
        boost.particleSystem.Play();
        boost.particleSystem.transform.localScale.Scale(userSize);
        boost.SetActive(false);
    }

    private void damageTakenEffect(string type)
    {
        //Create Particle System
        GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_DmgTaken"));
        instance.particleSystem.renderer.material = (Material)Instantiate(Resources.Load("Materials/" + type + "Effects")); // Change Material according to type
        instance.transform.position = user.position;
        instance.particleSystem.Play();
        //Cleaning up
        Destroy(instance, 1.5f);
    }
}
