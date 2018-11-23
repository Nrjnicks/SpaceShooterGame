using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetReferenceManager : MonoBehaviour {
	public AssetBundlesHandler assetBundlesHandler;
	[Space]
	//references to asset bundle information (Stored in SO files for ease of project)
	public CoreMechanicsABInfo coreMechanicsABInfo;
	public SODatasABInfo sODatasInfo;
	public AestheticsABInfo aestheticsABInfo;
	public SoundFilesABInfo soundFilesABInfo;
	[Space]
	public GameManager gameManager;

	void Start(){

	}

	///<discription>Set all inter-dependencies from asset bundles to in-game elements</discription>
	public void SetReferenceToElements(){
		//SODatas
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<LevelsSOData>(sODatasInfo,sODatasInfo.levelSODataName,SetLevelSOData);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<ScoreSOData>(sODatasInfo,sODatasInfo.scoreSODataName,SetScoreSOData);

		//Plane//AI//Bullets
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.playerPlanePrefabName,SetPlayerPlane);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.aIPlanePrefabName,SetAIPlane);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<GameObject>(coreMechanicsABInfo,coreMechanicsABInfo.bulletPrefabName,SetBulletPrefab);

		//Aethetics
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Font>(aestheticsABInfo,aestheticsABInfo.fontName,SetTextsFont);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Sprite>(aestheticsABInfo,aestheticsABInfo.planeSpriteName,SetPlaneSprite);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Sprite>(aestheticsABInfo,aestheticsABInfo.healthBarSpriteName,SetHealthBarSprite);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Material>(aestheticsABInfo,aestheticsABInfo.verticleScrollerMaterialName,SetBackgroundMaterial);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Sprite>(aestheticsABInfo,aestheticsABInfo.bulletSpriteName,SetBulletSprite);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Sprite>(aestheticsABInfo,aestheticsABInfo.blastSpriteName,SetBlastSprite);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<Material>(aestheticsABInfo,aestheticsABInfo.healthBarBlinkMaterialName,SetHealthBarBlinkMaterial);

		//Sound
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<AudioClip>(soundFilesABInfo,soundFilesABInfo.levelOnGoingMusicName,SetLevelOnGoingMusic);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<AudioClip>(soundFilesABInfo,soundFilesABInfo.levelOnCompleteMusicName,SetLevelOnCompleteMusic);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<AudioClip>(soundFilesABInfo,soundFilesABInfo.fireBulletSfxName,SetFireBulletSFX);
		assetBundlesHandler.LoadAndCacheAssetBundleAsyn<AudioClip>(soundFilesABInfo,soundFilesABInfo.blastSfxName,SetBlastSFX);

		
		assetBundlesHandler.UnloadAllCachedAssetBundle(false);

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

	void SetBlastSprite(Sprite blastSprite){
		gameManager.levelManager.poolManager.blastPool.SetAllBlastSprite(blastSprite);
	}

	void SetHealthBarSprite(Sprite healthSprite){
		gameManager.levelManager.poolManager.planeSpawnManager.SetAllPlaneHealthBarSprite(healthSprite);
	}

	void SetTextsFont(Font font){
		gameManager.hUDController.SetFontForAllText(font);
	}

	void SetBackgroundMaterial(Material material){
		gameManager.levelManager.SetBackgroundSpriteMaterial(material);
	}

	void SetHealthBarBlinkMaterial(Material material){
		gameManager.levelManager.poolManager.planeSpawnManager.SetHealthBlinkMaterial(material);
	}

	void SetLevelOnGoingMusic(AudioClip audioClip){
		gameManager.levelManager.soundManager.levelOnGoingMusic = audioClip;
	}

	void SetLevelOnCompleteMusic(AudioClip audioClip){
		gameManager.levelManager.soundManager.levelOnCompleteMusic = audioClip;
	}

	void SetFireBulletSFX(AudioClip audioClip){
		gameManager.levelManager.soundManager.fireBulletSfx = audioClip;
	}

	void SetBlastSFX(AudioClip audioClip){
		gameManager.levelManager.soundManager.blastSfx = audioClip;
	}
}
