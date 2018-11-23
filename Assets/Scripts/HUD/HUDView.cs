using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {
	[SerializeField] Text P1HealthText;
	[SerializeField] Text P2HealthText;
	[SerializeField] Text P1ControlText;
	[SerializeField] Text P2ControlText;
	[SerializeField] Text levelText;
	[SerializeField] GameObject gameEndView;
	[SerializeField] GameObject gameWonObj;
	[SerializeField] GameObject gameLostObj;
	[SerializeField] GameObject levelStart;
	[Range(0,5)] float disableLevelStartObjAfterSec = 3;
	[SerializeField] GameObject levelComplete;

	///<description>Set Health Text</description>
	public void SetHealthText(string health, int playerNum = 1){
		if(playerNum == 1)
			P1HealthText.text = health;
		if(playerNum == 2)
			P2HealthText.text = health;
	}

	///<description>Activate or deactivate UI for Player 2</description>
	public void P2SetActiveUI(bool state){
		P2HealthText.gameObject.SetActive(state);
	}

	///<description>Set Text of controls for ease of player 1</description>
	public void SetP1ControlsText(string controls){
		P1ControlText.text = controls;
	}

	///<description>Set Text of controls for ease of player 2</description>
	public void SetP2ControlsText(string controls){
		P2ControlText.text = controls;
	}

	public void OnGameFinished(bool gameWon){
		DisableAll();
		gameEndView.SetActive(true);
		if(gameWon){
			levelText.text = "";
			gameWonObj.SetActive(true);
			gameLostObj.SetActive(false);
		}
		else{
			levelText.text = "";
			gameWonObj.SetActive(false);
			gameLostObj.SetActive(true);
		}
	}

	public void OnLevelStart(int level){
		levelText.text = "Level: "+level;
		levelComplete.SetActive(false);
		levelStart.SetActive(true);
		Invoke("DisableLevelStartObj",disableLevelStartObjAfterSec);
	}

	void DisableLevelStartObj(){		
		levelStart.SetActive(false);
	}
	public void OnLevelComplete(int level){
		levelText.text = "Completed: "+level;
		levelStart.SetActive(false);
		levelComplete.SetActive(true);
	}

	public void DisableAll(){
		levelStart.SetActive(false);
		levelComplete.SetActive(false);
	}
}
