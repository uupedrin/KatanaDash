using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;
    public static AudioManager manager;
    public Slider masterSlider;
	public Slider musicSlider;
	public Slider sfxSlider;
	public GameObject buttonClick;
    public GameObject coin;
    public GameObject damage;
	public GameObject dash;
	public GameObject explosion;
	public GameObject jumpPwP;
	public GameObject katanaHit;
	public GameObject achievment;
	void Start()
	{
		if(manager == null)
		{
			manager = this;
		}
		else
		{
			Destroy(gameObject);
		}
		masterSlider.value = GameManager.manager.saveMasterSlider;
		musicSlider.value = GameManager.manager.saveMusicSlider;
		sfxSlider.value = GameManager.manager.saveSfxSlider;
	}
	public void ChangeMasterVol()
	{
		mixer.SetFloat("MasterVolume", (masterSlider.value));
        GameManager.manager.saveMasterSlider = masterSlider.value;
    }
	public void ChangeMusicVol()
	{
		mixer.SetFloat("MusicVolume",(musicSlider.value));
        GameManager.manager.saveMusicSlider = musicSlider.value;
	}
	public void ChangeSFXVol()
	{
		mixer.SetFloat("SfxVolume", (sfxSlider.value));
        GameManager.manager.saveSfxSlider = sfxSlider.value;
	}
	public void PlayClick() 
	{
		Instantiate(buttonClick);
	}
}