using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public PlaneSpawnManager planeSpawnManager;
	
	public ScoreController scoreController;
	int currentLevel;
	Plane playerPlane;
	LevelData levelData;
	GameManager gameManager;
	
	public void InitParam(GameManager gameManager){
		this.gameManager = gameManager;
		gameManager.onGameFinished+=scoreController.OnGameFinished;
		StartCoroutine(SetUpGame());

	}
	IEnumerator SetUpGame(){
		currentLevel = 1;		

		playerPlaneController.InitControls(this);
		aIPlaneController.InitControls(this);
		scoreController.InitParam(this);

		yield return StartCoroutine(SpawnPlayers());
		SetUpLevel(currentLevel);
	}

	public void ResetParam(){
		scoreController.ResetParam(this);
		playerPlaneController.ResetControls(this);
		aIPlaneController.ResetControls(this);
		planeSpawnManager.DisableAllAIPlanes();
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
		playerPlane = planeSpawnManager.SpawnPlayerPlanes(levelsSOData.playerPlaneSOData,playerPlaneController,OnPlayerDead);
		if(onPlayerSet!=null) onPlayerSet(playerPlane);
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(planeSpawnManager.SpawnPlanesForLevel(aIPlaneController, levelData, OnEnemyKilled));
		}
	}

	void OnPlayerDead(Plane playerPlane){
		gameManager.OnGameFinished(false);
		ResetParam();
	}

	public void OnEnemyKilled(Plane aIPlane){
		scoreController.OnEnemyKilled((AIPlaneSOData)aIPlane.planeData);
		// if(levelData.winCondition.ConditionToWin(scoreManager)){
		// 	LevelComplete();
		// }
	}
	

	IEnumerator CheckWinCondition(){
		while(!levelData.winCondition.ConditionToWin(scoreController)) yield return new WaitForSeconds(scoreController.scoreSOData.scoreUpdateFrequency);
		LevelComplete();
		
	}

	void LevelComplete(){
		if(onLevelComplete!=null) onLevelComplete(currentLevel);
		// Debug.Log ("Level WON "+currentLevel);
		StopCoroutine(aISpawnCoroutine);
		StartNextLevel();
	}

	public System.Action<int> onLevelStart, onLevelComplete;
	public System.Action<Plane> onPlayerSet;
}
