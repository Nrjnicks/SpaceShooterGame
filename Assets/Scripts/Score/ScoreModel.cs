using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class ScoreModel : MonoBehaviour {
	[System.Serializable]
	public struct HighScoreInformation
	{
		public string name;
		public float score;
		public HighScoreInformation(string _name, float _score){
			name = _name;
			score = _score;
		}
	}

	[System.Serializable]
	public class HighScores{
		
		public List<HighScoreInformation> highScoresList;
		public HighScores(){
			highScoresList = new List<HighScoreInformation>();
		}
	}
	public int maxRankToSave = 5;
	HighScores highScores;
	SaveLoad saveLoad;
	void Start(){
		saveLoad = new SaveLoad();
	}
	// Use this for initialization
	public List<HighScoreInformation> GetHighScoreList() {
		highScores = saveLoad.LoadData<HighScores>();
		if(highScores == null){
			highScores = new HighScores();			
			SetInitialHighScores(highScores);
			SaveHighScoreList(highScores.highScoresList);
		}
		return highScores.highScoresList;
	}

	public void SaveHighScoreList(List<HighScoreInformation> highScoresList){
		this.highScores.highScoresList = highScoresList;
		saveLoad.SaveData<HighScores>(highScores);
	}
	
	void SetInitialHighScores(HighScores highScores){
		for (int i = maxRankToSave; i > 0; i--)
		{
			highScores.highScoresList.Add(new HighScoreInformation(i.ToString(),i*000));
		}
	}
}


