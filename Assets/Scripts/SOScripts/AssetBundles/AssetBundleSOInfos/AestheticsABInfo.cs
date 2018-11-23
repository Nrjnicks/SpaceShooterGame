using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AestheticsBundleInfoSOData", menuName = "Asset Bundle Informations/Aesthetics")]
public class AestheticsABInfo : ABInfoSOData {
	[Space]
	public string fontName;	
	public string planeSpriteName;
	public string bulletSpriteName;
	public string blastSpriteName;
	public string healthBarSpriteName;
	public string verticleScrollerMaterialName;
	public string healthBarBlinkMaterialName;
}
