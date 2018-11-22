using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawnManager : MonoBehaviour {

	[SerializeField] Transform[] playerSpawnPosition;
	[SerializeField] PlanePool aIPlanePool;
	Plane planePrefab;
	List<Plane> playerPlanesPool;
	[SerializeField] Transform[] aISpawnPositions;
	int currentSpawnIndex;

	Plane tempPlane;
	public void SetPlayerPlanePrefab(Plane planePrefab){
		this.planePrefab = planePrefab;
	}
	public void PoolPlayerPlanes(int numOfPlayers = 1){
		if(playerPlanesPool==null) 
			playerPlanesPool = new List<Plane>();
		if(playerPlanesPool.Count>=numOfPlayers)
			return;
		for (int i = playerPlanesPool.Count; i < numOfPlayers; i++)
		{
			playerPlanesPool.Add(Instantiate (planePrefab,transform));
			((PlayerPlane)playerPlanesPool[i]).playerNumber = (i+1);
			playerPlanesPool[i].gameObject.SetActive(false);			
		}
	}
	public Plane SpawnPlayerPlanes(PlayerDataAndControl playerPlaneDataAndControl, APlaneContoller planeContoller, System.Action<Plane> onDeathCallback, int playerNumber=1){
		playerPlanesPool[playerNumber-1].InitPlane(playerPlaneDataAndControl.planeSOData, planeContoller, playerSpawnPosition[playerNumber-1]);
		((PlayerPlane)playerPlanesPool[playerNumber-1]).keyControls = playerPlaneDataAndControl.keyBoardControl;
		playerPlanesPool[playerNumber-1].onDeath += onDeathCallback;
		playerPlanesPool[playerNumber-1].gameObject.SetActive(true);
		return playerPlanesPool[playerNumber-1];
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
		planePrefab.SetPlaneSprite(sprite);	
		aIPlanePool.SetAllPlaneSprite(sprite);
	}

	public void DisableAllAIPlanes(){
		aIPlanePool.DisableAllPoolObjects();
	}

	public void DisableAllPlayerPlanes(){
		for (int i = 0; i < playerPlanesPool.Count; i++)
		{
			playerPlanesPool[i].gameObject.SetActive(false);
		}
	}
	
}
