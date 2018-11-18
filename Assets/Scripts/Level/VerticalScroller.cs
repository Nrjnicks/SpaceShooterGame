using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroller : MonoBehaviour {

	public Material verticalScrollableMat;
	[Range(0,0.01f)] public float speed = 0.002f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		verticalScrollableMat.mainTextureOffset += Vector2.up * speed;
	}

	void OnDisable(){
		verticalScrollableMat.mainTextureOffset += Vector2.zero;
	}
}
