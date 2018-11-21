using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetReferenceManager : MonoBehaviour {
	public AssetBundlesHandler assetBundlesHandler;
	public CoreMechanicsABInfoSOData coreMechanicsABInfo;
	public GameManager gameManager;

	void Start(){

	}

	public void SetReferenceToElements(){
		//Plane//AI//Bullets
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle(coreMechanicsABInfo,coreMechanicsABInfo.playerPlanePrefabName,SetPlayerPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle(coreMechanicsABInfo,coreMechanicsABInfo.aIPlanePrefabName,SetAIPlane);
		assetBundlesHandler.CacheAndLoadAsynFromAssetBundle(coreMechanicsABInfo,coreMechanicsABInfo.bulletPrefabName,SetBulletPrefab);
	}

	void SetPlayerPlane(GameObject planeObj){
		gameManager.levelManager.playerPlane = planeObj.GetComponent<Plane>();
		Debug.Log("SetPlayerPlane Success");
		gameManager.OnAssetLoad();
	}

	void SetAIPlane(GameObject planeObj){
		gameManager.levelManager.planeSpawnManager.SetAIPlanePrefab(planeObj.GetComponent<Plane>());
		Debug.Log("SetAIPlane Success");
	}

	void SetBulletPrefab(GameObject bulletObj){
		gameManager.levelManager.playerPlaneController.bulletPool.CreatePool(bulletObj.GetComponent<Bullet>());
		Debug.Log("SetBulletPrefab Success");
	}
}
