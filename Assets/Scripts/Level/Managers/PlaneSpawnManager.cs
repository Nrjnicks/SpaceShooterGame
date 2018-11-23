using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawnManager : MonoBehaviour {

	[SerializeField] Transform[] playerSpawnPosition;
	Plane planePrefab;
	List<Plane> playerPlanesPool;

	[SerializeField] Transform[] aISpawnPositions;
	[SerializeField] PlanePool aIPlanePool;
	int currentSpawnIndex;
	
	///<description>Set Player plane prefab for pseudo pool instantitation</description>
	public void SetPlayerPlanePrefab(Plane planePrefab){
		this.planePrefab = planePrefab;
	}

	
	///<description>Pseudo Pool for Player plane</description>
	public void PoolPlayerPlanes(int numOfPlayers = 1){
		if(playerPlanesPool==null) 
			playerPlanesPool = new List<Plane>();
		if(playerPlanesPool.Count>=numOfPlayers)
			return;
		for (int i = playerPlanesPool.Count; i < numOfPlayers; i++)//Instantiate new plane if required or lese, just one
		{
			playerPlanesPool.Add(Instantiate (planePrefab,transform));
			((PlayerPlane)playerPlanesPool[i]).playerNumber = (i+1);
			playerPlanesPool[i].gameObject.SetActive(false);			
		}
	}

	///<description>Pseudo Pool for Player plane</description>
	///<param name="playerPlaneDataAndControl">Plane Data and Keyboard controller </param>
	///<param name="planeContoller">Plane Contoller</param>
	///<param name="onDeathCallback">onDeathCallback</param>
	///<param name="playerNumber">Player Number</param>
	public Plane SpawnPlayerPlanes(PlayerDataAndControl playerPlaneDataAndControl, APlaneContoller planeContoller, System.Action<Plane> onDeathCallback, int playerNumber=1){
		playerPlanesPool[playerNumber-1].InitPlane(playerPlaneDataAndControl.planeSOData, planeContoller, playerSpawnPosition[playerNumber-1]);
		((PlayerPlane)playerPlanesPool[playerNumber-1]).keyControls = playerPlaneDataAndControl.keyBoardControl;
		playerPlanesPool[playerNumber-1].onDeath += onDeathCallback;
		playerPlanesPool[playerNumber-1].gameObject.SetActive(true);
		return playerPlanesPool[playerNumber-1];
	}

	///<description>Set AI plane prefab for pool creation</description>
	public void SetAIPlanePrefab(Plane planePrefab){
		aIPlanePool.CreatePool(planePrefab);
	}
	
	///<description>Pseudo Pool for Player plane</description>
	///<param name="planeContoller">Plane Contoller</param>
	///<param name="levelData">level Data contatining spawn frequency and wave delays</param>
	///<param name="onDeathCallback">onDeathCallback</param>
	public IEnumerator SpawnPlanesForLevel(APlaneContoller planeContoller, LevelData levelData, System.Action<Plane> onDeathCallback){		
		currentSpawnIndex = 0;
		
		Plane tempPlane;
		foreach(AIWaveData aIWaveData in levelData.enemySpawnSequence)
		{
			for (int i = 0; i < aIWaveData.numberOfSpawns; i++)
			{			
				tempPlane = aIPlanePool.SpawnPlane(aIWaveData.aIPlaneSOData, planeContoller, aISpawnPositions[currentSpawnIndex]);
				tempPlane.onDeath+=onDeathCallback;
				currentSpawnIndex = (currentSpawnIndex+1)%aISpawnPositions.Length;
				yield return new WaitForSeconds(aIWaveData.timeDiffToSpawn);
			}
			yield return new WaitForSeconds(levelData.timeDiffBetweenWaves);
		}
	}


	///<description>SetAllPlaneSprite</description>
	public void SetAllPlaneSprite(Sprite sprite){
		planePrefab.SetPlaneSprite(sprite);	
		aIPlanePool.SetAllPlaneSprite(sprite);
	}


	///<description>SetAllPlaneHealthBarSprite</description>
	public void SetAllPlaneHealthBarSprite(Sprite sprite){
		planePrefab.healthModel.healthController.healthView.healthBar.sprite = sprite;
		aIPlanePool.SetAllHealthBarSprite(sprite);
	}


	///<description>SetHealthBlinkMaterial</description>
	public void SetHealthBlinkMaterial(Material material){
		planePrefab.GetComponentInChildren<HealthBarBlink>().SetBlinkMaterial(material);
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
