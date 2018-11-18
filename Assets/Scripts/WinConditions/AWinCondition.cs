using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWinCondition: ScriptableObject {

	public abstract bool ConditionToWin(ScoreManager scoreManager);
}
