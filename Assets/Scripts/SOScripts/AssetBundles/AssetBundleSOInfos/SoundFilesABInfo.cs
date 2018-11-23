using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundFilesAssetBundleInfo", menuName = "Asset Bundle Informations/Sound Files")]
public class SoundFilesABInfo : ABInfoSOData {
	[Space]
	
	public string levelOnGoingMusicName;
	public string levelOnCompleteMusicName;
	public string fireBulletSfxName;
	public string blastSfxName;
}
