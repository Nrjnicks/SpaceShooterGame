using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {
	[SerializeField] Text healthText;
	[SerializeField] GameObject gameEndView;
	[SerializeField] GameObject gameWonObj;
	[SerializeField] GameObject gameLostObj;
	[SerializeField] GameObject levelStart;
	[Range(0,5)] float disableLevelStartObjAfterSec = 3;
	[SerializeField] GameObject levelComplete;

	public void SetHealthText(string health){
		healthText.text = health;
	}

	public void OnGameStart(){
		
	}

	public void OnGameFinished(bool gameWon){
		DisableAll();
		gameEndView.SetActive(true);
		if(gameWon){
			gameWonObj.SetActive(true);
			gameLostObj.SetActive(false);
		}
		else{
			gameWonObj.SetActive(false);
			gameLostObj.SetActive(true);
		}
	}

	public void OnLevelStart(int level){
		levelComplete.SetActive(false);
		levelStart.SetActive(true);
		Invoke("DisableLevelStartObj",disableLevelStartObjAfterSec);
	}

	void DisableLevelStartObj(){		
		levelStart.SetActive(false);
	}
	public void OnLevelComplete(int level){
		levelStart.SetActive(false);
		levelComplete.SetActive(true);
	}

	public void DisableAll(){
		levelStart.SetActive(false);
		levelComplete.SetActive(false);
	}
}
