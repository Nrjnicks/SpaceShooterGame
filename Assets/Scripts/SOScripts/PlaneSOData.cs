using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenericPlaneSOData", menuName = "Plane Data/Generic")]
public class PlaneSOData : ScriptableObject {
	[Header("Plane Prefab for this data")]
	[Tooltip("Color of Plane. (This variable can be changed to Sprite or Prefab, based on requirement)")]
	public Color planeColor = Color.white;
	[Header("Generic Plane Data")]
	[Tooltip("Name of the Plane of better in-game reference in future")]public string planeName = "Plane";
	[Tooltip("Speed of movement")]public float Speed = 5;//Relative Thrust	
	[Tooltip("Max Health of this Plane")]public float maxHealth = 100;
	[Tooltip("Fire Attack Cooldown")]public float attackCooldown = 0.5f;
	[Tooltip("Speed of bullet (or any projectile)")]public float bulletSpeed = 15;
	[Tooltip("Strengh of the bullet")]public float bulletStrength = 15;
}
