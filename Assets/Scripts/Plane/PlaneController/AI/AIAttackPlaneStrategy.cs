using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackPlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(Transform aIPlaneT, Transform playerPlaneT){
		moveTowards = playerPlaneT.position - aIPlaneT.position;
	}

	public override bool ShouldOrNotFire(Plane aIPlane, Transform playerPlaneT){
		if(Vector2.Angle(playerPlaneT.position - aIPlane.transform.position, aIPlane.transform.up) < ((AIPlaneSOData)aIPlane.PlaneData).FOVToAttack)
			return true; 
		return false;
	}
	public override bool ConditionToSwitch(Plane aIPlaneT, Transform playerPlaneT){
		if(!aIPlaneT.IsAttackInCoolDown) return true;
		return false;
	}
}
