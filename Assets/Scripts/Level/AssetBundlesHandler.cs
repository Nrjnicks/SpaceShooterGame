using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetBundlesHandler : MonoBehaviour {
	
	Dictionary<ABInfoSOData, AssetBundle> cachedAssetBundle; //caching assetbundles
	// Use this for initialization
	void Start () {
		cachedAssetBundle = new Dictionary<ABInfoSOData, AssetBundle>();
	}
	///<description>Load AssetBundle Async</description>
	///<param name="assetBundleInfo">Information of asset bundle stored in SO format</param>
	///<param name="OnBundleLoad">Event Callback once asset bundle is loaded</param>
	void LoadAssetBundle(ABInfoSOData assetBundleInfo, System.Action OnBundleLoad){
		AssetBundleCreateRequest assetBundleCreateRequest = (AssetBundleCreateRequest)AssetBundle.LoadFromFileAsync(Path.Combine(UnityEngine.Application.streamingAssetsPath,assetBundleInfo.assetBundlePath));
		
		cachedAssetBundle[assetBundleInfo] = assetBundleCreateRequest.assetBundle;

		assetBundleCreateRequest.completed+= (AsyncOperation asyncOperation)=>{OnBundleLoad();};
	}

	///Load AssetBundle Async and cache it for reuse
	///<param name="assetBundleInfo">Information of asset bundle stored in SO format</param>
	///<param name="OnCompleteCallBack">Event Callback once asset is loaded</param>
	public void LoadAndCacheAssetBundleAsyn<T>(ABInfoSOData assetBundleInfo, string assetName, System.Action<T> OnCompleteCallBack)where T : Object{
		if(cachedAssetBundle.ContainsKey(assetBundleInfo)) {
			OnCompleteCallBack(GetAssetFromAssetBundle<T>(cachedAssetBundle[assetBundleInfo],assetName));
			return;
		}
		LoadAssetBundle(assetBundleInfo, ()=>{OnCompleteCallBack(GetAssetFromAssetBundle<T>(cachedAssetBundle[assetBundleInfo],assetName));});
	}

	///Get Asset From Asset Bundle
	///<param name="assetBundle">Asset Bundle</param>
	///<param name="assetName">name of the asset to find</param>
	///<return>return asset of type</return>
	T GetAssetFromAssetBundle<T>(AssetBundle assetBundle, string assetName) where T : Object{
		return assetBundle.LoadAsset<T>(assetName);
	}


	///<description>Unload All Cached AssetBundle</description>
	///<param name="removeReferences">remove references from the game or not</param>
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
