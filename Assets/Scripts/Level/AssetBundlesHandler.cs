using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetBundlesHandler : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "coreelements")).completed+=OnAssetBundleLoadComplete;
	}

	void OnAssetBundleLoadComplete(AsyncOperation asyncOperation){
		AssetBundleCreateRequest assetBundleCreateRequest = (AssetBundleCreateRequest)asyncOperation;
			Debug.Log((assetBundleCreateRequest.assetBundle.LoadAsset<GameObject>("Plane")));
		foreach (string item in assetBundleCreateRequest.assetBundle.GetAllAssetNames())
		{
			Debug.Log((assetBundleCreateRequest.assetBundle.LoadAsset<GameObject>(item)));	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
