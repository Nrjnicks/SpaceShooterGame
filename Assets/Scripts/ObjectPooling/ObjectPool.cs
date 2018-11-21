using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component {

	public T objectForPool;
	public int poolAmount = 5;
	public bool canGrow = false;
	int nextUnused = -1;
	
	List<T> ObjPool;
	
	// Use this for initialization
	protected virtual void Start () {
		if(objectForPool==null) return;
		CreatePool();
	}

	public virtual void CreatePool(T poolObject){
		objectForPool = poolObject;
		CreatePool();
	}

	protected void CreatePool(){
		DestroyAllPoolObjects();
		ObjPool = new List<T>();
		for (int i = 0; i < poolAmount; i++)
		{
			ObjPool.Add(CreateOnePoolObject());
		}
		nextUnused = 0;
	}	
	T CreateOnePoolObject(){
		T poolObj;
		poolObj = Instantiate(objectForPool,transform) as T;
		poolObj.gameObject.SetActive(false);
		return poolObj;
	}

	public virtual void DisableAllPoolObjects(){
		if(ObjPool == null) return;
		foreach(T obj in ObjPool){
			obj.gameObject.SetActive(false);
		}
	}	

	public virtual void DestroyAllPoolObjects(){
		if(ObjPool == null) return;
		foreach(T obj in ObjPool){
			Destroy(obj.gameObject);
		}
	}

	protected T GetNextUnusedPooledObject(){
		if(nextUnused == -1){
			Debug.Log("Pool Not Created yet!");
			return null;
		}
		T nextObj;
		if(ObjPool[nextUnused].gameObject.activeSelf){				
			if(canGrow){
				nextObj = CreateOnePoolObject();
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
