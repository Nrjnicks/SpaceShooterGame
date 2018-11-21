using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public PlaneSpawnManager planeSpawnManager;
	[HideInInspector] public Plane playerPlane;
	
	public ScoreController scoreController;
	int currentLevel;
	LevelData levelData;
	GameManager gameManager;
	
	public void InitParam(GameManager gameManager){
		this.gameManager = gameManager;
		gameManager.onGameFinished+=scoreController.OnGameFinished;
		StartCoroutine(SetUpGame());

	}
	IEnumerator SetUpGame(){
		currentLevel = 1;		
		scoreController.InitParam(this);

		playerPlaneController.InitControls(this);
		yield return StartCoroutine(SpawnPlayers());
		aIPlaneController.InitControls(this);
		SetUpLevel(currentLevel);
	}

	public void ResetParam(){
		scoreController.ResetParam(this);
		playerPlaneController.ResetControls(this);
		aIPlaneController.ResetControls(this);
		planeSpawnManager.DisableAllPlanes();
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
		planeSpawnManager.SpawnPlayerPlanes(ref playerPlane,levelsSOData.playerPlaneSOData,playerPlaneController,OnPlayerDead);
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(planeSpawnManager.SpawnPlanesForLevel(aIPlaneController, levelData, OnEnemyKilled));
		}
	}

	void OnPlayerDead(PlaneSOData planeData){
		gameManager.OnGameFinished(false);
		ResetParam();
	}

	public void OnEnemyKilled(PlaneSOData planeData){
		scoreController.OnEnemyKilled((AIPlaneSOData)planeData);
		// if(levelData.winCondition.ConditionToWin(scoreManager)){
		// 	LevelComplete();
		// }
	}
	

	IEnumerator CheckWinCondition(){
		while(!levelData.winCondition.ConditionToWin(scoreController)) yield return new WaitForSeconds(levelsSOData.checkWinConditionFrequency);
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
