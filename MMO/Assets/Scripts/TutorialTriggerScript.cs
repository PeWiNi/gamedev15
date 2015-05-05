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
        if (col.gameObject.tag.Equals("TutorialPlayer"))
        {
            triggerText.enabled = true;
        }
	}

    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag.Equals("TutorialPlayer") && !hasBeenDisplayed)//Detect if the player moved.
        {
            StartCoroutine("DisplayText");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag.Equals("TutorialPlayer"))//Detect if the player moved.
        {
            triggerText.enabled = false;
            StopCoroutine("DisplayText");
        }
    }

    IEnumerator DisplayText() {
        triggerText.enabled = true;
        yield return new WaitForSeconds(5f);
        triggerText.enabled = false;
        hasBeenDisplayed = true;
    }
}
