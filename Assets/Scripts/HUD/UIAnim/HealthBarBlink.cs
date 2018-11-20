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
		blinkMat = new Material(blinkMat);//new instance for individuality
		blinkMat.SetColor("_Color", healthBar.color);
	}

	void OnEnable(){		
		healthController.onHealthChange+=OnHealthChange;
	}
	
	void OnHealthChange(float currentHealth, float maxHealth){		
		if(currentHealth/maxHealth<percToStartBlink)
		{
			healthController.onHealthChange-=OnHealthChange;
			healthBar.material = blinkMat;
			healthBar.material.SetFloat("_Blink", 1);
		}
	}

	void OnDisable(){		
		healthController.onHealthChange-=OnHealthChange;
		blinkMat.SetFloat("_Blink", 0);
	}
}
