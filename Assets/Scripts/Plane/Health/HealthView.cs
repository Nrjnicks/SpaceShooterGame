using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour {
		public Image healthBar;
		public void SetHealthBarFillAmount(float currentHealth){//, float maxHealth){
			healthBar.fillAmount = currentHealth;
		}
}
