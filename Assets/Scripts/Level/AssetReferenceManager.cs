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
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<Font>(aestheticsABInfo,aestheticsABInfo.fontName,SetTextsFont);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<Sprite>(aestheticsABInfo,aestheticsABInfo.planeSpriteName,SetPlaneSprite);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<Material>(aestheticsABInfo,aestheticsABInfo.verticleScrollerMaterialName,SetBackgroundMaterial);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle<Sprite>(aestheticsABInfo,aestheticsABInfo.bulletSpriteName,SetBulletSprite);
		

		// assetBundlesHandler.UnloadAllCachedAssetBundle(false);
	}

	void SetPlayerPlane(GameObject planeObj){
		gameManager.levelManager.poolManager.planeSpawnManager.SetPlayerPlanePrefab(planeObj.GetComponent<Plane>());
	}

	void SetAIPlane(GameObject planeObj){
		gameManager.levelManager.poolManager.planeSpawnManager.SetAIPlanePrefab(planeObj.GetComponent<Plane>());
	}

	void SetBulletPrefab(GameObject bulletObj){
		gameManager.levelManager.poolManager.bulletPool.CreatePool(bulletObj.GetComponent<Bullet>());
	}

	void SetLevelSOData(LevelsSOData levelSOData){
		gameManager.levelManager.levelsSOData = levelSOData;
	}

	void SetScoreSOData(ScoreSOData scoreSOData){
		gameManager.levelManager.scoreController.scoreSOData = scoreSOData;
	}

	void SetPlaneSprite(Sprite planeSprite){
		gameManager.levelManager.poolManager.planeSpawnManager.SetAllPlaneSprite(planeSprite);
	}

	void SetBulletSprite(Sprite bulletSprite){
		gameManager.levelManager.poolManager.bulletPool.SetAllBulletSprite(bulletSprite);
	}

	void SetTextsFont(Font font){
		gameManager.hUDController.SetFontForAllText(font);
	}

	void SetBackgroundMaterial(Material material){
		gameManager.levelManager.SetBackgroundSpriteShader(material);
	}
}
