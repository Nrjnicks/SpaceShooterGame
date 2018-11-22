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
	

	///<description>Set High Score UI For List</description>
	public void SetHighScoreUIForList(List<ScoreModel.HighScoreInformation> highScoresList){
		int rank;
		for (rank = 1; rank <= highScoresList.Count; rank++)
		{
			SetHighScoreUIForRank(rank,highScoresList[rank-1].name, highScoresList[rank-1].score);
		}
		for (; rank <= highScoresUIInRank.Length; rank++)
		{
			highScoresUIInRank[rank-1].nameText.transform.parent.gameObject.SetActive(false);
		}
	}

	///<description>Activate 'enter name' dialog box on high score defeat</description>
	public void OnHighScoreBeat(bool isBeat){
		highScoreNameInputField.SetActive(isBeat);
	}

	///<description>Update High SCore information for particular rank</description>
	public void SetHighScoreUIForRank(int rank, string name, float score){
		if(rank<1 || rank>highScoresUIInRank.Length) return;
		highScoresUIInRank[rank-1].nameText.transform.parent.gameObject.SetActive(true);
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
