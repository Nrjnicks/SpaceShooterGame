using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	public PlaneSpawnManager planeSpawnManager;
	public BulletPool bulletPool;
	public BlastPool blastPool;

	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	///<param name="numOfPlayers">num Of Players</param>
	public void InitParam(LevelManager levelManager,int numOfPlayers = 1){
		levelManager.playerPlaneController.onFireShot+=OnFire;
		levelManager.aIPlaneController.onFireShot+=OnFire;
		blastPool.InitParam(levelManager);
		planeSpawnManager.PoolPlayerPlanes(numOfPlayers);
		planeSpawnManager.DisableAllPlayerPlanes(); //Disable Pool Elements
	}

	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public void ResetParam(LevelManager levelManager){
		levelManager.playerPlaneController.onFireShot-=OnFire;
		levelManager.aIPlaneController.onFireShot-=OnFire;
		blastPool.ResetParam(levelManager);
		planeSpawnManager.DisableAllAIPlanes(); //Disable Pool Elements
		bulletPool.DisableAllPoolObjects(); //Disable Pool Elements
		blastPool.DisableAllPoolObjects(); //Disable Pool Elements
	}

	void OnFire(Plane plane){
		bulletPool.FireBullet(plane.transform, plane.planeData.bulletSpeed,plane.planeData.bulletStrength);
	}
}
