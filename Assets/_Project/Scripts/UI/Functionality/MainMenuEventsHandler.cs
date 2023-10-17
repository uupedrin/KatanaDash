using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEventsHandler : MonoBehaviour
{
	void OnEnable()
	{
		MainMenuScreen.PlayButtonClick += PlayBtn;
		MainMenuScreen.SettingsButtonClick += SettingsBtn;
		MainMenuScreen.QuitButtonClick += QuitBtn;
	}
	
	void OnDisable()
	{
		MainMenuScreen.PlayButtonClick -= PlayBtn;
		MainMenuScreen.SettingsButtonClick -= SettingsBtn;
		MainMenuScreen.QuitButtonClick -= QuitBtn;
	}
	
	void PlayBtn()
	{
		Debug.Log("Play");
	}
	void SettingsBtn()
	{
		Debug.Log("Settings");
	}
	void QuitBtn()
	{
		Application.Quit();
		Debug.Log("Quit");
	}
}
