using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyKilledWinCondition", menuName = "Level/WinCondition/EnemyKilled")]
public class EnemyKilledWinCondition : AWinCondition {
	public int EnemyToKill;
	public override bool ConditionToWin(ScoreController scoreController){
		return scoreController.GetEnemiesKilledCount() >= EnemyToKill;
	}
}
