using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour, IHealthable{

	float speed;
	[SerializeField] HealthModel healthModel;
	public float inflictingDamageAmount{get; private set;}
	Vector2 direction;
	Vector2 newPos;
	[Space]
	[SerializeField] SpriteRenderer bulletSprite;
	// Use this for initialization
	public void InitBullet (Vector2 startPos, Vector2 moveDirection, float bulletSpeed = 15, float damageAmount = 10) {
		transform.position = startPos;
		direction = moveDirection.normalized;
		transform.up = direction;
		speed = bulletSpeed;
		inflictingDamageAmount = damageAmount;
		healthModel.InitParams(this, damageAmount, DisableBullet);
	}
	public void SetBulletSprite(Sprite sprite){
		bulletSprite.sprite = sprite;
	}
	
	void Update (){
		newPos = transform.position + (Vector3)direction*speed*Time.deltaTime;
		if(WorldSpaceGameBoundary.Instance.IsPointInsideCameraView(newPos))
			transform.position = newPos;
		else
			DisableBullet();
	}

	void DisableBullet(){
		gameObject.SetActive(false);
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if(onHit!=null) onHit(collider.gameObject.GetComponent<IHealthable>());
		DisableBullet();
	}

	public System.Action<IHealthable> onHit{get;set;}
}
