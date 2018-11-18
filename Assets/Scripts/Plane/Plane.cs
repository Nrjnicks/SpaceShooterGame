using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class Plane : MonoBehaviour, IHealthable {

	public PlaneSOData PlaneData{get; private set;}
	APlaneContoller PlaneContoller;
	public HealthModel healthModel;
	float activateTime;
	public float ActiveTime{get{return Time.time - activateTime;}}

	void ResetParams(){
		
		onDeath=null;
	}

	public void InitPlane(PlaneSOData PlaneData, APlaneContoller PlaneContoller){
		ResetParams();
		this.PlaneData = PlaneData;
		this.PlaneContoller = PlaneContoller;
		healthModel.InitParams(this, PlaneData.maxHealth, OnPlaneDeath);
		activateTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(PlaneContoller!=null)
			PlaneContoller.UpdateControls(this);
	}	

	public bool IsAttackInCoolDown{ get; private set;}
	public void FireBullet(){
		if(IsAttackInCoolDown) return;
		
		PlaneContoller.FireBullet(this);

		IsAttackInCoolDown = true;
		Invoke("RemoveCoolDownStatus", PlaneData.attackCooldown);
	}

	void RemoveCoolDownStatus(){
		IsAttackInCoolDown = false;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(gameObject.layer!=collider.gameObject.layer)
		OnHit(collider.gameObject.GetComponent<IHealthable>());
	}

	void OnHit(IHealthable healthable){
		if(onHit!=null) onHit(healthable);
	}

	public void DisablePlane(){
		gameObject.SetActive(false);
	}

	public void OnPlaneDeath(){
		DisablePlane();
		if(onDeath!=null) onDeath();
	}
	
	public System.Action onDeath;


	public float inflictingDamageAmount{get{return PlaneData.maxHealth;}}
	public System.Action<IHealthable> onHit{get;set;}
}
