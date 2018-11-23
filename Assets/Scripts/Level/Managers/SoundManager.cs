using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource audioSource;
	public AudioSource SFXAudioSource;
	[Space]
	public AudioClip levelOnGoingMusic;
	public AudioClip levelOnCompleteMusic;
	public AudioClip fireBulletSfx;
	public AudioClip blastSfx;
	
	///<description>Initializing Internal Parameters of this instance</description>
	///<param name="levelManager">LevelManager instance</param>
	public void InitParam(LevelManager levelManager){
		levelManager.onLevelStart += OnLevelStart;
		levelManager.onLevelComplete += OnLevelComplete;
		levelManager.onEnemyKilled += OnEnemyKilled;
		levelManager.playerPlaneController.onFireShot+=PlayFireSFX;
		levelManager.aIPlaneController.onFireShot+=PlayFireSFX;
		
		PlayGameMusic();
	}

	///<description>Reseting Internal Parameters</description>
	///<param name="levelManager">LevelManager instance</param>
	public void ResetParam(LevelManager levelManager){
		levelManager.onLevelStart -= OnLevelStart;
		levelManager.onLevelComplete -= OnLevelComplete;
		levelManager.onEnemyKilled -= OnEnemyKilled;
		levelManager.playerPlaneController.onFireShot-=PlayFireSFX;
		levelManager.aIPlaneController.onFireShot-=PlayFireSFX;
	}

	public void PlayGameMusic(){
		PlayMusic(levelOnGoingMusic,true);
	}

	void OnEnemyKilled(Plane plane){
		PlayBlastSFX();
	}

	void OnLevelStart(int level){
		PlayMusic(levelOnGoingMusic,true);
	}

	void OnLevelComplete(int level){
		PlayMusic(levelOnCompleteMusic);
	}

	void PlayBlastSFX(){
		PlaySFX(blastSfx);
	}

	void PlayFireSFX(Plane plane){
		PlaySFX(fireBulletSfx);
	}

	public void SetVolume(float vol){
		audioSource.volume = vol;
		SFXAudioSource.volume = vol;
	}

	public void PlayMusic(AudioClip audioClip, bool loop = false){
		if(audioSource.clip == audioClip) return;
		audioSource.clip = audioClip;
		audioSource.loop = loop;
		audioSource.Play();		
	}

	public void PlaySFX(AudioClip audioClip){
		SFXAudioSource.clip = audioClip;
		SFXAudioSource.Play();		
	}
}
