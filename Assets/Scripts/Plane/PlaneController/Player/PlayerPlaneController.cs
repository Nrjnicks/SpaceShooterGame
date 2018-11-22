using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaneController : APlaneContoller {

	KeyBoardControlsSO keyControls;

	protected override bool ShouldOrNotMoveForward(){
		if(Input.GetKey(keyControls.Forward))
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveBackward(){
		if(Input.GetKey(keyControls.Backward))
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveLeft(){
		if(Input.GetKey(keyControls.Left))
			return true;
		return false;
	}
	protected override bool ShouldOrNotMoveRight(){
		if(Input.GetKey(keyControls.Right))
			return true;
		return false;
	}
	protected override bool ShouldOrNotFire(Plane plane){
		if(Input.GetKey(keyControls.Fire))
			return true;
		return false;
	}

	
	///<description>Get Direction to move to, for this plane</description>
	public override Vector2 GetMoveDeltaPosition (Plane plane) {
		keyControls = ((PlayerPlane)plane).keyControls;
		return base.GetMoveDeltaPosition(plane);
	}

}
