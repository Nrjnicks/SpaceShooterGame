using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIApproachPlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(Transform aIPlaneT, Transform playerPlaneT){
		moveTowards = playerPlaneT.position - aIPlaneT.position;
	}
	public override bool ConditionToSwitch(Plane aIPlaneT, Transform playerPlaneT){
		if(Vector2.Distance(aIPlaneT.transform.position,playerPlaneT.transform.position) > ((AIPlaneSOData)aIPlaneT.PlaneData).minDistanceToAttack) return true;
		return false;
	}
}
