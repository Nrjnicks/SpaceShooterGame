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

	bool forceEvade;
	public override void InitControls(LevelManager levelManager){
		approachPlaneStrategy = new AIApproachPlaneStrategy();
		attackPlaneStrategy = new AIAttackPlaneStrategy();
		evadePlaneStrategy = new AIEvadePlaneStrategy();
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
	}

	public override void UpdateControls(Plane plane){
		if(ConditionToForceDie(plane)) plane.DisablePlane();
		//update strategy here
		UpdateStrategy(plane);
		currentStrategy.UpdateMoveDirection(plane.transform, playerPlaneT);
		base.UpdateControls(plane);
	}

	public virtual bool ConditionToForceDie(Plane plane){
		return (((AIPlaneSOData)plane.PlaneData).maxActiveTimeOnScreen<plane.ActiveTime ||forceEvade)
					&& WorldSpaceGameBoundary.Instance.IsPointOutsideCameraView(plane.transform.position);
	}

	void UpdateStrategy(Plane plane){
		
		if(evadePlaneStrategy.ConditionToSwitch(plane, playerPlaneT)||forceEvade){
			currentStrategy = evadePlaneStrategy;
		}
		else{			
			if(approachPlaneStrategy.ConditionToSwitch(plane, playerPlaneT))
				currentStrategy = approachPlaneStrategy;
			else
				currentStrategy = attackPlaneStrategy;
		}
	}
	
	public override Vector2 GetMoveDeltaPosition (Plane plane) {
		plane.transform.up = Vector2.Lerp(plane.transform.up, base.GetMoveDeltaPosition(plane), plane.PlaneData.Speed * Time.deltaTime);
		return plane.transform.up;
	}

	void OnLevelStart(int level){
		SetForceEvade(false);
	}
	void OnLevelComplete(int level){
		SetForceEvade(true);
	}

	void SetForceEvade(bool evade){
		forceEvade = evade;
	}

	

	float errorRange = 0.1f;
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
