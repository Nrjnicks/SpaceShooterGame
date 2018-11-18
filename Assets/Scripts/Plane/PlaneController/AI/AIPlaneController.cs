using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaneController : APlaneContoller {
	public Transform playerPlaneT;

	//Strategies---------->
	AIApproachPlaneStrategy approachPlaneStrategy;
	AIAttackPlaneStrategy attackPlaneStrategy;
	AIEvadePlaneStrategy evadePlaneStrategy;
	//<---------------------

	AIPlaneStrategy currentStrategy;

	float errorRange = 0.1f;

	public override void InitControls(){
		approachPlaneStrategy = new AIApproachPlaneStrategy();
		attackPlaneStrategy = new AIAttackPlaneStrategy();
		evadePlaneStrategy = new AIEvadePlaneStrategy();
	}

	public override void UpdateControls(Plane plane){
		if(ConditionToForceDie(plane)) plane.DisablePlane();
		//update strategy here
		UpdateStrategy(plane);
		currentStrategy.UpdateMoveDirection(plane.transform, playerPlaneT);
		base.UpdateControls(plane);
	}

	public virtual bool ConditionToForceDie(Plane plane){
		return ((AIPlaneSOData)plane.PlaneData).maxActiveTimeOnScreen<plane.ActiveTime 
					&& WorldSpaceGameBoundary.Instance.IsPointOutsideCameraView(plane.transform.position);
	}

	void UpdateStrategy(Plane plane){
		
		if(plane.IsAttackInCoolDown || ((AIPlaneSOData)plane.PlaneData).maxActiveTimeOnScreen<plane.ActiveTime){
			currentStrategy = evadePlaneStrategy;
		}
		else{			
			if(Vector2.Distance(plane.transform.position,playerPlaneT.transform.position) > ((AIPlaneSOData)plane.PlaneData).minDistanceToAttack)
				currentStrategy = approachPlaneStrategy;
			else
				currentStrategy = attackPlaneStrategy;
		}
	}
	
	public override Vector2 GetMoveDeltaPosition (Plane plane) {
		plane.transform.up = Vector2.Lerp(plane.transform.up, base.GetMoveDeltaPosition(plane), plane.PlaneData.Speed * Time.deltaTime);
		return plane.transform.up;
	}

	

	protected override bool ShouldOrNotMoveForward(){
		if(currentStrategy.GetNormalizedMoveDirection().y > +errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveBackward(){
		if(currentStrategy.GetNormalizedMoveDirection().y < -errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveLeft(){
		if(currentStrategy.GetNormalizedMoveDirection().x < -errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveRight(){
		if(currentStrategy.GetNormalizedMoveDirection().x > +errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotFire(Plane plane){
		if(currentStrategy.ShouldOrNotFire(plane, playerPlaneT))
			return true;
		return false;
	}

	protected override bool IsPossibleToMoveTo(Vector2 newPos){
		return WorldSpaceGameBoundary.Instance.IsPointInsideExtraSpace(newPos);
	}
}
