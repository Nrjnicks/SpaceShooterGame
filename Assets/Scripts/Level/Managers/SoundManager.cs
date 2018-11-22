using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource audioSource;
	public AudioSource SFXAudioSource;
	public AudioClip levelOnGoingMusic;
	public AudioClip levelOnCompleteMusic;
	public AudioClip blastSfx;
	
	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	public void InitParam(LevelManager levelManager){
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		levelManager.onEnemyKilled += OnEnemyKilled;
	}

	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public void ResetParam(LevelManager levelManager){
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		levelManager.onEnemyKilled -= OnEnemyKilled;
	}

	void OnEnemyKilled(Plane plane){
		PlayBlastSFX();
	}

	void OnLevelStart(int level){
		// audioSource.clip = levelOnGoingMusic;
		audioSource.loop = true;
		audioSource.Play();
	}

	void OnLevelComplete(int level){
		// audioSource.clip = levelOnCompleteMusic;
		audioSource.loop = false;
		audioSource.Play();
	}

	void PlayBlastSFX(){
		// SFXAudioSource.clip = blastSfx;
		SFXAudioSource.loop = false;
		SFXAudioSource.Play();
	}

	public void SetVolume(float vol){
		audioSource.volume = vol;
		SFXAudioSource.volume = vol;
	}
}
