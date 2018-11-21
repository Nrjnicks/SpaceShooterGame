using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NoOfAIPerType{
	public AIPlaneSOData aIPlaneSOData;
	public int numberOfSpawns;
	public float spawnFrequency;
}
[System.Serializable]
public struct LevelData{
	public AWinCondition winCondition;
	public float timeDiffBetweenWaves;
	public List<NoOfAIPerType> enemySpawnSequence;
}

[CreateAssetMenu(fileName = "LevelsSOData", menuName = "Level/Level Information")]
public class LevelsSOData : ScriptableObject {

	public bool isCommonWinCondition;
	[SerializeField] AWinCondition winCondition;
	public PlaneSOData playerPlaneSOData;
	public int totalNumOfLevels = 5;
	public float timeDifferenceBetweenLevels = 5;
	public List<LevelData> levelDatas;

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
