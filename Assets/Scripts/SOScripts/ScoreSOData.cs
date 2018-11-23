using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSOData", menuName = "Level/Score Update Data")]
public class ScoreSOData : ScriptableObject {
	[Range(0.02f,30)] [Tooltip("update score every (this) sec")]public float scoreUpdateFrequency = 0.5f;
	[Tooltip("Add (this) score every update cycle")]public float scorePerUpdateCycle = 10;
	[Tooltip("Level Complete Bonus")]public float scorePerLevelComplete = 1000;
	
}
