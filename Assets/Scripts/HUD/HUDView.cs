using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {
	[SerializeField] Text P1healthText;
	[SerializeField] Text P2healthText;
	[SerializeField] GameObject gameEndView;
	[SerializeField] GameObject gameWonObj;
	[SerializeField] GameObject gameLostObj;
	[SerializeField] GameObject levelStart;
	[Range(0,5)] float disableLevelStartObjAfterSec = 3;
	[SerializeField] GameObject levelComplete;

	///<description>Set Health Text</description>
	public void SetHealthText(string health, int playerNum = 1){
		if(playerNum == 1)
			P1healthText.text = health;
		if(playerNum == 2)
			P2healthText.text = health;
	}

	///<description>Activate or deactivate UI for Player 2</description>
	public void P2SetActiveUI(bool state){
		P2healthText.gameObject.SetActive(state);
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
