using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlaneController : APlaneContoller {
	List<Plane> playerPlanes;
	AIPlane currentAIPlane;

	//States---------->
	AIApproachPlaneState approachPlaneState;
	AIAttackPlaneState attackPlaneState;
	AIEvadePlaneState evadePlaneState;
	//<---------------------

	AIPlaneState currentState;

	bool forceEvade;

	void Start(){		
		approachPlaneState = new AIApproachPlaneState();
		attackPlaneState = new AIAttackPlaneState();
		evadePlaneState = new AIEvadePlaneState();
	}
	
	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	public override void InitControls(LevelManager levelManager){
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		levelManager.onPlayerSet += SetPlayerPlane;
		playerPlanes = new List<Plane>();
	}

	///<description>Get Random Enemy plane to aim</description>
	public Plane GetRandomEnemyPlane(){
		return playerPlanes[UnityEngine.Random.Range(0,playerPlanes.Count)];
	}

	void SetPlayerPlane(Plane playerPlane, int playerNum){
		this.playerPlanes.Add(playerPlane);//order doesn't matter
	}

	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public override void ResetControls(LevelManager levelManager){
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		levelManager.onPlayerSet -= SetPlayerPlane;
	}

	///<description>Updating Controls and States of the AI</description>
	public override void UpdateControls(Plane plane){
		currentAIPlane = (AIPlane)plane;
		if(ConditionToForceDie(currentAIPlane)) plane.DisablePlane();
		//update State here
		UpdateState(currentAIPlane);
		currentState.UpdateMoveDirection(currentAIPlane);
		base.UpdateControls(plane);
	}

	public virtual bool ConditionToForceDie(AIPlane plane){
		return (((AIPlaneSOData)plane.planeData).maxActiveTimeOnScreen< plane.ActiveTime ||forceEvade)
					&& WorldSpaceGameBoundary.Instance.IsPointOutsideCameraView(plane.transform.position);
	}

	///<description>Update State Machine</description>
	void UpdateState(AIPlane aiPlane){
		//In sentence: Attack, if in range and not in cooldown. Evade, if in cooldown. Approach, if not in cooldown and far away
		if(evadePlaneState.ConditionToSwitch(aiPlane)||forceEvade){
			currentState = evadePlaneState;
		}
		else{			
			if(approachPlaneState.ConditionToSwitch(aiPlane))
				currentState = approachPlaneState;
			else
				currentState = attackPlaneState;
		}
	}
	
	///<description>Get Direction to move to, for this plane</description>
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
		if(currentState.GetNormalizedMoveDirection().y > +errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveBackward(){
		if(currentState.GetNormalizedMoveDirection().y < -errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveLeft(){
		if(currentState.GetNormalizedMoveDirection().x < -errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveRight(){
		if(currentState.GetNormalizedMoveDirection().x > +errorRange)
			return true;
		return false;
	}
	protected override bool ShouldOrNotFire(Plane plane){
		if(currentState.ShouldOrNotFire((AIPlane)plane))
			return true;
		return false;
	}

	protected override bool IsPossibleToMoveTo(Vector2 newPos){
		return WorldSpaceGameBoundary.Instance.IsPointInsideExtraSpace(newPos);
	}
}
