using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public LevelManager levelManager;
	public HUDController hUDController;
	public AssetReferenceManager assetReferenceManager;
	// Use this for initialization
	void Start () {
	}

	public void StartGame(){
		assetReferenceManager.SetReferenceToElements();//Called after every game restart//todo
	}

	public void OnAssetLoad(){
		hUDController.InitParam(this);
		levelManager.gameObject.SetActive(true);
		levelManager.InitParam(this);

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
