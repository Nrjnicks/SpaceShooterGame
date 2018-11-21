using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackPlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(AIPlane aIPlane){
		moveTowards = aIPlane.enemyPlane.transform.position - aIPlane.transform.position;
	}

	public override bool ShouldOrNotFire(AIPlane aIPlane){
		if(Vector2.Angle(aIPlane.enemyPlane.transform.position - aIPlane.transform.position, aIPlane.transform.up) < ((AIPlaneSOData)aIPlane.planeData).FOVToAttack)
			return true; 
		return false;
	}
	public override bool ConditionToSwitch(AIPlane aIPlane){
		if(!aIPlane.IsAttackInCoolDown) return true;
		return false;
	}
}
