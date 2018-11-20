using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : ObjectPool<Plane> {

	public Plane SpawnPlane(PlaneSOData planeSOData, APlaneContoller planeContoller, Transform spawnPosRot){
		Plane plane= GetNextUnusedPooledObject();
		plane.InitPlane(planeSOData, planeContoller,spawnPosRot);
        return plane;
	}

}
