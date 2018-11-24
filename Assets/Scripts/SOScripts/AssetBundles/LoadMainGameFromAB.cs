using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGameFromAB : MonoBehaviour {

	[Tooltip("Path Relative to Streaming Assets")]public string sceneABPath;
	public string sceneName;
	[Space]
	[Tooltip("Path Relative to Streaming Assets")]public string assetBundlesInfoABPath;
	public string assetBundlesInfo;
	// Use this for initialization
	void Awake () {
		AssetBundle.LoadFromFileAsync(System.IO.Path.Combine(UnityEngine.Application.streamingAssetsPath,sceneABPath)).completed+=LoadScene;
	}
	
	void LoadScene(AsyncOperation asyncOperation){
		AssetBundle assetBundle = ((AssetBundleCreateRequest)asyncOperation).assetBundle;
		SceneManager.LoadSceneAsync(sceneName).completed+=OnSceneLoaded;
	}
	
	void OnSceneLoaded(AsyncOperation asyncOperation){
		AssetBundle.LoadFromFileAsync(System.IO.Path.Combine(UnityEngine.Application.streamingAssetsPath,assetBundlesInfoABPath)).completed+=SetAssetBundleInfo;
	}
	
	void SetAssetBundleInfo(AsyncOperation asyncOperation){
		AssetBundle assetBundle = ((AssetBundleCreateRequest)asyncOperation).assetBundle;
		assetBundle.LoadAssetAsync<ScriptableObject>(assetBundlesInfo).completed+=(AsyncOperation assetBundleRequest)=>{
			AssetBundlesABInfo assetBundlesABInfo = (AssetBundlesABInfo)((AssetBundleRequest)assetBundleRequest).asset;
			GameObject.FindObjectOfType<AssetReferenceManager>().LoadAssetBundles(assetBundlesABInfo);
		};
	}
}
