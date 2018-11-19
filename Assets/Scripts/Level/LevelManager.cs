using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	int currentLevel;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public AIPlaneSpawnManager aIPlaneSpawnManager;
	public Plane playerPlane;
	public PlaneSOData playerPlaneData;
	public HUDController hUDController;
	
	ScoreManager scoreManager;
	LevelData levelData;
	public void InitParam(ScoreManager scoreManager){
		currentLevel = 1;
		this.scoreManager = scoreManager;		
		this.scoreManager.InitParam(this);
		hUDController.InitParam(scoreManager, playerPlane);
		playerPlaneController.InitControls(this);
		aIPlaneController.InitControls(this);

		StartCoroutine(SpawnPlayers());
		SetUpLevel(currentLevel);
	}

	void StartNextLevel(){		
		IncreaseLevelCount();
		if(currentLevel>=levelsSOData.totalNumOfLevels){
			Debug.Log("Game Done. Congrats!");
			return;
		}
		SetUpLevel(currentLevel);
	}

	public void SetUpLevel(int level){		
		levelData = levelsSOData.GetLevelData(level);
		StartCoroutine(StartLevelAfter(levelsSOData.timeDifferenceBetweenLevels));
	}

	void IncreaseLevelCount(){
		currentLevel++;
	}

	Coroutine aISpawnCoroutine;
	IEnumerator StartLevelAfter(float sec = 0){	
		yield return new WaitForSeconds(sec);
		if(onLevelStart!=null) onLevelStart(currentLevel);
		yield return StartCoroutine(SpawnAIs());
	}

	IEnumerator SpawnPlayers(){
		// Plane plane = planePool.GetNextUnusedPooledObject();
		playerPlane.InitPlane(playerPlaneData, playerPlaneController);
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(aIPlaneSpawnManager.SpawnPlanesForLevel(aIPlaneController, levelData, OnEnemyKilled));
		}
	}
	public void OnEnemyKilled(){
		scoreManager.OnEnemyKilled();
		if(levelData.winCondition.ConditionToWin(scoreManager)){
			OnLevelComplete();
		}
	}

	void OnLevelComplete(){
		if(onLevelComplete!=null) onLevelComplete(currentLevel);
		Debug.Log ("Level WON");
		StopCoroutine(aISpawnCoroutine);
		StartNextLevel();
	}

	public System.Action<int> onLevelStart, onLevelComplete;
}
