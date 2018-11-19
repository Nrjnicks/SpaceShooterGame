using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaneSpawnManager : MonoBehaviour {

	[SerializeField] PlanePool planePool;
	[SerializeField] Transform[] spawnPositions;
	int currentSpawnIndex;

	Plane tempPlane;

	public IEnumerator SpawnPlanesForLevel(APlaneContoller planeContoller, LevelData levelData, System.Action onDeathCallback){		
		currentSpawnIndex = 0;
		foreach(NoOfAIPerType aIPerType in levelData.enemySpawnSequence)
		{
			for (int i = 0; i < aIPerType.numberOfSpawns; i++)
			{			
				tempPlane = planePool.SpawnPlane(aIPerType.aIPlaneSOData, planeContoller, spawnPositions[currentSpawnIndex].position);
				tempPlane.onDeath+=onDeathCallback;
				currentSpawnIndex = (currentSpawnIndex+1)%spawnPositions.Length;
				yield return new WaitForSeconds(aIPerType.spawnFrequency);
			}
			yield return new WaitForSeconds(levelData.sequenceSpawnFrequency);
		}
	}
}
