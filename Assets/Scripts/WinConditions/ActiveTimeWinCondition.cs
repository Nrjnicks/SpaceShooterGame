using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveTimeWinCondition", menuName = "Level/WinCondition/ActiveTime")]
public class ActiveTimeWinCondition : AWinCondition {
	public float minTimeToLevelComplete = 20;
	public override bool ConditionToWin(ScoreController scoreController){
		return scoreController.GetLevelElapsedTime() >= minTimeToLevelComplete;
	}
}
