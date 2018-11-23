using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AIWaveData{
	[Tooltip("Plane Data SO of AI")]public AIPlaneSOData aIPlaneSOData;
	[Tooltip("number of spawns in this wave")]public int numberOfSpawns;
	[Tooltip("")]public float timeDiffToSpawn;
}
[System.Serializable]
public struct LevelData{
	[Tooltip("")]public AWinCondition winCondition;
	[Tooltip("")]public float timeDiffBetweenWaves;
	[Tooltip("")]public List<AIWaveData> enemySpawnSequence;
}

[CreateAssetMenu(fileName = "LevelsSOData", menuName = "Level/Level Information")]
public class LevelsSOData : ScriptableObject {

	[Tooltip("is win condition for all levels same or not")]public bool isCommonWinCondition;
	[Tooltip("Common win condition")][SerializeField] AWinCondition winCondition;
	[Tooltip("Multiplayer data (list of player data and controls)")]public MultiPlayerSOData multiplayerSOData;
	[Tooltip("Total number of levels in a game session")]public int totalNumOfLevels = 5;
	[Tooltip("Rest period before next level starts")]public float timeDifferenceBetweenLevels = 5;
	[Tooltip("List of level Datas")]public List<LevelData> levelDatas;

	public LevelData GetLevelData(int level){
		if(level<1 || level>totalNumOfLevels)
			Debug.LogAssertion("Invalid level Number: "+level); 

		return GetCorrectLevelData(level);		
	}

	LevelData GetCorrectLevelData(int level){
		LevelData levelData = levelDatas[level-1];
		if(isCommonWinCondition) levelData.winCondition = winCondition;
		return levelData;
	}

}
