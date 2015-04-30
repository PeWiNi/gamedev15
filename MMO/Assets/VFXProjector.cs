using UnityEngine;
using System.Collections;

public class VFXProjector : MonoBehaviour
{
    Projector projector;

    Texture pukeAim;
    Texture tailAim;
    Texture boomAim;

    public static bool rotated = false;

	// Use this for initialization
	void Start () {
        pukeAim = Resources.Load<Texture>("Images/AimPuke");
        tailAim = Resources.Load<Texture>("Images/AimFishTail");
        boomAim = Resources.Load<Texture>("Images/AimPointer");
        projector = GetComponent<Projector>();
        projector.aspectRatio = 1;
        projector.transform.position = new Vector3(0, 10, 0);
        projector.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space)) {
            projector.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            if (rotated) {
                projector.transform.Rotate(0, 0, 270);
                rotated = false;
            }
            projector.material.mainTexture = pukeAim;
            projector.aspectRatio = 1;
            projector.transform.localPosition = new Vector3(0, 10, 2);
            projector.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            if (rotated) {
                projector.transform.Rotate(0, 0, 270);
                rotated = false;
            }
            projector.material.mainTexture = tailAim;
            projector.aspectRatio = 2;
            projector.transform.localPosition = new Vector3(0, 20, 2);
            projector.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.F)) {
            if (rotated) {
                projector.transform.Rotate(0, 0, 270);
                rotated = false;
            }
            projector.material.mainTexture = boomAim;
            projector.aspectRatio = 4;
            projector.transform.localPosition = new Vector3(0, 10, 4);
            projector.transform.Rotate(0, 0, 90);
            rotated = true;
            projector.enabled = true;
        }
	}
}
