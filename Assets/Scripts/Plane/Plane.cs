using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class Plane : MonoBehaviour, IHealthable {

	public PlaneSOData planeData{get; private set;}
	APlaneContoller planeContoller;
	public HealthModel healthModel;
	[Space]
	[SerializeField] SpriteRenderer planeSprite;

	void ResetParams(){
		
		onDeath=null;
	}

	public virtual void InitPlane(PlaneSOData planeData, APlaneContoller PlaneContoller, Transform spawnPosRot = null){
		ResetParams();
		EnablePlane();
		this.planeData = planeData;
		this.planeContoller = PlaneContoller;
		healthModel.InitParams(this, planeData.maxHealth, OnPlaneDeath);
		planeSprite.color = planeData.planeColor;
		if(spawnPosRot!=null){
			transform.position = spawnPosRot.position;
			transform.rotation = spawnPosRot.rotation;
		}
	}

	public void SetPlaneSprite(Sprite sprite){
		planeSprite.sprite = sprite;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(planeContoller!=null)
			planeContoller.UpdateControls(this);
	}	

	public bool IsAttackInCoolDown{ get; private set;}
	public virtual void FireBullet(){
		if(IsAttackInCoolDown) return;
		
		planeContoller.FireBullet(this);

		IsAttackInCoolDown = true;
		Invoke("RemoveCoolDownStatus", planeData.attackCooldown);
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
		if(onDeath!=null) onDeath(this);
	}
	
	public System.Action<Plane> onDeath;


	public float inflictingDamageAmount{get{return planeData.maxHealth;}}
	public System.Action<IHealthable> onHit{get;set;}
}
