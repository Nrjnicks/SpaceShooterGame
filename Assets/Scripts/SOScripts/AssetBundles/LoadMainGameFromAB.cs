using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGameFromAB : MonoBehaviour {

	public string sceneABPath;
	public string sceneName;
	[Space]
	public string assetBundlesInfoABPath;
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
		AssetBundlesABInfo assetBundlesABInfo = (AssetBundlesABInfo) assetBundle.LoadAsset<ScriptableObject>(assetBundlesInfo);
		GameObject.FindObjectOfType<AssetReferenceManager>().LoadAssetBundles(assetBundlesABInfo);
	}
}
