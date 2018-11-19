﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : ObjectPool<Plane> {

	public Plane SpawnPlane(PlaneSOData planeSOData, APlaneContoller planeContoller, Vector2 position){
		Plane plane= GetNextUnusedPooledObject();
		plane.InitPlane(planeSOData, planeContoller);
		plane.transform.position = position;
		plane.gameObject.SetActive(true);
        return plane;
	}

}
