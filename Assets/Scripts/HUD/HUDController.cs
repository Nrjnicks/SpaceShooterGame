using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
	public HUDView hUDView;
	public void InitParam(ScoreManager scoreManager, Plane playerPlane){
		scoreManager.onScoreChange += OnScoreChange;
		playerPlane.healthModel.healthController.onHealthChange+=OnHealthChange;
	}

	void OnHealthChange(float currentHealth, float maxHealth){
		hUDView.SetHealthText(currentHealth+"/"+maxHealth);
	}

	void OnScoreChange(float score){
		hUDView.SetScoreText(score.ToString());
	}
}
