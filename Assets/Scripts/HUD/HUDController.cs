using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
	public HUDView hUDView; 
	public void InitParam(GameManager gameManager){
		gameManager.onGameFinished += OnGameFinished;
		gameManager.levelManager.onPlayerSet += OnPlayerSet;
		gameManager.levelManager.onLevelStart += hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete += hUDView.OnLevelComplete;
	}	
	public void ResetParam(GameManager gameManager){
		gameManager.onGameFinished -= OnGameFinished;
		gameManager.levelManager.onPlayerSet -= OnPlayerSet;
		gameManager.levelManager.onLevelStart -= hUDView.OnLevelStart;
		gameManager.levelManager.onLevelComplete -= hUDView.OnLevelComplete;
	}

	void OnPlayerSet(Plane playerPlane){
		playerPlane.onDeath+=OnPlayerDeath;
		playerPlane.healthModel.healthController.onHealthChange+=OnHealthChange;
		OnHealthChange(playerPlane.healthModel.currentHealth,playerPlane.healthModel.maxHealth);
	}

	void OnPlayerDeath(Plane playerPlane){
		playerPlane.healthModel.healthController.onHealthChange-=OnHealthChange;
	}
	
	public void OnGameFinished(bool gameWon){
		hUDView.OnGameFinished(gameWon);
	}

	void OnHealthChange(float currentHealth, float maxHealth){
		hUDView.SetHealthText(currentHealth+"/"+maxHealth);
	}

	
}
