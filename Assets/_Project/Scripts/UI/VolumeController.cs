using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeController : MonoBehaviour
{
	public static Action<float> OnMasterSliderChange, OnMusicSliderChange, OnSFXSliderChange;
	VisualElement masterSlider, musicSlider, sfxSlider;
	
	IEnumerator Start()
	{
		yield return new WaitForSeconds(.15f);
		GetReferences();
	}
	
	void GetReferences()
	{
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		masterSlider = root.Query<Slider>("customSlider").AtIndex(0);
		musicSlider = root.Query<Slider>("customSlider").AtIndex(1);
		sfxSlider = root.Query<Slider>("customSlider").AtIndex(2);
		
		masterSlider.RegisterCallback<ChangeEvent<float>>(MasterValueChanged);
		musicSlider.RegisterCallback<ChangeEvent<float>>(MusicValueChanged);
		sfxSlider.RegisterCallback<ChangeEvent<float>>(SFXValueChanged);
	}
	
	void MasterValueChanged(ChangeEvent<float> value)
	{
		OnMasterSliderChange?.Invoke(value.newValue);
	}
	void MusicValueChanged(ChangeEvent<float> value)
	{
		OnMusicSliderChange?.Invoke(value.newValue);
	}
	void SFXValueChanged(ChangeEvent<float> value)
	{
		OnSFXSliderChange?.Invoke(value.newValue);
	}
	
}
