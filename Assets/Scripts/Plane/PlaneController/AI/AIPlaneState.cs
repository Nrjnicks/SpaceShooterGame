using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlaneState {
	protected Vector2 moveTowards;
	///<description>Update Direction to move towards</description>
	public virtual void UpdateMoveDirection(AIPlane aIPlaneT){
		moveTowards = Vector2.zero;
	}

	public virtual Vector2 GetNormalizedMoveDirection(){
		return moveTowards.normalized;
	}
	///<description>Condition to fire</description>
	public virtual bool ShouldOrNotFire(AIPlane aIPlaneT){
		return false;
	}
	
	///<description>Condition to switch to current State</description>
	public virtual bool ConditionToSwitch(AIPlane aIPlaneT){
		return false;
	}
}
