using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
	public HUDView hUDView; 
	public void InitParam(GameManager gameManager){
		gameManager.levelManager.playerPlane.healthModel.healthController.onHealthChange+=OnHealthChange;
		gameManager.onGameFinished += OnGameFinished;
		gameManager.levelManager.onLevelStart += hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete += hUDView.OnLevelComplete;
	}	
	public void ResetParam(GameManager gameManager){
		gameManager.levelManager.playerPlane.healthModel.healthController.onHealthChange-=OnHealthChange;
		gameManager.onGameFinished -= OnGameFinished;
		gameManager.levelManager.onLevelStart -= hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete -= hUDView.OnLevelComplete;
	}
	
	public void OnGameFinished(bool gameWon){
		hUDView.OnGameFinished(gameWon);
	}

	void OnHealthChange(float currentHealth, float maxHealth){
		hUDView.SetHealthText(currentHealth+"/"+maxHealth);
	}

	
}
