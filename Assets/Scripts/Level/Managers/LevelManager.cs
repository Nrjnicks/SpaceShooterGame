using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {//Manages one session of game (from player alive->dead)
	

	public LevelsSOData levelsSOData;
	public PlayerPlaneController playerPlaneController; //Controller to update all Players
	public AIPlaneController aIPlaneController; //Controller to update all AIs
	public PoolManager poolManager;
	public ScoreController scoreController;
	public SoundManager soundManager;
	[Space]
	[SerializeField] SpriteRenderer BackgroundSprite;

	int currentLevel;
	int activePlayers;
	LevelData currentLevelData;//Level Data for current Level
	GameManager gameManager;
	
	///<description>Initializing Level and Internal Parameters</description>
	///<param name="gameManager">GameManager instance</param>
	public void InitParam(GameManager gameManager){
		this.gameManager = gameManager;
		this.gameManager.onGameFinished+=scoreController.OnGameFinished;
		onPlayerKilled+=OnPlayerDead;
		StartCoroutine(SetUpGame(gameManager.GetNumberOfPlayers()));
	}

	
	///<description>Unset all parameter on Game End</description>
	public void UnsetParams(){		
		gameManager.onGameFinished-=scoreController.OnGameFinished;
		onPlayerKilled-=OnPlayerDead;
	}
	
	///<description>Set up game elements for this session</description>
	///<param name="numOfPlayers">number of players to start level with, requested by game</param>
	IEnumerator SetUpGame(int numOfPlayers = 1){
		currentLevel = 1;		
		if(levelsSOData.multiplayerSOData.playerDataAndControls.Count<numOfPlayers){ //if data information is not present, switch to max available
			Debug.Log("Not enough multiplayer data set in LevelsDataSO. Switching to max available.");
			numOfPlayers = levelsSOData.multiplayerSOData.playerDataAndControls.Count;
		}

		playerPlaneController.InitControls(this);
		aIPlaneController.InitControls(this);
		poolManager.InitParam(this,numOfPlayers);
		scoreController.InitParam(this);
		soundManager.InitParam(this);

		yield return StartCoroutine(SpawnPlayers(numOfPlayers));
		SetUpLevel(currentLevel);
	}

	///<description>Reset Parameters to restart level again</description>
	public void ResetParam(){
		scoreController.ResetParam(this);
		soundManager.ResetParam(this);
		poolManager.ResetParam(this);
		playerPlaneController.ResetControls(this);
		aIPlaneController.ResetControls(this);
	}
	
	///<description>start next level</description>
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
		return currentLevelData;
	}

	///<description>Set Level</description>
	///<param name="level">set this level</param>
	public void SetUpLevel(int level){		
		currentLevelData = levelsSOData.GetLevelData(level);
		StartCoroutine(StartLevelAfter(levelsSOData.timeDifferenceBetweenLevels));
	}

	void IncreaseLevelCount(){
		currentLevel++;
	}

	Coroutine aISpawnCoroutine;
	///<description>Set StartLevelAfter</description>
	///<param name="sec">Delay (rest period) to start next level</param>
	IEnumerator StartLevelAfter(float sec = 0){	
		yield return new WaitForSeconds(sec);
		if(onLevelStart!=null) onLevelStart(currentLevel);
		StartCoroutine(CheckWinCondition(scoreController.scoreSOData.scoreUpdateFrequency));//Start Check for win
		yield return StartCoroutine(SpawnAIs());
	}

	///<description>SpawnPlayers</description>
	///<param name="numOfPlayers">Number Of Players to spawn</param>
	IEnumerator SpawnPlayers(int numOfPlayers = 1){
		
		Plane playerPlane;
		for (int i = 1; i <= numOfPlayers; i++)
		{
			//Spawn players from pool and setting its data and controller
			playerPlane = poolManager.planeSpawnManager.SpawnPlayerPlanes(levelsSOData.multiplayerSOData.playerDataAndControls[i-1]
																				,playerPlaneController,onPlayerKilled, i);
			if(onPlayerSet!=null) onPlayerSet(playerPlane,i);
		}
		activePlayers = numOfPlayers;
		yield break;
	}

	///<description>SpawnAIs</description>
	IEnumerator SpawnAIs(){
		while(true){
			yield return aISpawnCoroutine = StartCoroutine(poolManager.planeSpawnManager.SpawnPlanesForLevel(aIPlaneController, currentLevelData, onEnemyKilled));
		}
	}

	///<description>Called when a player plane gets destroyed</description>
	void OnPlayerDead(Plane playerPlane){
		activePlayers--;
		if(activePlayers<1){
			gameManager.OnGameFinished(false);
		}
	}

	// public void OnEnemyKilled(Plane aIPlane){//used to use this event callback when game only have one tpye of win condition
	// 	scoreController.OnEnemyKilled(aIPlane);
	// 	// if(levelData.winCondition.ConditionToWin(scoreManager)){
	// 	// 	LevelComplete();
	// 	// }
	// }
	

	///<description>Start Checking for win condition</description>
	///<param name="deltaTime">refresh time</param>
	IEnumerator CheckWinCondition(float deltaTime){
		while(!currentLevelData.winCondition.ConditionToWin(scoreController)) yield return new WaitForSeconds(deltaTime);
		LevelComplete();		
	}
	
	///<description>OnLevel Complete</description>
	void LevelComplete(){
		if(onLevelComplete!=null) onLevelComplete(currentLevel);
		// Debug.Log ("Level WON "+currentLevel);
		StopCoroutine(aISpawnCoroutine);
		StartNextLevel();
	}
	
	///<description>Setting Background Sprite Material</description>
	public void SetBackgroundSpriteMaterial(Material mat, Sprite sprite = null){
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
	public System.Action<Plane> onPlayerKilled, onEnemyKilled;
}
