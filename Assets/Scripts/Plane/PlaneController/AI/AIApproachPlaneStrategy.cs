using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIApproachPlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(Transform aIPlaneT, Transform playerPlaneT){
		moveTowards = playerPlaneT.position - aIPlaneT.position;
	}
}
