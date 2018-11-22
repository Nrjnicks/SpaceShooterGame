using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldSpaceGameBoundary : MonoBehaviour {
	static WorldSpaceGameBoundary instance;
	public static WorldSpaceGameBoundary Instance{
		get {if(instance==null) instance = GameObject.FindObjectOfType<WorldSpaceGameBoundary>();
			return instance;
			} 
		}

	Vector2 centre, boundSize;
	Camera gameCamera;
	// Use this for initialization
	void Awake(){
		if(instance!=null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		gameCamera = Camera.main;
	}
	void OnEnable () {
		Vector2 bottomLeftWorldSpaceBound = gameCamera.ScreenToWorldPoint(Vector3.zero);
		Vector2 topRightWorldSpaceBound = gameCamera.ScreenToWorldPoint(new Vector3(gameCamera.pixelWidth, gameCamera.pixelHeight));
		boundSize = topRightWorldSpaceBound - bottomLeftWorldSpaceBound;
		centre = 0.5f * boundSize + bottomLeftWorldSpaceBound;
	}

	///<description>Is Point Inside Camera View</description>
	public bool IsPointInsideCameraView(Vector2 position){
		return IsPointInsideBound(position,boundSize);
	}

	///<description>IsPointOutsideCameraView but inside half of (buffer) extra space</description>
	public bool IsPointOutsideCameraView(Vector2 position){
		return !IsPointInsideBound(position,boundSize+extraSpace*0.5f);
	}

	Vector2 extraSpace = new Vector2(4,4);
	///<description>Is Point Inside (Buffer)Extra Space</description>
	public bool IsPointInsideExtraSpace(Vector2 position){
		return IsPointInsideBound(position,boundSize+extraSpace);
	}
	
	bool IsPointInsideBound(Vector2 _position, Vector2 _boundSize){
		_position = new Vector2(Mathf.Abs(_position.x - centre.x),Mathf.Abs(_position.y - centre.y));//relative to camera centre;
		if(_position.x>0.5f*_boundSize.x)
			return false;
		if(_position.y>0.5f*_boundSize.y)
			return false;
		return true;
	}

	void OnDestroy(){
		instance = null;
	}
	

	#if UNITY_EDITOR//Draw Gizmo for ease of Level Designers
	Color inColor = new Color(0, 1, 0, 0.3f);
	Color midColor = new Color(1, 1, 0, 0.3f);
	Color outColor = new Color(1, 0, 0, 0.3f);
	void OnDrawGizmosSelected()
    {
		Gizmos.color = outColor;
        Gizmos.DrawCube(centre, boundSize + extraSpace);
		Gizmos.color = midColor;
        Gizmos.DrawCube(centre, boundSize + extraSpace*0.5f);
		Gizmos.color = inColor;
        Gizmos.DrawCube(centre, boundSize);
    }
	#endif

}
