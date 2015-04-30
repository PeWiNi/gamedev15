using UnityEngine;
using System.Collections;

public class bananaAnimScript : MonoBehaviour {
	public Animation anim;
	//
	private AnimationState slap;
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animation>();
		slap = anim["Take 001"];
		
		anim["Take 001"].wrapMode = WrapMode.Once;
		anim["Take 001"].speed = 1;
		anim["Take 001"].layer = 0;
	}
	
	// Update is called once per frame
	public void playAnimation () {
		anim.wrapMode = WrapMode.Once;
		anim.Play("Take 001");
	}
}
