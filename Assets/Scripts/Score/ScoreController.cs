using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	public ScoreSOData scoreSOData;
	public ScoreModel scoreModel;
	public ScoreView scoreView;
	float score;
	public System.Action<float> onScoreChange;
	int enemiesKilled;
	float scoreTillLastLevel;
	Coroutine scoreCoroutine;
	float timeSinceLevelStart;
	bool restPeriod;

	Dictionary<AIPlaneSOData,int> enemiesKilledPerType;

	public void InitParam(LevelManager levelManager){
		restPeriod = true;
		AddToScore(-score);
		enemiesKilled = 0;
		enemiesKilledPerType = new Dictionary<AIPlaneSOData, int>();
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		scoreCoroutine = StartCoroutine(CalculateScore());
	}

	public void ResetParam(LevelManager levelManager){
		// AddToScore(-score);
		enemiesKilled = 0;
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		if(scoreCoroutine!=null) StopCoroutine(scoreCoroutine);
	}

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
	List<ScoreModel.HighScoreInformation> highScoresList;
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
	public void OnEnemyKilled(AIPlaneSOData aiplaneData){
		if(!restPeriod)
			enemiesKilled++;
		AddToScore(aiplaneData.scoreBonusOnKill);

		if(!enemiesKilledPerType.ContainsKey(aiplaneData)) enemiesKilledPerType[aiplaneData] = 0;
		enemiesKilledPerType[aiplaneData]++;

	}
	IEnumerator CalculateScore(){
		if(scoreSOData.scoreUpdateFrequency<=0) yield break;
		while(true){
			if(!restPeriod){
				AddToScore(scoreSOData.scorePerUpdateCycle);
			}
			yield return new WaitForSeconds(scoreSOData.scoreUpdateFrequency);
		}
	}
	
	public int GetEnemiesKilledCount(){
		return enemiesKilled;
	}

	public float GetLevelElapsedTime(){
		return Time.time - timeSinceLevelStart;
	}

	public float GetThisLevelScore(){
		return score - scoreTillLastLevel;
	}


}
