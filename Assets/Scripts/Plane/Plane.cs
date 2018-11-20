using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class Plane : MonoBehaviour, IHealthable {

	public PlaneSOData PlaneData{get; private set;}
	APlaneContoller PlaneContoller;
	public HealthModel healthModel;

	void ResetParams(){
		
		onDeath=null;
	}

	public virtual void InitPlane(PlaneSOData PlaneData, APlaneContoller PlaneContoller, Transform spawnPosRot = null){
		ResetParams();
		EnablePlane();
		this.PlaneData = PlaneData;
		this.PlaneContoller = PlaneContoller;
		healthModel.InitParams(this, PlaneData.maxHealth, OnPlaneDeath);

		if(spawnPosRot!=null){
			transform.position = spawnPosRot.position;
			transform.rotation = spawnPosRot.rotation;
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(PlaneContoller!=null)
			PlaneContoller.UpdateControls(this);
	}	

	public bool IsAttackInCoolDown{ get; private set;}
	public virtual void FireBullet(){
		if(IsAttackInCoolDown) return;
		
		PlaneContoller.FireBullet(this);

		IsAttackInCoolDown = true;
		Invoke("RemoveCoolDownStatus", PlaneData.attackCooldown);
	}

	protected virtual void RemoveCoolDownStatus(){
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

	public void EnablePlane(){
		gameObject.SetActive(true);
	}

	public void OnPlaneDeath(){
		DisablePlane();
		if(onDeath!=null) onDeath(PlaneData);
	}
	
	public System.Action<PlaneSOData> onDeath;


	public float inflictingDamageAmount{get{return PlaneData.maxHealth;}}
	public System.Action<IHealthable> onHit{get;set;}
}
