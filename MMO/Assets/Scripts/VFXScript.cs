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
    void Start()
    {
        // Assumes that the camera is a child of the playerObj. 
        user = mainCamera.transform.parent;
        userSize = user.collider.bounds.size;

        boostDecay = boostAlpha / boostDuration;

        burdenEffect = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Burdened"));
        burdenEffect.particleSystem.Stop();

        aim = user.GetChild(1);
        aim.renderer.enabled = false;

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

        #region Burden
        if (Input.GetKeyDown(burdenKey))
        {
            holdingCoconut = !holdingCoconut;
            if (holdingCoconut)
                burdenEffect.particleSystem.Play();
            else
                burdenEffect.particleSystem.Stop();
            //burdenEffect.SetActive(holdingCoconut = !holdingCoconut);
        }
        if (holdingCoconut)
            burdenEffect.transform.position = new Vector3(user.position.x, (user.position.y + (userSize.y / 2)), user.position.z);
        #endregion

        #region Boost
        if (boostTimer <= 0)
            boost.SetActive(false);
        if (Input.GetKeyDown(boostKey))
        {
            boostTimer = boostDuration;
            boostAlpha = 0.2f;
            boost.SetActive(true);
        }

        // Calculate in seconds!
        if (boostTimer <= boostDuration && boostTimer > 0)
        {
            boost.transform.position = new Vector3(user.position.x, (user.position.y - (userSize.y / 2)), user.position.z);
            boost.particleSystem.startColor = new Color(boost.particleSystem.startColor.r, boost.particleSystem.startColor.g, boost.particleSystem.startColor.b, boostAlpha);
            secondsTimer += Time.deltaTime;
            if (secondsTimer > 1.0f)
            {
                buffDecay();
                secondsTimer -= 1.0f;
            }
        }
        #endregion

        if (Input.GetKeyDown(aimKey))
        {
            //aim = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Burdened"));
            aim.renderer.enabled = true;
            aimOverlay(1, range, 0.5f);
        }
        if (Input.GetKeyUp(aimKey))
        {

            Vector3 shot = new Vector3(user.position.x, user.position.y, gameObject.GetComponentInParent<PlayerStats>().boomnanaRange + user.position.z);
            Vector3 offset = user.position - shot;
            end = (user.forward * gameObject.GetComponentInParent<PlayerStats>().boomnanaRange);
            float desiredAngle = user.eulerAngles.y;

            Debug.Log("Angle = " + desiredAngle);

            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            //Vector3 retry = desiredAngle;
            
            Vector3 endPos = (user.position) - (rotation * offset);
            Debug.Log("EndPos = " + endPos.x + "," + endPos.y + "," + endPos.z);
            Debug.Log("FIRING BOOMNANA FROM VFX"); 
            boomscript.spawn(GOuser, boomscript.owner, user.position, /*user.forward * range, */endPos); 
            aim.renderer.enabled = false;
        }
    }

    /// <summary>
    /// Method for managing the Alpha-decay of the Boost Particle system.
    /// </summary>
    private void buffDecay()
    {
        print("Buff left: " + (boostTimer - 1));
        boostTimer -= 1;
        boostAlpha -= boostDecay;
    }

    /// <summary>
    /// Method for setting up the Particle System for when activating AMP ability.
    /// </summary>
    private void buffSetup()
    {
        //Create Particle System
        boost = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Boost"));
        //Make it "fire" upwards
        boost.transform.Rotate(Vector3.right, -90);
        //"Scale" according to size of user
        boost.particleSystem.startLifetime += 0.2f * userSize.y; //Works, makes the particles last longer and therefore moves further in y-axis (scaling it accoding to height of mesh)
        //boost.particleSystem.shape.radius = 2;
        boost.particleSystem.transform.localScale.Scale(user.transform.lossyScale); //doesn't work 
        //TODO: Make at least the radius of particle system (shape) scale with size of user
        //Start and Deactivate (Initialize it without it being activated)
        boost.particleSystem.Play();
        boost.SetActive(false);
    }

    /// <summary>
    /// Method for Particle System effect when taking damage.
    /// </summary>
    /// <param name="type">Name of material from prefab. Currently Supports "Banana" and "Fish".</param>
    private void damageTakenEffect(string type)
    {
        //Create Particle System
        GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_DmgTaken"));
        //Assign Material
        instance.particleSystem.renderer.material = (Material)Instantiate(Resources.Load("Materials/VFX_" + type));
        //Activate it at the user position (will be center of object)
        instance.transform.position = user.position;
        instance.particleSystem.Play();
        //Cleaning up
        Destroy(instance, 1.5f);
    }

    public void aimOverlay(float radius, float rangeMax, float rangeMin = 0)
    {
        float range = rangeMax - rangeMin;
        aim.localScale = new Vector3(radius, userSize.x / 2, range);
        //aim.localScale = new Vector3(range, radius, userSize.x / 2);
        aim.localPosition = new Vector3(0, 0, (userSize.z / 2) + rangeMin + (range / 2));
    }
}