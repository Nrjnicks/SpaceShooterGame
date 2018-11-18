using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	int currentLevel;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public PlanePool planePool;
	public Plane playerPlane;
	public PlaneSOData playerPlaneData;
	public HUDController hUDController;
	
	ScoreManager scoreManager;
	LevelData levelData;
	public void InitParam(ScoreManager scoreManager){
		currentLevel = 1;
		this.scoreManager = scoreManager;		
		this.scoreManager.InitParam(playerPlane);
		hUDController.InitParam(scoreManager, playerPlane);
		playerPlaneController.InitControls();
		aIPlaneController.InitControls();
	}

	public void SetUpLevel(){		
		levelData = levelsSOData.GetLevelData(currentLevel);
		StartCoroutine(StartLevel());
	}

	IEnumerator StartLevel(){
		yield return StartCoroutine(SpawnPlayers());
		yield return StartCoroutine(SpawnAIs());
	}

	IEnumerator SpawnPlayers(){
		// Plane plane = planePool.GetNextUnusedPooledObject();
		playerPlane.InitPlane(playerPlaneData, playerPlaneController);
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			planePool.SpawnPlane(aIPlaneController, levelData, OnEnemyKilled);
			yield break;
		}
	}

	
	public void OnEnemyKilled(){
		scoreManager.OnEnemyKilled();
		if(levelsSOData.winCondition.ConditionToWin(scoreManager)) Debug.Log ("WON");
	}
}
