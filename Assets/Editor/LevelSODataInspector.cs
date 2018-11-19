using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelsSOData))]
[CanEditMultipleObjects]
public class LevelSODataInspector : Editor {

	SerializedProperty winCondition;
	SerializedProperty numOfLevel;
	SerializedProperty levelDatas;
	SerializedProperty commonWinCondition;
	SerializedProperty timeDiffBwLevel;
	void OnEnable()
    {
        winCondition = serializedObject.FindProperty("winCondition");
        numOfLevel = serializedObject.FindProperty("totalNumOfLevels");
        levelDatas = serializedObject.FindProperty("levelDatas");
		commonWinCondition = serializedObject.FindProperty("isCommonWinCondition");
		timeDiffBwLevel = serializedObject.FindProperty("timeDifferenceBetweenLevels");
    }
	public override void OnInspectorGUI(){
		serializedObject.Update();
		
		EditorGUILayout.Space();
		commonWinCondition.boolValue = EditorGUILayout.Toggle("Common Win Condition for All Levels?", commonWinCondition.boolValue);
		if(commonWinCondition.boolValue){
			EditorGUILayout.PropertyField(winCondition, new GUIContent("Common Win Condition"));
			EditorGUILayout.Space();
		}
		EditorGUILayout.Space();
        EditorGUILayout.PropertyField(numOfLevel);
		if(numOfLevel.intValue>0){
			EditorGUILayout.Space();
			timeDiffBwLevel.floatValue = EditorGUILayout.FloatField("Time Between Consecutive Levels", timeDiffBwLevel.floatValue);
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Level Datas", EditorStyles.boldLabel);
			levelDatas.arraySize = numOfLevel.intValue;	
			ShowList(levelDatas);
		}
        serializedObject.ApplyModifiedProperties();

	}
	SerializedProperty inList, winCond, spawnFrq;
	void ShowList(SerializedProperty list){
        EditorGUILayout.PropertyField(list);
		if(list.isExpanded){
			EditorGUI.indentLevel += 1;
			for (int i = 0; i < list.arraySize; i++)
			{					
				inList = list.GetArrayElementAtIndex(i).FindPropertyRelative("enemySpawnSequence");
				EditorGUILayout.PropertyField(inList, new GUIContent("Level "+ (i+1), "Level Data for level "+(i+1)));
					
				if(inList.isExpanded){
					EditorGUI.indentLevel += 1;
					winCond = list.GetArrayElementAtIndex(i).FindPropertyRelative("winCondition");
					if(!commonWinCondition.boolValue){						
						EditorGUILayout.PropertyField( winCond, new GUIContent("Win condition for this level"));
						EditorGUILayout.Space();
					}
					inList.arraySize = EditorGUILayout.IntField ("Sequence Size", inList.arraySize);
					for (int j = 0; j < inList.arraySize; j++)
					{	
						EditorGUILayout.PropertyField(inList.GetArrayElementAtIndex(j), new GUIContent("Sqn"+j+": DataSO, Num, Frequency"));
					}
					EditorGUILayout.Space();
					if(inList.arraySize>1){						
						spawnFrq = list.GetArrayElementAtIndex(i).FindPropertyRelative("sequenceSpawnFrequency");
						spawnFrq.floatValue = EditorGUILayout.FloatField ("Sequence Spawn Frequency in sec", spawnFrq.floatValue);
					}	
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();
					EditorGUILayout.Space();
				}
			}
			// 
		}
	}
}

[CustomPropertyDrawer(typeof(NoOfAIPerType))]
public class NoOfAIPerTypeDrawer: PropertyDrawer
{
	
	SerializedProperty aIPlaneController;
	SerializedProperty numOfSpawns;
	SerializedProperty frqOfSpawns;
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		aIPlaneController = property.FindPropertyRelative("aIPlaneSOData");
		numOfSpawns = property.FindPropertyRelative("numberOfSpawns");
		frqOfSpawns = property.FindPropertyRelative("spawnFrequency");

		label = EditorGUI.BeginProperty(position, label, property);
		Rect contentPosition = EditorGUI.PrefixLabel(position, label);
		contentPosition.x = contentPosition.width*0.5f;
		contentPosition.width *= 0.6f;
		EditorGUI.PropertyField(contentPosition, aIPlaneController, GUIContent.none);
		contentPosition.x += contentPosition.width;
		contentPosition.width *= 0.5f;
		numOfSpawns.intValue = EditorGUI.IntField(contentPosition, numOfSpawns.intValue);
		contentPosition.x += contentPosition.width;
		frqOfSpawns.floatValue = EditorGUI.FloatField(contentPosition, frqOfSpawns.floatValue);
		EditorGUI.EndProperty();
	}
}
