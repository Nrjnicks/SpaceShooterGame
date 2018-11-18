using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public ScoreManager scoreManager;

	public LevelManager levelManager;
	// Use this for initialization
	void Start () {
		levelManager.InitParam(scoreManager);
		StartGame();
	}

	void StartGame(){
		levelManager.SetUpLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
