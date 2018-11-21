using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIApproachPlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(AIPlane aIPlane){
		moveTowards = aIPlane.enemyPlane.transform.position - aIPlane.transform.position;
	}
	public override bool ConditionToSwitch(AIPlane aIPlane){
		if(Vector2.Distance(aIPlane.transform.position,aIPlane.enemyPlane.transform.position) > ((AIPlaneSOData)aIPlane.planeData).minDistanceToAttack) return true;
		return false;
	}
}
