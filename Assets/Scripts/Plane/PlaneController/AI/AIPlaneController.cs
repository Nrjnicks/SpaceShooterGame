using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaneController : APlaneContoller {
	Plane playerPlane;
	AIPlane currentAIPlane;

	//Strategies---------->
	AIApproachPlaneStrategy approachPlaneStrategy;
	AIAttackPlaneStrategy attackPlaneStrategy;
	AIEvadePlaneStrategy evadePlaneStrategy;
	//<---------------------

	AIPlaneStrategy currentStrategy;

	bool forceEvade;

	void Start(){		
		approachPlaneStrategy = new AIApproachPlaneStrategy();
		attackPlaneStrategy = new AIAttackPlaneStrategy();
		evadePlaneStrategy = new AIEvadePlaneStrategy();
	}
	public override void InitControls(LevelManager levelManager){
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		levelManager.onPlayerSet += SetPlayerPlane;
	}

	public Plane GetEnemyPlane(){
		return playerPlane;
	}

	void SetPlayerPlane(Plane playerPlane){
		this.playerPlane = playerPlane;
	}

	public override void ResetControls(LevelManager levelManager){
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		levelManager.onPlayerSet -= SetPlayerPlane;
	}

	public override void UpdateControls(Plane plane){
		currentAIPlane = (AIPlane)plane;
		if(ConditionToForceDie(currentAIPlane)) plane.DisablePlane();
		//update strategy here
		UpdateStrategy(currentAIPlane);
		currentStrategy.UpdateMoveDirection(currentAIPlane);
		base.UpdateControls(plane);
	}

	public virtual bool ConditionToForceDie(AIPlane plane){
		return (((AIPlaneSOData)plane.planeData).maxActiveTimeOnScreen< plane.ActiveTime ||forceEvade)
					&& WorldSpaceGameBoundary.Instance.IsPointOutsideCameraView(plane.transform.position);
	}

	void UpdateStrategy(AIPlane aiPlane){
		
		if(evadePlaneStrategy.ConditionToSwitch(aiPlane)||forceEvade){
			currentStrategy = evadePlaneStrategy;
		}
		else{			
			if(approachPlaneStrategy.ConditionToSwitch(aiPlane))
				currentStrategy = approachPlaneStrategy;
			else
				currentStrategy = attackPlaneStrategy;
		}
	}
	
	public override Vector2 GetMoveDeltaPosition (Plane plane) {
		plane.transform.up = Vector2.Lerp(plane.transform.up, base.GetMoveDeltaPosition(plane), plane.planeData.Speed * Time.deltaTime);
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
		if(currentStrategy.ShouldOrNotFire((AIPlane)plane))
			return true;
		return false;
	}

	protected override bool IsPossibleToMoveTo(Vector2 newPos){
		return WorldSpaceGameBoundary.Instance.IsPointInsideExtraSpace(newPos);
	}
}
