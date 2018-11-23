using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetBundlesABInfo", menuName = "Asset Bundle Informations/Asset Bundles")]
public class AssetBundlesABInfo : ABInfoSOData {
	[Space]
	public string coreMechanicsABInfoName;
	public string sODatasABInfoName;
	public string aestheticsABInfoName;
	public string soundFilesABInfoName;
}
