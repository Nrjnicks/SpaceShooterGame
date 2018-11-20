using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
	[System.Serializable]
	public struct HighScoreUI{
		public Text nameText;
		public Text scoreText;
	}
	[SerializeField] Text scoreText;
	[SerializeField] GameObject highScoreNameInputField;

	[SerializeField] HighScoreUI[] highScoresUIInRank;
	[SerializeField] Text levelCompleteScoreDiscriptionText;
	

	public void SetHighScoreUIForList(List<ScoreModel.HighScoreInformation> highScoresList){
		for (int rank = 1; rank <= highScoresList.Count; rank++)
		{
			SetHighScoreUIForRank(rank,highScoresList[rank-1].name, highScoresList[rank-1].score);
			
		}
	}

	public void IsHighScoreBeat(bool isBeat){
		highScoreNameInputField.SetActive(isBeat);
	}

	public void SetHighScoreUIForRank(int rank, string name, float score){
		if(rank<1 || rank>highScoresUIInRank.Length) return;
		highScoresUIInRank[rank-1].nameText.text = name;
		highScoresUIInRank[rank-1].scoreText.text = score.ToString();
	}
	
	public void OnLevelComplete(string discription){
		levelCompleteScoreDiscriptionText.text = discription;
	}
	public void SetScoreText(string score){
		scoreText.text = score;
	}
}
