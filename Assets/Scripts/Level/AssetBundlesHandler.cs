using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetBundlesHandler : MonoBehaviour {
	
	Dictionary<ABInfoSOData, AssetBundle> cachedAssetBundle;
	// Use this for initialization
	void Start () {
		cachedAssetBundle = new Dictionary<ABInfoSOData, AssetBundle>();
	}

	void LoadAssetBundle(ABInfoSOData assetBundleInfo, System.Action OnBundleLoad){
		AssetBundleCreateRequest assetBundleCreateRequest = (AssetBundleCreateRequest)AssetBundle.LoadFromFileAsync(assetBundleInfo.assetBundlePath);
		
		cachedAssetBundle[assetBundleInfo] = assetBundleCreateRequest.assetBundle;

		assetBundleCreateRequest.completed+= (AsyncOperation asyncOperation)=>{OnBundleLoad();};
	}

	public void CacheAndLoadAsynFromAssetBundle<T>(ABInfoSOData assetBundleInfo, string assetName, System.Action<T> OnCompleteCallBack)where T : Object{
		if(cachedAssetBundle.ContainsKey(assetBundleInfo)) {
			OnCompleteCallBack(GetObjectFromAssetBundle<T>(cachedAssetBundle[assetBundleInfo],assetName));
			return;
		}
		LoadAssetBundle(assetBundleInfo, ()=>{OnCompleteCallBack(GetObjectFromAssetBundle<T>(cachedAssetBundle[assetBundleInfo],assetName));});
	}

	T GetObjectFromAssetBundle<T>(AssetBundle assetBundle, string assetName) where T : Object{
		return assetBundle.LoadAsset<T>(assetName);
	}


	public void UnloadAllCachedAssetBundle(bool removeReferences){
		foreach(AssetBundle assetBundle in cachedAssetBundle.Values){
			assetBundle.Unload(removeReferences);
		}
		cachedAssetBundle = null;
	}
	void OnDestroy(){
		UnloadAllCachedAssetBundle(true);
	}
}
