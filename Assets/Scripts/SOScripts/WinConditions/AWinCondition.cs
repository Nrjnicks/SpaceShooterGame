using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWinCondition: ScriptableObject {
	///<description>Returns True if Condition to Win is Satisfied</description>
	public abstract bool ConditionToWin(ScoreController scoreController);
}
