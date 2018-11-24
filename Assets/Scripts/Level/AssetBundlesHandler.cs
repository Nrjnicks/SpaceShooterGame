using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetBundlesHandler : MonoBehaviour {
	
	Dictionary<ABInfoSOData, AssetBundle> cachedAssetBundle = new Dictionary<ABInfoSOData, AssetBundle>(); //caching assetbundles
	// Use this for initialization
	void Start () {
		cachedAssetBundle = new Dictionary<ABInfoSOData, AssetBundle>();
	}

	///Load AssetBundle Async and cache it for reuse
	///<param name="assetBundleInfo">Information of asset bundle stored in SO format</param>
	///<param name="assetName">name of the asset to find</param>
	///<param name="onCompleteCallBack">Event Callback once asset is loaded</param>
	public void LoadAndCacheAssetBundleAsyn<T>(ABInfoSOData assetBundleInfo, string assetName, System.Action<T> onCompleteCallBack)where T : Object{
		if(cachedAssetBundle.ContainsKey(assetBundleInfo)) {
			OnBundleLoaded<T>(cachedAssetBundle[assetBundleInfo],assetName,onCompleteCallBack);
			return;
		}
		LoadAssetBundle(assetBundleInfo, ()=>{OnBundleLoaded<T>(cachedAssetBundle[assetBundleInfo],assetName,onCompleteCallBack);});
	}

	///<description>Load AssetBundle Async</description>
	///<param name="assetBundleInfo">Information of asset bundle stored in SO format</param>
	///<param name="onBundleLoad">Event Callback once asset bundle is loaded</param>
	void LoadAssetBundle(ABInfoSOData assetBundleInfo, System.Action onBundleLoad){
		AssetBundleCreateRequest assetBundleCreateRequest = (AssetBundleCreateRequest)AssetBundle.LoadFromFileAsync(Path.Combine(UnityEngine.Application.streamingAssetsPath,assetBundleInfo.assetBundlePath));
		
		cachedAssetBundle[assetBundleInfo] = assetBundleCreateRequest.assetBundle;

		assetBundleCreateRequest.completed+= (AsyncOperation asyncOperation)=>{onBundleLoad();};
	}

	///Load asset async
	///<param name="assetBundle">Asset Bundle</param>
	///<param name="assetName">name of the asset to find</param>
	///<param name="onCompleteCallBack">Event Callback once asset is loaded</param>
	void OnBundleLoaded<T>(AssetBundle assetBundle, string assetName,System.Action<T> onCompleteCallBack) where T : Object{
		assetBundle.LoadAssetAsync<T>(assetName).completed +=
								(AsyncOperation asyncOperation)=>{OnAssetLoaded<T>((AssetBundleRequest)asyncOperation,onCompleteCallBack);};
	}

	///Call callback as soon as asset is loaded
	///<param name="assetBundleRequest">AssetBundleRequest which has information of asset</param>
	///<param name="onCompleteCallBack">Event Callback once asset is loaded</param>
	void OnAssetLoaded<T>(AssetBundleRequest assetBundleRequest,System.Action<T> onCompleteCallBack)where T : Object{
		onCompleteCallBack((T)(assetBundleRequest.asset));
	}


	///<description>Unload All Cached AssetBundle</description>
	///<param name="removeReferences">remove references from the game or not</param>
	public void UnloadAllCachedAssetBundle(bool removeReferences){
		if(cachedAssetBundle!=null)
		foreach(AssetBundle assetBundle in cachedAssetBundle.Values){
			assetBundle.Unload(removeReferences);
		}
		cachedAssetBundle = null;
	}
	///<description>Unload All Cached AssetBundle</description>
	///<param name="removeReferences">remove references from the game or not</param>
	
	public void UnloadCachedAssetBundle(ABInfoSOData aBInfoSOData, bool removeReferences = false){
		if(cachedAssetBundle.ContainsKey(aBInfoSOData)) cachedAssetBundle[aBInfoSOData].Unload(removeReferences);
	}
	void OnDestroy(){
		UnloadAllCachedAssetBundle(true);
	}
}
