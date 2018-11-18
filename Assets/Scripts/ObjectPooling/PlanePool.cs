using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : ObjectPool<Plane> {
	[SerializeField] Transform[] spawnPositions;
	int currentSpawnIndex;

	public void SpawnPlane(APlaneContoller planeContoller, LevelData levelData, System.Action onDeathCallback = null){
		currentSpawnIndex = 0;
		StartCoroutine(SpawnPlaneCoroutine(planeContoller, levelData, onDeathCallback));
	}

	IEnumerator SpawnPlaneCoroutine(APlaneContoller planeContoller, LevelData levelData, System.Action onDeathCallback){		
		
		foreach(NoOfAIPerType aIPerType in levelData.enemySpawnSequence)
		{
			for (int i = 0; i < aIPerType.numberOfSpawns; i++)
			{				
				Plane plane= GetNextUnusedPooledObject();
				plane.InitPlane(aIPerType.aIPlaneSOData, planeContoller);
				plane.onDeath+=onDeathCallback;
				plane.transform.position = spawnPositions[currentSpawnIndex].position;
				currentSpawnIndex = (currentSpawnIndex+1)%spawnPositions.Length;
				plane.gameObject.SetActive(true);
				yield return new WaitForSeconds(aIPerType.spawnFrequency);
			}
			yield return new WaitForSeconds(levelData.sequenceSpawnFrequency);
		}
	}

}
