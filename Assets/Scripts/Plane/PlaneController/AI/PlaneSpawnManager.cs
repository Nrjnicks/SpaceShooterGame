using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawnManager : MonoBehaviour {

	[SerializeField] Transform playerSpawnPosition;
	[SerializeField] PlanePool aIPlanePool;
	Plane playerPlane;
	[SerializeField] Transform[] aISpawnPositions;
	int currentSpawnIndex;

	Plane tempPlane;

	public void SetPlayerPlanePrefab(Plane planePrefab){
		playerPlane = Instantiate (planePrefab);
		playerPlane.transform.SetParent(transform);
		playerPlane.gameObject.SetActive(false);
	}
	public Plane SpawnPlayerPlanes(PlaneSOData playerPlaneData, APlaneContoller planeContoller, System.Action<Plane> onDeathCallback){
		playerPlane.InitPlane(playerPlaneData, planeContoller, playerSpawnPosition);
		playerPlane.onDeath += onDeathCallback;
		playerPlane.gameObject.SetActive(true);
		return playerPlane;
	}

	public void SetAIPlanePrefab(Plane planePrefab){
		aIPlanePool.CreatePool(planePrefab);
	}
	public IEnumerator SpawnPlanesForLevel(APlaneContoller planeContoller, LevelData levelData, System.Action<Plane> onDeathCallback){		
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
			yield return new WaitForSeconds(levelData.timeDiffBetweenWaves);
		}
	}

	public void SetAllPlaneSprite(Sprite sprite){
		playerPlane.SetPlaneSprite(sprite);
		aIPlanePool.SetAllPlaneSprite(sprite);
	}

	public void DisableAllAIPlanes(){
		aIPlanePool.DisableAllPoolObjects();
	}
	
}
