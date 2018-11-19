using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public float score;
	public System.Action<float> onScoreChange;
	Plane playerPlane;
	int enemiesKilled;

	public void InitParam(LevelManager levelManager){
		score = 0;
		enemiesKilled = 0;
		this.playerPlane = levelManager.playerPlane;
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		StartCoroutine(CalculateScore());
	}

	public int GetEnemiesKilledCount(){
		return enemiesKilled;
	}

	public float GetPlayerActiveTime(){
		return playerPlane.ActiveTime;
	}

	public void OnEnemyKilled(){
		enemiesKilled++;
		score+=50;
		if(onScoreChange!=null) onScoreChange(score);
	}

	void OnLevelStart(int level){
		enemiesKilled=0;
	}
	void OnLevelComplete(int level){
		score+=1000;
	}

	IEnumerator CalculateScore(){
		while(true){
			score+=10;
			if(onScoreChange!=null) onScoreChange(score);
			yield return new WaitForSeconds(0.5f);
		}
	}


}
