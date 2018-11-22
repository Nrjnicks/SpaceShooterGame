using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPool : ObjectPool<SpriteRenderer> {
	[Range(0.1f,1)]public float blastActiveFor = 0.3f;
	public void InitParam(LevelManager levelManager){
		levelManager.onEnemyKilled +=OnPlaneDestroyed;
		levelManager.onPlayerKilled +=OnPlaneDestroyed;
	}

	public void ResetParam(LevelManager levelManager){
		levelManager.onEnemyKilled -=OnPlaneDestroyed;
		levelManager.onPlayerKilled -=OnPlaneDestroyed;
	}

	public void OnPlaneDestroyed(Plane plane){
		SpawnBlast(plane.transform.position);
	}

	///<description>Spawn a blast at this position</description>
	void SpawnBlast(Vector2 position){
		SpriteRenderer blast = GetNextUnusedPooledObject();
		blast.transform.position = position;
		if(gameObject.activeInHierarchy) StartCoroutine(EnableDisable(blast.gameObject, blastActiveFor));
	}

	IEnumerator EnableDisable(GameObject obj, float disableAfter){
		obj.SetActive(true);
		yield return new WaitForSeconds(disableAfter);
		obj.SetActive(false);		
	}

	///<description>Set Blast Sprite</description>
	public void SetAllBlastSprite(Sprite sprite){
		objectForPool.sprite = sprite;
		
		List<SpriteRenderer> blastPool = GetPoolList();
		foreach (SpriteRenderer blast in blastPool)
		{			
			blast.sprite = sprite;
		}
	}
}
