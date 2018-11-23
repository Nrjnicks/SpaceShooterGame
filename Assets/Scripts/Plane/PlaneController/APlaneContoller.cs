using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class APlaneContoller: MonoBehaviour { //DI

	//Conditions to move or fire-->
	protected abstract bool ShouldOrNotMoveForward();
	protected abstract bool ShouldOrNotMoveBackward();
	protected abstract bool ShouldOrNotMoveLeft();
	protected abstract bool ShouldOrNotMoveRight();
	protected abstract bool ShouldOrNotFire(Plane plane);
	//<--
	
	
	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	public virtual void InitControls(LevelManager levelManager){

	}
	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public virtual void ResetControls(LevelManager levelManager){

	}
	public virtual void UpdateControls(Plane plane){
		Move(plane, GetMoveDeltaPosition(plane));
		if(ShouldOrNotFire(plane))
			plane.FireBullet();
	}
	Vector2 deltaPos;
	///<description>Get Direction to move to, for this plane</description>
	public virtual Vector2 GetMoveDeltaPosition (Plane plane) {
		deltaPos = Vector2.zero;
		if(ShouldOrNotMoveForward())
			deltaPos+=Vector2.up;
		if(ShouldOrNotMoveBackward())
			deltaPos+=Vector2.down;
		if(ShouldOrNotMoveLeft())
			deltaPos+=Vector2.left;
		if(ShouldOrNotMoveRight())
			deltaPos+=Vector2.right;
		return deltaPos;
	}
	Vector2 newPos;
	///<description>Move this plane in this relative direction</description>
	protected void Move(Plane plane, Vector2 relDirection){
		if(relDirection == Vector2.zero) return;

		newPos = plane.transform.position + plane.planeData.Speed* (Vector3)relDirection * Time.deltaTime;
		if(IsPossibleToMoveTo(newPos)){
			plane.transform.position = newPos;
		}
	}

	///<description>Check if point is inside movable space</description>
	protected virtual bool IsPossibleToMoveTo(Vector2 newPos){
		return WorldSpaceGameBoundary.Instance.IsPointInsideCameraView(newPos);
	}


	///<description>Fire bullet</description>
	public void FireBullet(Plane plane){		
		onFireShot(plane);
	}

	public System.Action<Plane> onFireShot;
}
