using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {
	[SerializeField] Text healthText;
	[SerializeField] Text scoreText;

	public void SetHealthText(string health){
		healthText.text = health;
	}
	public void SetScoreText(string score){
		scoreText.text = score;
	}

}
