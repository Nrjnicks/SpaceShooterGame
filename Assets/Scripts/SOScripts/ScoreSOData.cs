using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSOData", menuName = "Level/Score Update Data")]
public class ScoreSOData : ScriptableObject {
	[Range(0.02f,30)] public float scoreUpdateFrequency = 0.5f;
	public float scorePerUpdateCycle = 10;
	public float scorePerLevelComplete = 1000;
	
}
