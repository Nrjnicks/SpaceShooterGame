using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class APlaneContoller: MonoBehaviour {
	protected abstract bool ShouldOrNotMoveForward();
	protected abstract bool ShouldOrNotMoveBackward();
	protected abstract bool ShouldOrNotMoveLeft();
	protected abstract bool ShouldOrNotMoveRight();
	protected abstract bool ShouldOrNotFire(Plane plane);
	
	public BulletPool bulletPool;

	public virtual void InitControls(LevelManager levelManager){

	}
	
	public virtual void UpdateControls(Plane plane){
		Move(plane, GetMoveDeltaPosition(plane));
		if(ShouldOrNotFire(plane))
			plane.FireBullet();
	}
	Vector2 deltaPos;
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
	protected void Move(Plane plane, Vector2 relDirection){
		if(relDirection == Vector2.zero) return;

		newPos = plane.transform.position + plane.PlaneData.Speed* (Vector3)relDirection * Time.deltaTime;
		if(IsPossibleToMoveTo(newPos)){
			plane.transform.position = newPos;
		}
	}

	protected virtual bool IsPossibleToMoveTo(Vector2 newPos){
		return WorldSpaceGameBoundary.Instance.IsPointInsideCameraView(newPos);
	}


	public void FireBullet(Plane plane){		
		bulletPool.FireBulletFromPool(plane.transform.position + plane.transform.up, plane.transform.up, plane.PlaneData.bulletSpeed, plane.PlaneData.bulletStrength);
	}
}
