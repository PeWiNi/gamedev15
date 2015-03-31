using UnityEngine;
using System.Collections;

public class AimTest : MonoBehaviour {
    float EffectTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(EffectTime>0) {
            if(EffectTime < 450 && EffectTime > 400)
                renderer.sharedMaterial.SetVector("_ShieldColor", new Vector4(0.7f, 1f, 1f, 0f));

            EffectTime-=Time.deltaTime * 1000;
            renderer.sharedMaterial.SetFloat("_EffectTime", EffectTime);
        }
	}

    void onCollisionEnter(Collision collision) {
        foreach(ContactPoint contact in collision.contacts) {
            renderer.sharedMaterial.SetVector("_ShieldColor", new Vector4(0.7f, 1f, 1f, 0.05f));

            renderer.sharedMaterial.SetVector("_Position", transform.InverseTransformPoint(contact.point));
            
            EffectTime = 500;
        }
    }
}
