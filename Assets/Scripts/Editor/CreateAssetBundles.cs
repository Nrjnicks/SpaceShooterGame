using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/AssetBundles/Build All AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = UnityEngine.Application.streamingAssetsPath;
        if(!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }
}