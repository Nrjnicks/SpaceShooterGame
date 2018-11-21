using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetBundlesHandler : MonoBehaviour {
	
	static Dictionary<ABInfoSOData, AssetBundle> cachedAssetBundle;
	// Use this for initialization
	void Start () {
		cachedAssetBundle = new Dictionary<ABInfoSOData, AssetBundle>();
	}

	void LoadAssetBundle(ABInfoSOData assetBundleInfo, System.Action OnBundleLoad){
		AssetBundleCreateRequest assetBundleCreateRequest = (AssetBundleCreateRequest)AssetBundle.LoadFromFileAsync(assetBundleInfo.assetBundlePath);
		
		cachedAssetBundle[assetBundleInfo] = assetBundleCreateRequest.assetBundle;

		assetBundleCreateRequest.completed+= (AsyncOperation asyncOperation)=>{OnBundleLoad();};
	}

	public void CacheAndLoadAsynFromAssetBundle(ABInfoSOData assetBundleInfo, string assetName, System.Action<GameObject> OnCompleteCallBack){
		if(cachedAssetBundle.ContainsKey(assetBundleInfo)) {
			OnCompleteCallBack(GetObjectFromAssetBundle(cachedAssetBundle[assetBundleInfo],assetName));
			return;
		}
		LoadAssetBundle(assetBundleInfo, ()=>{OnCompleteCallBack(GetObjectFromAssetBundle(cachedAssetBundle[assetBundleInfo],assetName));});
	}

	GameObject GetObjectFromAssetBundle(AssetBundle assetBundle, string assetName){
		return assetBundle.LoadAsset<GameObject>(assetName);
	}


	public void UnloadAllCachedAssetBundle(bool removeReferences){
		foreach(AssetBundle assetBundle in cachedAssetBundle.Values){
			assetBundle.Unload(removeReferences);
		}

	}
	void OnDestroy(){
		UnloadAllCachedAssetBundle(true);
	}
}
