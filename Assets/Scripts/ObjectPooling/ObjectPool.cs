using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component {

	public T objectForPool;
	public int poolAmount = 5;
	public bool canGrow = false;
	int nextUnused;
	
	List<T> ObjPool;
	
	// Use this for initialization
	protected virtual void Start () {
		CreatePool();
	}

	protected void CreatePool(){
		ObjPool = new List<T>();
		T temp;
		for (int i = 0; i < poolAmount; i++)
		{
			temp = Instantiate(objectForPool,transform) as T;
			temp.gameObject.SetActive(false);
			ObjPool.Add(temp);
		}
		nextUnused = 0;
	}

	protected T GetNextUnusedPooledObject(){
		T nextObj;
		if(ObjPool[nextUnused].gameObject.activeSelf){				
			if(canGrow){
				nextObj = Instantiate(objectForPool,transform) as T;
				ObjPool.Add(nextObj);
				poolAmount++;
				nextUnused++;
			}	
			else{
				Debug.Log("Pool size of type *"+(ObjPool[nextUnused]).GetType()+"* is small and can't grow");
				nextObj = null;
			}
		}
		else{
			nextObj = ObjPool[nextUnused];
			nextUnused = (nextUnused+1) % ObjPool.Count;
		}
		return nextObj;
	}


}
