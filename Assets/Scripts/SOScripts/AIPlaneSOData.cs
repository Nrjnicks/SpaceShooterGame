using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIPlaneSOData", menuName = "Plane Data/AI")]
public class AIPlaneSOData : PlaneSOData {
	
	[Header("AI specific Plane Data")]
	public float minDistanceToAttack = 5;
	public float FOVToAttack = 10;
	public float maxActiveTimeOnScreen = 10;
	[Header("Score")]
	public float scoreBonusOnKill = 50;
}
