using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEvadePlaneState : AIPlaneState {
	
	public override void UpdateMoveDirection(AIPlane aIPlane){
		moveTowards = aIPlane.transform.position - aIPlane.enemyPlane.transform.position;
	}
	public override bool ConditionToSwitch(AIPlane aIPlane){
		if(aIPlane.IsAttackInCoolDown || ((AIPlaneSOData)aIPlane.planeData).maxActiveTimeOnScreen<aIPlane.ActiveTime) return true;
		return false;
	}
}
