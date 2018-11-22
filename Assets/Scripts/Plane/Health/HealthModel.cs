using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : MonoBehaviour {

	public HealthController healthController;
	IHealthable actor;
	public float maxHealth{get; private set;}
	[HideInInspector] public float currentHealth;

	///<description>Setting Health and IHealthable params and callbacks for DeathEvent</description>
	public void InitParams(IHealthable actor, float maxHealth, System.Action OnDeathCallback){
		this.actor = actor;
		this.maxHealth = maxHealth;
		this.currentHealth = maxHealth;
		this.actor.onHit += healthController.OnActorHit;
		healthController.InitParams(this);
		healthController.onDeath += OnDeathCallback;
		healthController.onDeath += OnDeath;
	}

	void OnDeath(){
		this.actor.onHit -= healthController.OnActorHit; 
		healthController.onDeath -= OnDeath;
	}
}
