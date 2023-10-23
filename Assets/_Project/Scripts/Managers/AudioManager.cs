using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;
	
	public static AudioManager manager;
	
	void Awake()
	{
		 if(manager == null){
			manager = this;
		}
		else{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}
	
	void OnEnable()
	{
		VolumeController.OnMasterSliderChange += ChangeMasterVol;
		VolumeController.OnMusicSliderChange += ChangeMusicVol;
		VolumeController.OnSFXSliderChange += ChangeSFXVol;
	}
	
	public void ChangeMasterVol(float volume){
		mixer.SetFloat("MasterVolume", volume);
	}
	public void ChangeMusicVol(float volume){
		mixer.SetFloat("MusicVolume", volume);
	}
	public void ChangeSFXVol(float volume){
		mixer.SetFloat("SfxVolume", volume);
	}
}
