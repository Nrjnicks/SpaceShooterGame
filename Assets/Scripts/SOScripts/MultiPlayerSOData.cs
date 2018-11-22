using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiPlayerSOData", menuName = "Plane Data/MultiPlayer")]
public class MultiPlayerSOData : ScriptableObject {

	public List<PlayerDataAndControl> playerDataAndControls;
}
