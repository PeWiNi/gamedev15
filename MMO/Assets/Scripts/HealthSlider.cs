using UnityEngine;
using System.Collections;

public class HealthSlider : MonoBehaviour
{

		UnityEngine.UI.Slider healthSlider;
		PlayerStats ps;

		// Use this for initialization
		void Start ()
		{
				healthSlider = this.gameObject.GetComponentInChildren<UnityEngine.UI.Slider> ();
				ps = gameObject.GetComponentInParent<PlayerStats> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				healthSlider.value = (int)((float)(ps.hp / ps.maxHealth) * 100);
		}
}
