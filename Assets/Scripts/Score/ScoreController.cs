﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	public ScoreSOData scoreSOData;
	public ScoreModel scoreModel;
	public ScoreView scoreView;
	
	float score;
	int enemiesKilled;
	float scoreTillLastLevel;
	Coroutine scoreCoroutine;
	float timeSinceLevelStart;
	bool restPeriod;

	Dictionary<AIPlaneSOData,int> enemiesKilledPerType;

	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	public void InitParam(LevelManager levelManager){
		restPeriod = true;
		AddToScore(-score);
		enemiesKilled = 0;
		enemiesKilledPerType = new Dictionary<AIPlaneSOData, int>();
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		levelManager.onEnemyKilled += OnEnemyKilled;
		scoreCoroutine = StartCoroutine(CalculateScore());
	}

	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public void ResetParam(LevelManager levelManager){
		// AddToScore(-score);
		enemiesKilled = 0;
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		levelManager.onEnemyKilled -= OnEnemyKilled;
		if(scoreCoroutine!=null) StopCoroutine(scoreCoroutine);
	}

	
	///<description>Set Final score on game finish</description>
	public void OnGameFinished(bool gameWon){
		EvaluateScore();
	}

	int scoreRank;
	public int GetCurrentScoreRank(){
		return scoreRank;
	}
	public float GetCurrentScore(){
		return score;
	}
	List<ScoreModel.HighScoreInformation> highScoresList; //list of high score from score model
	
	///<description>Set Final score on game finish and update high score list (in View)</description>
	void EvaluateScore(){

		ScoreModel.HighScoreInformation currentScoreInfo = new ScoreModel.HighScoreInformation("Enter Name To Save Score...",score);

		highScoresList = scoreModel.GetHighScoreList();
		int rank = 1;
		scoreRank = highScoresList.Count+1;
		scoreView.OnHighScoreBeat(false);
		for (; rank <= highScoresList.Count; rank++)
		{
			if(highScoresList[rank-1].score<score){
				scoreRank = rank;
				break;
			}			
		}		
		if(scoreRank <= highScoresList.Count){
			rank = highScoresList.Count;
			for (; rank > scoreRank; rank--)
			{
				highScoresList[rank-1] = highScoresList[rank-2];
			}
			highScoresList[scoreRank-1] = currentScoreInfo;
			scoreView.OnHighScoreBeat(true);
		}
		scoreView.SetHighScoreUIForList(highScoresList);
	}

	///<description>Called by UI to save name of player with high score</description>
	public void UpdateHighScoreHolderName(string name){
		if(scoreRank<1 || scoreRank>highScoresList.Count) return;
		
		scoreView.SetHighScoreUIForRank(scoreRank, name, score);
		highScoresList[scoreRank-1] = new ScoreModel.HighScoreInformation(name,score);
		scoreModel.SaveHighScoreList(highScoresList);
	}

	void OnLevelStart(int level){
		restPeriod = false;
		timeSinceLevelStart = Time.time;
		enemiesKilledPerType.Clear();
		enemiesKilled=0;
	}
	void OnLevelComplete(int level){
		restPeriod = true;
		enemiesKilled=0;
		AddToScore(scoreSOData.scorePerLevelComplete);
		scoreTillLastLevel = score;
		timeSinceLevelStart = Time.time;
		SetLevelCompleteDescription(level);
	}

	///<description>Setting level description- enemies killed (by type) and level bonus</description>
	void SetLevelCompleteDescription(int level){		
		string levelDiscription = "Level "+level+" Complete";
		levelDiscription += "\n\nEnemy type\t\t\t|\tPoint Gain\tx\tKill count";
		foreach (AIPlaneSOData planeSOData in enemiesKilledPerType.Keys)
		{
			levelDiscription+= "\n"+planeSOData.planeName+"\t\t|\t"+ planeSOData.scoreBonusOnKill+"\t\t\t\tx\t"+enemiesKilledPerType[planeSOData];			
		}
		levelDiscription += "\n\nLevel Complete Bonus: +"+ scoreSOData.scorePerLevelComplete;

		scoreView.OnLevelComplete(levelDiscription);
	}

	void AddToScore(float deltaScore){
		score+=deltaScore;
		if(onScoreChange!=null) onScoreChange(score);
		scoreView.SetScoreText(score.ToString());

	}

	//Win Conditions with Current Score
	
	///<description>OnEnemyKilled called from event callback of LevelManager</description>
	public void OnEnemyKilled(Plane plane){
		AIPlaneSOData aiplaneData = (AIPlaneSOData)plane.planeData;
		if(!restPeriod)
			enemiesKilled++;
		AddToScore(aiplaneData.scoreBonusOnKill);

		if(!enemiesKilledPerType.ContainsKey(aiplaneData)) enemiesKilledPerType[aiplaneData] = 0;
		enemiesKilledPerType[aiplaneData]++;

	}

	
	///<description>UpdateScore</description>
	IEnumerator CalculateScore(){
		if(scoreSOData.scoreUpdateFrequency<=0) yield break;
		while(true){
			if(!restPeriod){
				AddToScore(scoreSOData.scorePerUpdateCycle);
			}
			yield return new WaitForSeconds(scoreSOData.scoreUpdateFrequency);
		}
	}
	

	//Used to check WinConditions
	public int GetEnemiesKilledCount(){
		return enemiesKilled;
	}

	public float GetLevelElapsedTime(){
		return Time.time - timeSinceLevelStart;
	}

	public float GetThisLevelScore(){
		return score - scoreTillLastLevel;
	}


	public System.Action<float> onScoreChange;//subscribe to this for any score change callbacks
}
