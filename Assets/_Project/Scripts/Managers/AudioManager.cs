using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;
	public Slider masterSlider;
	public Slider musicSlider;
	public Slider sfxSlider;
	
	public static AudioManager manager;
	
	void Awake()
	{
		if(manager == null)
		{
			manager = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}
	public void ChangeMasterVol()
	{
		mixer.SetFloat("MasterVolume", (masterSlider.value));
	}
	public void ChangeMusicVol()
	{
		mixer.SetFloat("MusicVolume",(musicSlider.value));
	}
	public void ChangeSFXVol()
	{
		mixer.SetFloat("SfxVolume", (sfxSlider.value));
	}
}
