﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericPlaneSOData", menuName = "Plane Data/Generic")]
public class PlaneSOData : ScriptableObject {
	[Header("Plane Prefab for this data")]
	public Color planeColor = Color.white;
	[Header("Generic Plane Data")]
	public string planeName = "Plane";
	public float Speed = 5;//Relative Thrust	
	public float maxHealth = 100;
	public float attackCooldown = 0.5f;
	public float bulletSpeed = 15;
	public float bulletStrength = 15;
}
