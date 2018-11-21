using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetReferenceManager : MonoBehaviour {
	public AssetBundlesHandler assetBundlesHandler;
	public CoreMechanicsABInfo coreMechanicsABInfo;
	public SODatasABInfo sODatasInfo;
	public AestheticsABInfo aestheticsABInfo;
	[Space]
	public GameManager gameManager;
	public SpriteRenderer sprite;

	void Start(){

	}

	public void SetReferenceToElements(){
		//SODatas
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<LevelsSOData>(sODatasInfo,sODatasInfo.levelSODataName,SetLevelSOData);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<ScoreSOData>(sODatasInfo,sODatasInfo.scoreSODataName,SetScoreSOData);

		//Plane//AI//Bullets
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.playerPlanePrefabName,SetPlayerPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.aIPlanePrefabName,SetAIPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.bulletPrefabName,SetBulletPrefab);

		//Aethetics
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<Sprite>(aestheticsABInfo,aestheticsABInfo.planeSpriteName,SetPlaneSprite);
	}

	void SetPlayerPlane(GameObject planeObj){
		gameManager.levelManager.planeSpawnManager.SetPlayerPlanePrefab(planeObj.GetComponent<Plane>());
	}

	void SetAIPlane(GameObject planeObj){
		gameManager.levelManager.planeSpawnManager.SetAIPlanePrefab(planeObj.GetComponent<Plane>());
	}

	void SetBulletPrefab(GameObject bulletObj){
		gameManager.levelManager.playerPlaneController.bulletPool.CreatePool(bulletObj.GetComponent<Bullet>());
	}

	void SetLevelSOData(LevelsSOData levelSOData){
		gameManager.levelManager.levelsSOData = levelSOData;
	}

	void SetScoreSOData(ScoreSOData scoreSOData){
		gameManager.levelManager.scoreController.scoreSOData = scoreSOData;
	}

	void SetPlaneSprite(Sprite planeSprite){
		gameManager.levelManager.planeSpawnManager.SetAllPlaneSprite(planeSprite);
	}

	void SetHealthBarBlinkShader(){
	}
}
