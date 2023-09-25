using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider FxVolume;
    public Slider MusicVolume;
    public Slider MasterVolume;
   
    //Dei uma Hardcordada pq eu não sabia se o jeito que eu tava pensando funcionaria
    public void FxVolChange()
    {
        mixer.SetFloat("FxVolume", FxVolume.value);
    }
    public void MusicChange()
    {
        mixer.SetFloat("MusicVolume", MusicVolume.value);
    }
    public void MasterChange()
    {
        mixer.SetFloat("MasterVolume", MusicVolume.value);
    }
}
