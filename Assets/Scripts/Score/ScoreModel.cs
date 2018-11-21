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
	[Range(0,5)] public int maxRankToSave = 5;
	HighScores highScores;
	SaveLoad saveLoad;
	
	public List<HighScoreInformation> GetHighScoreList() {
		if(highScores==null)
			UpdateHighScores();
		return highScores.highScoresList;
	}

	void UpdateHighScores(){		
		if(saveLoad == null) 
			saveLoad = new SaveLoad();
		highScores = saveLoad.LoadData<HighScores>();
		if(highScores == null){
			highScores = new HighScores();			
			SetInitialHighScores(highScores);
			SaveHighScoreList(highScores.highScoresList);
		}
	}

	public void SaveHighScoreList(List<HighScoreInformation> highScoresList){
		if(saveLoad == null) 
			saveLoad = new SaveLoad();
			
		if(highScores==null)
			highScores = new HighScores();
		this.highScores.highScoresList = highScoresList;
		saveLoad.SaveData<HighScores>(highScores);
	}
	
	void SetInitialHighScores(HighScores highScores){
		string[] randomNames = {"Is this the real life?", "Is this just fantasy?","Caught in a landslide","no escape from reality", "Please Hire MEE!!!"};
		for (int i = 0; i < maxRankToSave; i++)
		{
			highScores.highScoresList.Add(new HighScoreInformation(randomNames[i],(maxRankToSave-i)*1000));
		}
	}
}


