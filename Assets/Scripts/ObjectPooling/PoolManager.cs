using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	public PlaneSpawnManager planeSpawnManager;
	public BulletPool bulletPool;
	public void InitParam(LevelManager levelManager,int numOfPlayers = 1){
		levelManager.playerPlaneController.SetBulletPool(bulletPool);
		levelManager.aIPlaneController.SetBulletPool(bulletPool);
		planeSpawnManager.PoolPlayerPlanes(numOfPlayers);
		planeSpawnManager.DisableAllPlayerPlanes();
	}

	public void ResetParam(LevelManager levelManager){
		planeSpawnManager.DisableAllAIPlanes();
		bulletPool.DisableAllPoolObjects();
	}
}
