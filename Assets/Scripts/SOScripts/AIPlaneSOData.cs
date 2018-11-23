using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIPlaneSOData", menuName = "Plane Data/AI")]
public class AIPlaneSOData : PlaneSOData {
	
	[Header("AI specific Plane Data")]
	[Tooltip("Attack player once reach this distance")]public float minDistanceToAttack = 5;
	[Tooltip("Field of View of AI")]public float FOVToAttack = 10;
	[Tooltip("Max time AI will be active on screen before evading")]public float maxActiveTimeOnScreen = 10;
	[Header("Score")]
	[Tooltip("Score bonus when enemy is killed")]public float scoreBonusOnKill = 50;
}
