using UnityEngine;
using System.Collections;

public class RotateGO : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.RotateAround (transform.position, transform.right, Time.deltaTime * 90f);
	}
}
