using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlaneStrategy {
	protected Vector2 moveTowards;
	public virtual void UpdateMoveDirection(AIPlane aIPlaneT){
		moveTowards = Vector2.zero;
	}

	public virtual Vector2 GetNormalizedMoveDirection(){
		return moveTowards.normalized;
	}
	public virtual bool ShouldOrNotFire(AIPlane aIPlaneT){
		return false;
	}

	public virtual bool ConditionToSwitch(AIPlane aIPlaneT){
		return false;
	}
}
