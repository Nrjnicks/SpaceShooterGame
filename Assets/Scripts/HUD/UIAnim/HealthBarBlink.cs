using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBlink : MonoBehaviour {
	[SerializeField] HealthController healthController;
	[SerializeField] [Range(0,1)] float percToStartBlink = 0.2f;
	[SerializeField] UnityEngine.UI.Image healthBar;
	[SerializeField] Material blinkMat;
	// Use this for initialization
	void Start () {
		healthController.onHealthChange+=OnHealthChange;
	}
	
	void OnHealthChange(float currentHealth, float maxHealth){		
		if(currentHealth/maxHealth<percToStartBlink)
		{
			healthController.onHealthChange-=OnHealthChange;
			healthBar.material = new Material(blinkMat);
			healthBar.material.SetColor("_Color", healthBar.color);
			healthBar.material.SetFloat("_Blink", 1);
		}
	}
}
