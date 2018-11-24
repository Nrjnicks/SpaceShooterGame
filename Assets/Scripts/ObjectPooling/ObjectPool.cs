using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component {

	public T objectForPool;
	[Tooltip("initial number of components to instantiate")] public int poolAmount = 5;
	[Tooltip("Can this pool grow or not")] public bool canGrow = false;
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

	///<description>Create New Pool</description>
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

	///<description>Disable All Pool Elements</description>
	public virtual void DisableAllPoolObjects(){
		if(ObjPool == null) return;
		foreach(T obj in ObjPool){
			obj.gameObject.SetActive(false);
		}
	}	

	///<description>Destroy All Pool Elements</description>
	public virtual void DestroyAllPoolObjects(){
		if(ObjPool == null) return;
		foreach(T obj in ObjPool){
			Destroy(obj.gameObject);
		}
	}

	///<description>Get Next Unused Pooled Object. returns next disabled object or create new if pool can grow</description>
	protected T GetNextUnusedPooledObject(){
		if(nextUnused == -1){
			Debug.Log("Pool Not Created yet!");
			return null;
		}
		T nextObj;
		if(ObjPool[nextUnused].gameObject.activeSelf){
			int i = 1;
			for (; i < ObjPool.Count; i++)
			{
				if(!ObjPool[(nextUnused+i)%ObjPool.Count].gameObject.activeSelf){
					nextUnused = (nextUnused+i+1)%ObjPool.Count;
					return ObjPool[(nextUnused+i)%ObjPool.Count];
				}
			}
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

	public List<T> GetPoolList(){
		return ObjPool;
	}


}
