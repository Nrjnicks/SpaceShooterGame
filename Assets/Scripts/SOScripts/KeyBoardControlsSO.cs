using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBoardControlsSO", menuName = "Controls/Keyboard")]
public class KeyBoardControlsSO : ScriptableObject {
	public KeyCode Forward = KeyCode.W;
	public KeyCode Backward  = KeyCode.S;
	public KeyCode Left  = KeyCode.A;
	public KeyCode Right  = KeyCode.D;
	public KeyCode Fire  = KeyCode.Space;
}
