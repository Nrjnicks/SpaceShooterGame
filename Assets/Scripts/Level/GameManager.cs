using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	int numOfPlayers = 1;
	public LevelManager levelManager;
	public HUDController hUDController;
	public AssetReferenceManager assetReferenceManager;
	// Use this for initialization
	void Start () {
		assetReferenceManager.SetReferenceToElements();//Called after every game restart//todo
	}

	public void SetNumOfPlayerAndStartGame(int numOfPlayers){
		this.numOfPlayers = numOfPlayers;
		StartGame();
	}

	public void StartGame(){
		hUDController.InitParam(this);
		levelManager.gameObject.SetActive(true);
		levelManager.InitParam(this);
	}

	public int GetNumberOfPlayers(){
		return numOfPlayers;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnGameFinished(bool gameWon){
		if(onGameFinished!=null) onGameFinished(gameWon);
		levelManager.ResetParam();
		levelManager.gameObject.SetActive(false);//also reset
		hUDController.ResetParam(this);
	}

	public System.Action<bool> onGameFinished{get; set;}

}
