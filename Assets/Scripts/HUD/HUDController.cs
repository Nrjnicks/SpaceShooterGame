using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	public HUDView hUDView; 


	///<description>Initializing Internal Parameters</description>
	///<param name="gameManager">GameManager instance</param>
	public void InitParam(GameManager gameManager){
		gameManager.onGameFinished += OnGameFinished;
		gameManager.levelManager.onPlayerSet += OnPlayerSet;
		gameManager.levelManager.onLevelStart += hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete += hUDView.OnLevelComplete;
		gameManager.levelManager.onPlayerKilled+=OnPlayerDeath;
		hUDView.P2SetActiveUI(gameManager.GetNumberOfPlayers()>1);
	}	
	
	///<description>Reseting Internal Parameters</description>
	///<param name="gameManager">GameManager instance</param>
	public void ResetParam(GameManager gameManager){
		gameManager.onGameFinished -= OnGameFinished;
		gameManager.levelManager.onPlayerSet -= OnPlayerSet;
		gameManager.levelManager.onLevelStart -= hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete -= hUDView.OnLevelComplete;
		gameManager.levelManager.onPlayerKilled+=OnPlayerDeath;
	}

	void OnPlayerSet(Plane playerPlane, int playerNum){
		if(playerNum == 1){
			playerPlane.healthModel.healthController.onHealthChange+=OnP1HealthChange;
			OnP1HealthChange(playerPlane.healthModel.currentHealth,playerPlane.healthModel.maxHealth);
		}
		if(playerNum == 2){
			playerPlane.healthModel.healthController.onHealthChange+=OnP2HealthChange;
			OnP2HealthChange(playerPlane.healthModel.currentHealth,playerPlane.healthModel.maxHealth);
		}
	}

	void OnPlayerDeath(Plane playerPlane){
		if(((PlayerPlane)playerPlane).playerNumber == 1){
			playerPlane.healthModel.healthController.onHealthChange-=OnP1HealthChange;
			hUDView.SetHealthText("DIED!", 1);
		}
		if(((PlayerPlane)playerPlane).playerNumber == 2){
			playerPlane.healthModel.healthController.onHealthChange-=OnP2HealthChange;
			hUDView.SetHealthText("DIED!", 2);
		}
	}
	
	void OnGameFinished(bool gameWon){
		hUDView.OnGameFinished(gameWon);
	}

	void OnP1HealthChange(float currentHealth, float maxHealth){ //Player 1
		hUDView.SetHealthText(currentHealth+"/"+maxHealth, 1);
	}

	void OnP2HealthChange(float currentHealth, float maxHealth){ //Player 2
		hUDView.SetHealthText(currentHealth+"/"+maxHealth, 2);
	}

	///<description>Set Font of all text for any game theme</description>
	public void SetFontForAllText(Font font){
		Text[] allTexts = gameObject.GetComponentsInChildren<Text>(true);
		foreach (Text text in allTexts)
		{
			text.font = font;
		}
	}

	
}
