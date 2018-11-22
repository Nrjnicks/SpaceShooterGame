using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet> {
	
	public void FireBulletFromPool(Vector2 position, Vector2 forwardDirection, float bulletSpeed, float bulletStrength){
		Bullet bullet = GetNextUnusedPooledObject();
		bullet.InitBullet(position, forwardDirection, bulletSpeed, bulletStrength);
		bullet.gameObject.SetActive(true);
	}

	public void SetAllBulletSprite(Sprite sprite){
		objectForPool.SetBulletSprite(sprite);
		
		List<Bullet> bulletPool = GetPoolList();
		foreach (Bullet bullet in bulletPool)
		{			
			bullet.SetBulletSprite(sprite);
		}
	}
}
