using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEvadePlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(AIPlane aIPlane){
		moveTowards = aIPlane.transform.position - aIPlane.enemyPlane.transform.position;
	}
	public override bool ConditionToSwitch(AIPlane aIPlane){
		if(aIPlane.IsAttackInCoolDown || ((AIPlaneSOData)aIPlane.PlaneData).maxActiveTimeOnScreen<aIPlane.ActiveTime) return true;
		return false;
	}
}
