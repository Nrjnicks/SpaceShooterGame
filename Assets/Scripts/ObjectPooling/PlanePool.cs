using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : ObjectPool<Plane> {

	///<description>Spawn Plane with these parametes</description>
	public Plane SpawnPlane(PlaneSOData planeSOData, APlaneContoller planeContoller, Transform spawnPosRot){
		Plane plane= GetNextUnusedPooledObject();
		plane.InitPlane(planeSOData, planeContoller,spawnPosRot);
        return plane;
	}


	///<description>Set Sprite of all planes</description>
	public void SetAllPlaneSprite(Sprite sprite){
		objectForPool.SetPlaneSprite(sprite);
		
		List<Plane> planePool = GetPoolList();
		foreach (Plane plane in planePool)
		{			
			plane.SetPlaneSprite(sprite);
		}
	}

	///<description>Set health bar sprite to make fill amount work</description>
	public void SetAllHealthBarSprite(Sprite sprite){
		objectForPool.healthModel.healthController.healthView.healthBar.sprite = sprite;
		
		List<Plane> planePool = GetPoolList();
		foreach (Plane plane in planePool)
		{			
			plane.healthModel.healthController.healthView.healthBar.sprite = sprite;
		}
	}
}
