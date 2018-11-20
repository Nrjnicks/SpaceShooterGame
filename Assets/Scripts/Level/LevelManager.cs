using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public AIPlaneSpawnManager aIPlaneSpawnManager;
	public Plane playerPlane;
	public PlaneSOData playerPlaneData;
	
	public ScoreController scoreManager;
	int currentLevel;
	LevelData levelData;
	GameManager gameManager;
	
	public void InitParam(GameManager gameManager){
		this.gameManager = gameManager;
		currentLevel = 1;		
		scoreManager.InitParam(this);
		gameManager.onGameFinished+=scoreManager.OnGameFinished;
		playerPlaneController.InitControls(this);
		aIPlaneController.InitControls(this);

		StartCoroutine(SpawnPlayers());
		SetUpLevel(currentLevel);
	}

	public void ResetParam(){
		scoreManager.ResetParam(this);
		playerPlaneController.ResetControls(this);
		aIPlaneController.ResetControls(this);
		aIPlaneSpawnManager.DisableAllPlanes();
	}

	void StartNextLevel(){		
		IncreaseLevelCount();
		if(currentLevel>=levelsSOData.totalNumOfLevels){
			Debug.Log("Game Done. Congrats!");
			gameManager.OnGameFinished(true);
			ResetParam();
			return;
		}
		SetUpLevel(currentLevel);
	}

	public LevelData GetCurrentLevelData(){
		return levelData;
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
		StartCoroutine(CheckWinCondition());
		yield return StartCoroutine(SpawnAIs());
	}

	IEnumerator SpawnPlayers(){
		// Plane plane = planePool.GetNextUnusedPooledObject();
		playerPlane.InitPlane(playerPlaneData, playerPlaneController);
		playerPlane.onDeath+=OnPlayerDead;
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(aIPlaneSpawnManager.SpawnPlanesForLevel(aIPlaneController, levelData, OnEnemyKilled));
		}
	}

	void OnPlayerDead(PlaneSOData planeData){
		gameManager.OnGameFinished(false);
		ResetParam();
	}

	public void OnEnemyKilled(PlaneSOData planeData){
		scoreManager.OnEnemyKilled((AIPlaneSOData)planeData);
		// if(levelData.winCondition.ConditionToWin(scoreManager)){
		// 	LevelComplete();
		// }
	}
	

	IEnumerator CheckWinCondition(){
		while(!levelData.winCondition.ConditionToWin(scoreManager)) yield return new WaitForSeconds(levelsSOData.checkWinConditionFrequency);
		LevelComplete();
		
	}

	void LevelComplete(){
		if(onLevelComplete!=null) onLevelComplete(currentLevel);
		// Debug.Log ("Level WON "+currentLevel);
		StopCoroutine(aISpawnCoroutine);
		StartNextLevel();
	}

	public System.Action<int> onLevelStart, onLevelComplete;
}
