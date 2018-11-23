using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet> {

	public void FireBullet(Transform _transform, float bulletSpeed, float bulletStrength){		
		FireBullet(_transform.position + _transform.up, _transform.up, bulletSpeed, bulletStrength);
	}
	
	///<description>Fire Bullet and set params</description>
	public void FireBullet(Vector2 position, Vector2 forwardDirection, float bulletSpeed, float bulletStrength){
		Bullet bullet = GetNextUnusedPooledObject();
		bullet.InitBullet(position, forwardDirection, bulletSpeed, bulletStrength);
		bullet.gameObject.SetActive(true);
	}

	///<description>Set Bullet Sprite</description>
	public void SetAllBulletSprite(Sprite sprite){
		objectForPool.SetBulletSprite(sprite);
		
		List<Bullet> bulletPool = GetPoolList();
		foreach (Bullet bullet in bulletPool)
		{			
			bullet.SetBulletSprite(sprite);
		}
	}
}
