using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	int numOfPlayers = 1;
	public AssetReferenceManager assetReferenceManager;
	public LevelManager levelManager;
	public HUDController hUDController;
	// Use this for initialization
	void Start () {
		if(assetReferenceManager!=null)
		assetReferenceManager.SetReferenceToElements(this);//Set all references manually for more controls and less dependencies
	}

	///Called to Set Request of number of player and Start the game
	///<param name="numOfPlayers">number of players requested by the game to start level with</param>
	public void SetNumOfPlayerAndStartGame(int numOfPlayers){
		this.numOfPlayers = numOfPlayers;
		StartGame();
	}

	///Start Game
	public void StartGame(){
		hUDController.InitParam(this);
		levelManager.gameObject.SetActive(true);
		levelManager.InitParam(this);
	}
	
	///<returns>returns number of players requested by the game</returns>  
	public int GetNumberOfPlayers(){
		return numOfPlayers;
	}

	///Called when Game is finished
	///<param name="gameWon">won (true) | lost (false)</param>
	public void OnGameFinished(bool gameWon){
		if(onGameFinished!=null) onGameFinished(gameWon);
		levelManager.ResetParam();
		levelManager.UnsetParams();
		levelManager.gameObject.SetActive(false);//also reset
		hUDController.ResetParam(this);
	}

	///<description>event callback on Game Finished</description>
	public System.Action<bool> onGameFinished{get; set;}

}
