using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlane : Plane {
	public Plane enemyPlane{get; private set;}
	float activateTime;
	public float ActiveTime{get{return Time.time - activateTime;}}

	public override void InitPlane(PlaneSOData PlaneData, APlaneContoller PlaneContoller, Transform spawnPosRot = null){
		enemyPlane = ((AIPlaneController) PlaneContoller).GetRandomEnemyPlane();
		if(enemyPlane == null) DisablePlane();
		
		base.InitPlane(PlaneData, PlaneContoller, spawnPosRot);
		activateTime = Time.time;
	}

	public void SetEnemyPlane(Plane plane){
		enemyPlane = plane;
	}
}
