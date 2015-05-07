using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialTriggerScript : MonoBehaviour {

    public Text triggerText;
    bool hasBeenDisplayed = false;

	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals("player"))
        {
            triggerText.enabled = true;
			hasBeenDisplayed = true;
        }
	}

    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag.Equals("player") && !hasBeenDisplayed)//Detect if the player moved.
        {
            triggerText.enabled = true;
			hasBeenDisplayed = true;
//            StartCoroutine("DisplayText");
        }
    }

    void OnTriggerExit(Collider col) {
		if (col.gameObject.tag.Equals("player"))//Detect if the player moved.
        {
            triggerText.enabled = false;
			hasBeenDisplayed = false;
//            StopCoroutine("DisplayText");
        }
    }

    IEnumerator DisplayText() {
        triggerText.enabled = true;
        yield return new WaitForSeconds(5f);
        triggerText.enabled = false;
        hasBeenDisplayed = true;
    }
}
