using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetReferenceManager : MonoBehaviour {
	public AssetBundlesHandler assetBundlesHandler;
	public CoreMechanicsABInfo coreMechanicsABInfo;
	public SODatasABInfo sODatas;
	public GameManager gameManager;

	void Start(){

	}

	public void SetReferenceToElements(){
		//SODatas
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<LevelsSOData>(sODatas,sODatas.levelSODataName,SetLevelSOData);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<ScoreSOData>(sODatas,sODatas.scoreSODataName,SetScoreSOData);
		//Plane//AI//Bullets
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.playerPlanePrefabName,SetPlayerPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.aIPlanePrefabName,SetAIPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.bulletPrefabName,SetBulletPrefab);


	}

	void SetPlayerPlane(GameObject planeObj){
		gameManager.levelManager.planeSpawnManager.SetPlayerPlanePrefab(planeObj.GetComponent<Plane>());
		// Debug.Log("SetPlayerPlane Success");
	}

	void SetAIPlane(GameObject planeObj){
		gameManager.levelManager.planeSpawnManager.SetAIPlanePrefab(planeObj.GetComponent<Plane>());
		// Debug.Log("SetAIPlane Success");
	}

	void SetBulletPrefab(GameObject bulletObj){
		gameManager.levelManager.playerPlaneController.bulletPool.CreatePool(bulletObj.GetComponent<Bullet>());
		// Debug.Log("SetBulletPrefab Success");
	}

	void SetLevelSOData(LevelsSOData levelSOData){
		gameManager.levelManager.levelsSOData = levelSOData;
		// Debug.Log("SetBulletPrefab Success");
	}

	void SetScoreSOData(ScoreSOData scoreSOData){
		gameManager.levelManager.scoreController.scoreSOData = scoreSOData;
		// Debug.Log("SetBulletPrefab Success");
	}
}
