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
		
	}

	///<description>Set Blink Material so it can be be switched on event</description>
	public void SetBlinkMaterial(Material mat){
		blinkMat = new Material(mat);//new instance for individuality
		blinkMat.SetColor("_Color", healthBar.color);
		blinkMat.SetTexture("_MainTex", healthBar.mainTexture);
		blinkMat.SetFloat("_Blink", 0);
	}

	void OnEnable(){		
		healthController.onHealthChange+=OnHealthChange;
	}
	
	void OnHealthChange(float currentHealth, float maxHealth){		
		if(currentHealth/maxHealth<percToStartBlink)//attach blink material once condition reached
		{
			healthController.onHealthChange-=OnHealthChange;
			healthBar.material = blinkMat;
			healthBar.material.SetFloat("_Blink", 1);
		}
	}

	void OnDisable(){		
		healthController.onHealthChange-=OnHealthChange;
		healthBar.material = null;
	}
}
