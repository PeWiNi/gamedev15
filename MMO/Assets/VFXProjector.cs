using UnityEngine;
using System.Collections;

public class VFXProjector : MonoBehaviour
{
    Projector projector;

    Texture tailAim;
    Texture boomAim;
    Texture pukeAim;
    Texture fishAim;

    KeyCode tailKey;
    KeyCode boomKey;
    KeyCode pukeKey;
    KeyCode fishKey;

    float timer;

	// Use this for initialization
	void Start () {
        tailAim = Resources.Load<Texture>("Images/AimTail");
        boomAim = Resources.Load<Texture>("Images/AimPointer");
        pukeAim = Resources.Load<Texture>("Images/AimPuke");
        fishAim = Resources.Load<Texture>("Images/AimFish");
        projector = GetComponent<Projector>();
        projector.aspectRatio = 1;
        projector.transform.position = new Vector3(0, 10, 0);
        projector.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0) {
            projector.enabled = false;
        } else if (Input.anyKeyDown) {
            timer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(MenuScript.KeyBindings[0])) { //Tail
            castProjection(tailAim, 0.35f, 65, 5, 1.5f);
        }
        if (Input.GetKeyDown(MenuScript.KeyBindings[1])) { //Boomnana
            castProjection(boomAim, 0.2f, 220, 15, 3);
        }
        if (Input.GetKeyDown(MenuScript.KeyBindings[2])) { //Puke
            castProjection(pukeAim, 1, 60, 5, 4);
        }
        if (Input.GetKeyDown(MenuScript.KeyBindings[3])) { //Fish
            castProjection(fishAim, 1, 40, 2, 2);
        }
	}
    void castProjection(Texture txt, float aRatio, float height, float distance, float activeTime) {
        projector.material.mainTexture = txt;
        projector.aspectRatio = aRatio;
        projector.transform.localPosition = new Vector3(0, height, distance);
        projector.enabled = true;
        timer = activeTime;
    }
}
