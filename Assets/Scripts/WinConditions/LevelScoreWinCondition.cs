using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScoreWinCondition", menuName = "Level/WinCondition/LevelScore")]
public class LevelScoreWinCondition : AWinCondition {
	[Tooltip("Minimum Score (without level bonus) to complete current level")] 
	public float minScoreToLevelComplete = 1000;
	public override bool ConditionToWin(ScoreController scoreManager){
		return scoreManager.GetThisLevelScore() >= minScoreToLevelComplete;
	}
}
