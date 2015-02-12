using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {

	private bool isHeld = false;
	private GameObject go = null;
	// Use this for initialization
	void Start () {
	
	}

	public bool isHeldAtm(){
		return isHeld;
	}
	public GameObject getHolder(){
		return go;
	}
	// Update is called once per frame
	void Update () {
		Vector3 positionAtm = transform.position;
		if (isHeld) {
			//Debug.Log("It is held!");
			WASD wasd = go.GetComponent<WASD> ();
			int a = wasd.getFacing ();
			Vector3 position = 
				new Vector3 (wasd.gameObject.transform.position.x, wasd.gameObject.transform.position.y + 15,
				            wasd.gameObject.transform.position.z);
			transform.position = position;
		} else {
			//Debug.Log("not held");
			Vector3 newVec = new Vector3(positionAtm.x, positionAtm.y, positionAtm.z);
			transform.position = newVec;
		}
	}

	public void setCapture(GameObject go){
		this.go = go;
		isHeld = true;
	}
	public void removeCapture(GameObject go){
		this.go = null;
		isHeld = false;
	}
}
