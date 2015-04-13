using UnityEngine;
using System.Collections;

public class HealthSlider : MonoBehaviour
{

		UnityEngine.UI.Slider healthSlider;
        UnityEngine.UI.Text healthText;
		PlayerStats ps;

		// Use this for initialization
		void Start ()
		{
			healthSlider = this.gameObject.GetComponentInChildren<UnityEngine.UI.Slider> ();
            healthText = healthSlider.GetComponentInChildren<UnityEngine.UI.Text>();
			ps = gameObject.GetComponentInParent<PlayerStats> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
            healthText.text = ps.hp + "/" + ps.maxHealth;
            healthSlider.value = (int)((float)(ps.hp / ps.maxHealth) * 100);
		}
}
 