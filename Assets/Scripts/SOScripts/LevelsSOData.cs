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
	public float sequenceSpawnFrequency;
	public List<NoOfAIPerType> enemySpawnSequence;
}

[CreateAssetMenu(fileName = "LevelsSOData", menuName = "Level/Level Information")]
public class LevelsSOData : ScriptableObject {

	public AWinCondition winCondition;
	public int totalNumOfLevels = 5;
	public List<LevelData> levelDatas;

	public LevelData GetLevelData(int level){
		if(level<1 || level>totalNumOfLevels)
			Debug.LogAssertion("Invalid level Number: "+level); 

		return levelDatas[level-1];
		
	}

}
