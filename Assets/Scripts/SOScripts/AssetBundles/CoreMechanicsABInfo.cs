using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoreMechanicsAssetBundleInfoSOData", menuName = "Asset Bundle Informations/Core Mechanics")]
public class CoreMechanicsABInfo : ABInfoSOData {
	[Space]
	public string playerPlanePrefabName;
	public string aIPlanePrefabName;
	public string bulletPrefabName;
}
