using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaneStrategy {
	protected Vector2 moveTowards;
	public virtual void UpdateMoveDirection(Transform aIPlaneT, Transform playerPlaneT){
		moveTowards = Vector2.zero;
	}

	public virtual Vector2 GetNormalizedMoveDirection(){
		return moveTowards.normalized;
	}
	public virtual bool ShouldOrNotFire(Plane aIPlaneT, Transform playerPlaneT){
		return false;
	}
}
