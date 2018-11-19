using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public ScoreManager scoreManager;

	public LevelManager levelManager;
	// Use this for initialization
	void Start () {
		StartGame();
	}

	void StartGame(){
		levelManager.InitParam(scoreManager);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
