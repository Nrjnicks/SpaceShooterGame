using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	HealthModel healthModel;
	public HealthView healthView;
	public System.Action onDeath;
	public System.Action<float,float> onHealthChange;
	
	public void InitParams(HealthModel healthModel){
		this.healthModel = healthModel;
		if(onHealthChange!=null) onHealthChange(healthModel.currentHealth, healthModel.maxHealth);
		if(healthView!=null) healthView.SetHealthBarFillAmount(healthModel.currentHealth/healthModel.maxHealth);
	}
	public void OnActorHit(IHealthable collidable){
		if(collidable == null) return;
		
		healthModel.currentHealth-= collidable.inflictingDamageAmount;
		if(healthModel.currentHealth<0) healthModel.currentHealth = 0;
		if(onHealthChange!=null) onHealthChange(healthModel.currentHealth, healthModel.maxHealth);
		if(healthView!=null) healthView.SetHealthBarFillAmount(healthModel.currentHealth/healthModel.maxHealth);
		if(healthModel.currentHealth<=0) OnDeath();
	}

	void OnDeath(){
		if(onDeath!=null) onDeath();
		onDeath = null;//reset
	}

}
