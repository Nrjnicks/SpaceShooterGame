using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawnManager : MonoBehaviour {

	[SerializeField] Transform playerSpawnPosition;
	[SerializeField] PlanePool aIPlanePool;
	[SerializeField] Transform[] aISpawnPositions;
	int currentSpawnIndex;

	Plane tempPlane;

	public void SpawnPlayerPlanes(ref Plane planePrefab, PlaneSOData playerPlaneData, APlaneContoller planeContoller, System.Action<PlaneSOData> onDeathCallback){		
		planePrefab = Instantiate(planePrefab);
		planePrefab.InitPlane(playerPlaneData, planeContoller, playerSpawnPosition);
		planePrefab.onDeath += onDeathCallback;
	}
	public void SetAIPlanePrefab(Plane planePrefab){
		aIPlanePool.CreatePool(planePrefab);
	}

	public IEnumerator SpawnPlanesForLevel(APlaneContoller planeContoller, LevelData levelData, System.Action<PlaneSOData> onDeathCallback){		
		currentSpawnIndex = 0;
		foreach(NoOfAIPerType aIPerType in levelData.enemySpawnSequence)
		{
			for (int i = 0; i < aIPerType.numberOfSpawns; i++)
			{			
				tempPlane = aIPlanePool.SpawnPlane(aIPerType.aIPlaneSOData, planeContoller, aISpawnPositions[currentSpawnIndex]);
				tempPlane.onDeath+=onDeathCallback;
				currentSpawnIndex = (currentSpawnIndex+1)%aISpawnPositions.Length;
				yield return new WaitForSeconds(aIPerType.spawnFrequency);
			}
			yield return new WaitForSeconds(levelData.sequenceSpawnFrequency);
		}
	}

	public void DisableAllPlanes(){
		aIPlanePool.DisableAllPoolObjects();
	}
	
}
