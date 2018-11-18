using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEvadePlaneStrategy : AIPlaneStrategy {
	
	public override void UpdateMoveDirection(Transform aIPlaneT, Transform playerPlaneT){
		moveTowards = aIPlaneT.position - playerPlaneT.position;
	}
}
