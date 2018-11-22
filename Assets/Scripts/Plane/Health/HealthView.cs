using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour {
		public Image healthBar;
		
		///<description>Set Fill Amount [0,1] of health bar</description>
		public void SetHealthBarFillAmount(float fillAmount){
			healthBar.fillAmount = fillAmount;
		}
}
