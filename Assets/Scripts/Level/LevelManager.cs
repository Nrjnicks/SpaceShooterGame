using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	

	public LevelsSOData levelsSOData;
	public PlayerPlaneController playerPlaneController;
	public AIPlaneController aIPlaneController;
	public PoolManager poolManager;
	public ScoreController scoreController;
	[Space]
	[SerializeField] SpriteRenderer BackgroundSprite;
	int currentLevel;
	int activePlayers;
	LevelData levelData;
	GameManager gameManager;
	
	public void InitParam(GameManager gameManager){
		this.gameManager = gameManager;
		gameManager.onGameFinished+=scoreController.OnGameFinished;
		StartCoroutine(SetUpGame(gameManager.GetNumberOfPlayers()));
	}
	IEnumerator SetUpGame(int numOfPlayers = 1){
		currentLevel = 1;		
		if(levelsSOData.multiplayerSOData.playerDataAndControls.Count<numOfPlayers){
			Debug.Log("Not enough multiplayer data set in LevelsDataSO. Switching to max available.");
			numOfPlayers = levelsSOData.multiplayerSOData.playerDataAndControls.Count;
		}

		playerPlaneController.InitControls(this);
		aIPlaneController.InitControls(this);
		poolManager.InitParam(this,numOfPlayers);
		scoreController.InitParam(this);

		yield return StartCoroutine(SpawnPlayers(numOfPlayers));
		SetUpLevel(currentLevel);
	}

	public void ResetParam(){
		scoreController.ResetParam(this);
		poolManager.ResetParam(this);
		playerPlaneController.ResetControls(this);
		aIPlaneController.ResetControls(this);
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

	IEnumerator SpawnPlayers(int numOfPlayers = 1){
		
		Plane playerPlane;
		for (int i = 1; i <= numOfPlayers; i++)
		{
			playerPlane = poolManager.planeSpawnManager.SpawnPlayerPlanes(levelsSOData.multiplayerSOData.playerDataAndControls[i-1]
																				,playerPlaneController,OnPlayerDead, i);
			if(onPlayerSet!=null) onPlayerSet(playerPlane,i);
		}
		activePlayers = numOfPlayers;
		yield break;
	}

	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(poolManager.planeSpawnManager.SpawnPlanesForLevel(aIPlaneController, levelData, OnEnemyKilled));
		}
	}

	void OnPlayerDead(Plane playerPlane){
		activePlayers--;
		if(activePlayers<1){
			gameManager.OnGameFinished(false);
		}
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

	public void SetBackgroundSpriteShader(Material mat, Sprite sprite = null){
		BackgroundSprite.material = mat;
		if(sprite == null)
			BackgroundSprite.sprite = Sprite.Create(
										(Texture2D)mat.mainTexture, 
										new Rect(0.0f, 0.0f, mat.mainTexture.width, mat.mainTexture.height), 
										new Vector2(0.5f, 0.5f));
		else
			BackgroundSprite.sprite = sprite;
	}

	public System.Action<int> onLevelStart, onLevelComplete;
	public System.Action<Plane,int> onPlayerSet;
}
