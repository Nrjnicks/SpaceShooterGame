using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiPlayerSOData", menuName = "Plane Data/MultiPlayer")]
public class MultiPlayerSOData : ScriptableObject {

	[Tooltip("List of PlayerData and Keyboard control keys for multiplayer")]
	public List<PlayerDataAndControl> playerDataAndControls;
}
