using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlane : Plane {
	
	[HideInInspector] public int playerNumber = 1;
	public KeyBoardControlsSO keyControls; //Keyboard / joystick keys which will control movement of the plane
}	
[System.Serializable]
public struct PlayerDataAndControl
{
	public PlaneSOData planeSOData;
	public KeyBoardControlsSO keyBoardControl;
}
