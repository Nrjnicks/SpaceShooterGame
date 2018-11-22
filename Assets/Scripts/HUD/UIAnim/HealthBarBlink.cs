using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBlink : MonoBehaviour {
	[SerializeField] HealthController healthController;
	[SerializeField] [Range(0,1)] float percToStartBlink = 0.2f;
	[SerializeField] UnityEngine.UI.Image healthBar;
	[SerializeField] Shader shader;
	Material blinkMat;
	// Use this for initialization
	void Start () {
		SetShader(shader);
	}

	public void SetShader(Shader shader){
		blinkMat = new Material(shader);//new instance for individuality
		blinkMat.SetColor("_Color", healthBar.color);
		blinkMat.SetTexture("_MainTex", healthBar.mainTexture);
		blinkMat.SetFloat("_Blink", 0);
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
		healthBar.material = null;
	}
}
